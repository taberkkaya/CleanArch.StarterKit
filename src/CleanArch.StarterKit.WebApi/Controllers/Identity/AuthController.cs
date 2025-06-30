using CleanArch.StarterKit.Application.Features.Identity.Auth;
using CleanArch.StarterKit.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AuthController : ApiController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, string token, CancellationToken cancellationToken)
    {
        ConfirmEmailCommand request = new(Guid.Parse(userId), token);
        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsSuccess)
            return Ok("E-posta doğrulandı!");

        return BadRequest(result.Error?.Message ?? "E-posta doğrulanamadı.");
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SendConfirmEmail(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("forgotpassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ResetPasswordRequestCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok("Mail gönderildi!") : BadRequest(result.Error?.Message);
    }

    [AllowAnonymous]
    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok("Şifre sıfırlandı!") : BadRequest(result.Error?.Message);
    }

}
