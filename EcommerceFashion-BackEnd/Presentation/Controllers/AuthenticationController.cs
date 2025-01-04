using Application.AccountService.Commands;
using Application.AccountService.Models;
using Application.AccountService.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/authentications")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AccountSignInModel accountSignInModel)
        {
            try
            {
                var command = new AccountSignInCommand(accountSignInModel);
                var result = await _mediator.Send(command);
                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] AccountSignUpModel accountSignUpModel)
        {
            try
            {
                var command = new AccountSignUpCommand(accountSignUpModel);
                var result = await _mediator.Send(command);
                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost("verifycation")]
        public async Task<IActionResult> VerifycationCode([FromBody] AccountVerifyEmailCommand accountVerifyEmailCommand)
        {
            try
            {
                var result = await _mediator.Send(accountVerifyEmailCommand);
                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] AccountLoginModel accountLoginModel,
        //    [FromQuery] bool httpOnly = true)
        //{
        //    try
        //    {
        //        var command = new AccountLoginCommand(accountLoginModel);
        //        var result = await _mediator.Send(command);
        //        if (result.Status)
        //        {
        //            if (httpOnly)
        //            {
        //                HttpContext.Response.Cookies.Append("refreshToken", result.Data!.RefreshToken!,
        //                    new CookieOptions
        //                    {
        //                        Expires = DateTimeOffset.Now.AddDays(7),
        //                        HttpOnly = true,
        //                        IsEssential = true,
        //                        Secure = true,
        //                        SameSite = SameSiteMode.None
        //                    });

        //                result.Data.RefreshToken = null;
        //            }

        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("token/refresh")]
        //public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel,
        //    [FromQuery] bool httpOnly = true)
        //{
        //    try
        //    {
        //        HttpContext.Request.Cookies.TryGetValue("refreshToken", out string? refreshTokenFromCookie);

        //        if (refreshTokenFromCookie != null && httpOnly)
        //        {
        //            refreshTokenModel.RefreshToken = refreshTokenFromCookie;
        //        }
        //        var command = new RefreshTokenCommand(refreshTokenModel);
        //        var result = await _mediator.Send(command);
        //        if (result.Status)
        //        {
        //            if (httpOnly)
        //            {
        //                HttpContext.Response.Cookies.Append("refreshToken", result.Data!.RefreshToken!,
        //                    new CookieOptions
        //                    {
        //                        Expires = DateTimeOffset.Now.AddDays(7),
        //                        HttpOnly = true,
        //                        IsEssential = true,
        //                        Secure = true,
        //                        SameSite = SameSiteMode.None
        //                    });

        //                result.Data.RefreshToken = null;
        //            }

        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpGet("email/verify")]
        //public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string verificationCode)
        //{
        //    try
        //    {
        //        var command = new AccountVerifyEmailCommand(email, verificationCode);
        //        var result = await _mediator.Send(command);
        //        if (result.Status)
        //        {
        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("email/resend-verification")]
        //public async Task<IActionResult> ResendVerificationEmail([FromBody] EmailModel emailModel)
        //{
        //    try
        //    {
        //        var command = new ResendVerificationEmailCommand(emailModel);
        //        var result = await _mediator.Send(command);
        //        if (result.Status)
        //        {
        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("password/change")]
        //[Authorize]
        //public async Task<IActionResult> ChangePassword(
        //    [FromBody] AccountChangePasswordModel accountChangePasswordModel)
        //{
        //    try
        //    {
        //        var command = new ChangePasswordCommand(accountChangePasswordModel);
        //        var result = await _mediator.Send(command);
        //        if (result.Status)
        //        {
        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("password/forgot")]
        //public async Task<IActionResult> ForgotPassword([FromBody] EmailModel emailModel)
        //{
        //    try
        //    {
        //        var command = new ForgotPasswordCommand(emailModel);
        //        var result = await _mediator.Send(command);
        //        if (result.Status)
        //        {
        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPost("password/reset")]
        //public async Task<IActionResult> ResetPassword([FromBody] AccountResetPasswordModel accountResetPasswordModel)
        //{
        //    try
        //    {
        //        var command = new ResetPasswordCommand(accountResetPasswordModel);
        //        var result = await _mediator.Send(command);
        //        if (result.Status)
        //        {
        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpGet("login/google")]
        //public async Task<IActionResult> LoginGoogle([FromQuery] string code, [FromQuery] bool httpOnly = true)
        //{
        //    try
        //    {
        //        var command = new LoginGoogleCommand(code);
        //        var result = await _mediator.Send(command);
        //        if (result.Status)
        //        {
        //            if (httpOnly)
        //            {
        //                HttpContext.Response.Cookies.Append("refreshToken", result.Data!.RefreshToken!,
        //                    new CookieOptions
        //                    {
        //                        Expires = DateTimeOffset.Now.AddDays(7),
        //                        HttpOnly = true,
        //                        IsEssential = true,
        //                        Secure = true,
        //                        SameSite = SameSiteMode.None
        //                    });

        //                result.Data.RefreshToken = null;
        //            }

        //            return Ok(result);
        //        }

        //        return BadRequest(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
    }
}
