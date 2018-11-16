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
using ESoftor.Json;
using ESoftor.Framework;
using Microsoft.AspNetCore.Authorization;
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

        #region login apis
        /// <summary>
        ///     注册
        /// </summary>
        /// <param name="dto">注册参数模型</param>
        /// <returns></returns>
        [HttpPost]
        [Description("注册")]
        public async Task<AjaxResult> Register([FromBody] RegisterDto dto)
        {
            Check.NotNull(dto, nameof(dto));

            if (!ModelState.IsValid)
            {
                return new AjaxResult("提交信息验证失败", AjaxResultType.Error);
            }

            //todo 验证码 验证操作
            dto.RegisterIp = HttpContext.GetClientIp();
            OperationResult<User> result = await _identityService.Register(dto);
            if (result.Successed)
            {
                //User user = result.Data;
                //string code = await _identityService.GenerateEmailConfirmationTokenAsync(user);
                //code = UrlBase64ReplaceChar(code);
                //string url = $"{Request.Scheme}://{Request.Host}/#/identity/confirm-email?userId={user.Id}&code={code}";
                //string body =
                //    $"亲爱的用户 <strong>{user.NickName}</strong>[{user.UserName}]，您好！<br>"
                //    + $"欢迎注册，激活邮箱请 <a href=\"{url}\" target=\"_blank\"><strong>点击这里</strong></a><br>"
                //    + $"如果上面的链接无法点击，您可以复制以下地址，并粘贴到浏览器的地址栏中打开。<br>"
                //    + $"{url}<br>"
                //    + $"祝您使用愉快！";
                //await SendMailAsync(user.Email, "柳柳软件 注册邮箱激活邮件", body);
            }

            return result.ToAjaxResult();
        }

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

            OperationResult<string> result = await _identityService.Login(dto);

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
        #endregion

        #region authorication apis
        /// <summary>
        ///     OAuth2登录
        /// </summary>
        /// <param name="provider">第三方登录提供器</param>
        /// <param name="returnUrl">回调地址</param>
        /// <returns></returns>
        [HttpGet]
        [Description("OAuth2登录")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> OAuth2(string provider, string returnUrl = null)
        {
            string redirectUrl = Url.Action(nameof(OAuth2Callback), "Index", new { returnUrl });
            var result = await _identityService.OAuth2(provider, redirectUrl);
            return Challenge(result);
        }

        /// <summary>
        ///     OAuth2登录回调
        /// </summary>
        /// <param name="returnUrl">回调地址</param>
        /// <param name="remoteError">第三方登录错误提示</param>
        /// <returns></returns>
        [HttpGet]
        [Description("OAuth2登录回调")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> OAuth2Callback(string returnUrl = null, string remoteError = null)
        {
            var result = await _identityService.OAuth2Callback(returnUrl, remoteError);
            if (result) return Ok();
            return Unauthorized();
        }
        #endregion

        #region 微信测试token验证地址
        /// <summary>
        ///     微信测试token验证地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CheckSignature()
        {
            var signature = HttpContext.Request.Params("signature");
            var echostr = HttpContext.Request.Params("echostr");
            var timestamp = HttpContext.Request.Params("timestamp");
            var nonce = HttpContext.Request.Params("nonce");

            string[] ArrTmp = { AppSettingManager.Get("ESoftor:Jwt:Secret"), timestamp, nonce };
            Array.Sort(ArrTmp);	 //字典排序
            string tmpStr = string.Join("", ArrTmp);
            //tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = Security.HashHelper.GetSha1(tmpStr);
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                var json = new
                {
                    appid = AppSettingManager.Get("Authentication:WeChat:AppId"),//AppKey
                    timestamp,
                    nonceStr = nonce,
                    signature
                };
                return await Task.FromResult(Content(json.ToJsonString()));
            }
            var json1 = new
            {
                appid = AppSettingManager.Get("Authentication:WeChat:AppId"),//AppKey
                timestamp,
                nonceStr = nonce,
                signature = tmpStr
            };
            return await Task.FromResult(Content(json1.ToJsonString()));
        }
        #endregion

    }
}