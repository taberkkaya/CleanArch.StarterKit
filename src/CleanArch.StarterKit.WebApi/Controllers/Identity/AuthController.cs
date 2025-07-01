using CleanArch.StarterKit.Application.Features.Identity.Auth;
using CleanArch.StarterKit.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller providing authentication and identity-related endpoints.
/// </summary>
public class AuthController : ApiController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Confirms a user's email using a confirmation token.
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, string token, CancellationToken cancellationToken)
    {
        ConfirmEmailCommand request = new(Guid.Parse(userId), token);
        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsSuccess)
            return Ok("Email has been confirmed successfully.");

        return BadRequest(result.Error?.Message ?? "Email could not be confirmed.");
    }

    /// <summary>
    /// Sends an email confirmation link to the user.
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SendConfirmEmail(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Sends a password reset email.
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SendResetPasswordEmail([FromBody] SendResetPasswordEmailCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess
            ? Ok("Password reset email has been sent successfully.")
            : BadRequest(result.Error?.Message);
    }

    /// <summary>
    /// Resets the user's password using the provided token.
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess
            ? Ok("Password has been reset successfully.")
            : BadRequest(result.Error?.Message);
    }
}
