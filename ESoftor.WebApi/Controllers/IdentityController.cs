using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ESoftor.AspNetCore;
using ESoftor.AspNetCore.UI;
using ESoftor.Core.Permission.Identity;
using ESoftor.Core.Permission.Identity.Dto;
using ESoftor.Core.Permission.Identity.Entity;
using ESoftor.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESoftor.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Description("Identity Controller")]
    public class IdentityController : ControllerBase
    {

        #region ctor
        public IdentityController(IIdentityContract identityContract)
        {
            _identityService = identityContract;
        }
        #endregion

        #region fields
        private readonly IIdentityContract _identityService;
        #endregion

        /// <summary>
        ///     登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("用户登录")]
        public async Task<AjaxResult> Login([FromBody] LoginDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            if (!ModelState.IsValid)
                return new AjaxResult("提交信息验证失败", AjaxResultType.Error);
            //todo: 校验验证码
            dto.Ip = HttpContext.GetClientIp();
            dto.UserAgent = Request.Headers["User-Agent"].FirstOrDefault();

            OperationResult<User> result = await _identityService.Login(dto);

            return new AjaxResult(result.Message);
        }

        /// <summary>
        ///     Jwt登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("jwt登录")]
        public async Task<AjaxResult> Jwtoken([FromBody]LoginDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            if (!ModelState.IsValid)
                return new AjaxResult("提交信息验证失败", AjaxResultType.Error);
            //todo: 校验验证码
            dto.Ip = HttpContext.GetClientIp();
            dto.UserAgent = Request.Headers["User-Agent"].FirstOrDefault();

            OperationResult<string> token = await _identityService.JwtLogin(dto);

            return new AjaxResult("登录成功", AjaxResultType.Success, token);
        }

    }
}