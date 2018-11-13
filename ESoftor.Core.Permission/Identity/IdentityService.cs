// -----------------------------------------------------------------------
//  <copyright file="IdenityService.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Permission.Identity.Dto;
using ESoftor.Core.Permission.Identity.Entity;
using ESoftor.Data;
using ESoftor.Extensions;
using ESoftor.Framework.Infrastructure;
using ESoftor.Permission.Identity;
using ESoftor.Permission.Identity.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ESoftor.Core.Permission.Identity
{
    public class IdentityService : IIdentityContract
    {
        public IdentityService(
            IUnitOfWork unitOfWork,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<IdentityService> logger)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        #region fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<IdentityService> _logger;
        #endregion

        /// <summary>
        ///     登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResult<User>> Login(LoginDto dto)
        {
            var result = await _Login(dto);
            await _signInManager.SignInAsync(result.Data, dto.Remember);
            return new OperationResult<User>(OperationResultType.Success, "登录成功", result.Data);
        }

        public async Task<OperationResult<string>> JwtLogin(LoginDto dto)
        {
            OperationResult<User> result = await _Login(dto);

            if (!result.Successed)
            {
                return new OperationResult<string>(OperationResultType.Error, result.Message);
            }
            User user = result.Data;

            //生成Token，这里只包含最基本信息，其他信息从在线用户缓存中获取
            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            string token = JwtHelper.CreateToken(claims);

            //TODO 在线用户缓存

            return new OperationResult<string>(OperationResultType.Success, "登录成功", token);

        }

        public async Task<OperationResult> Logout(int userId)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"用户{userId}登出系统");
            //TODO其他消息推送等事件
            return OperationResult.Success;
        }

        /// <summary>
        ///     注册
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResult<User>> Register(RegisterDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            User user = new User() { UserName = dto.UserName, NickName = dto.NickName ?? dto.UserName, Email = dto.Email };
            IdentityResult result = dto.Password == null ? await _userManager.CreateAsync(user) : await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return result.ToOperationResult(user);
            //TODO 注册成功之后的其它操作,如,创建用户详情,创建默认角色及权限信息,and so on;但是一定要记住 uow的commit();
            return result.ToOperationResult(user);
        }


        #region private

        private async Task<OperationResult<User>> _Login(LoginDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            User user = await _userManager.FindByNameAsync(dto.Account);
            if (user == null)
            {
                if (dto.Account.IsEmail())
                    user = await _userManager.FindByEmailAsync(dto.Account);
                if (dto.Account.IsMobileNumber())
                    user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == dto.Account);
            }
            if (user == null)
            {
                return new OperationResult<User>(OperationResultType.QueryNull, "用户不存在");
            }
            if (user.IsLocked)
            {
                return new OperationResult<User>(OperationResultType.Error, $"登录失败,账号已经被冻结");
            }

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);
            OperationResult<User> result = ToOperationResult(signInResult, user);

            //其他事件触发,比如订阅发布
            return result;
        }

        /// <summary>
        ///     处理signInManager的执行结果
        /// </summary>
        /// <param name="result">signInManager的登录结果</param>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        private OperationResult<User> ToOperationResult(SignInResult result, User user)
        {
            if (result.IsNotAllowed)
            {
                if (_userManager.Options.SignIn.RequireConfirmedEmail && !user.EmailConfirmed)
                {
                    _logger.LogWarning(2, $"用户 {user.UserName} 因邮箱未验证而不允许登录");
                    return new OperationResult<User>(OperationResultType.Error, "用户邮箱未验证，请到邮箱收邮件进行确认后再登录");
                }
                if (_userManager.Options.SignIn.RequireConfirmedPhoneNumber && !user.PhoneNumberConfirmed)
                {
                    _logger.LogWarning(2, $"用户 {user.UserName} 因手机号未验证而不允许登录");
                    return new OperationResult<User>(OperationResultType.Error, "用户手机号未验证，请接收手机验证码验证之后再登录");
                }
                return new OperationResult<User>(OperationResultType.Error, "用户未满足登录条件，不允许登录");
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(2, $"用户 {user.UserName} 因密码错误次数过多被锁定，解锁时间：{user.LockoutEnd}");
                return new OperationResult<User>(OperationResultType.Error,
                    $"用户因密码错误次数过多而被锁定 {_userManager.Options.Lockout.DefaultLockoutTimeSpan.TotalMinutes} 分钟，请稍后重试");
            }
            if (result.RequiresTwoFactor)
            {
                return new OperationResult<User>(OperationResultType.Error, "用户登录需要二元验证");
            }
            if (result.Succeeded)
            {
                return new OperationResult<User>(OperationResultType.Success, "用户登录成功", user);
            }
            return new OperationResult<User>(OperationResultType.Error,
                $"用户名或密码错误，剩余 {_userManager.Options.Lockout.MaxFailedAccessAttempts - user.AccessFailedCount} 次机会");
        }

        #endregion
    }
}
