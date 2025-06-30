using CleanArch.StarterKit.Application.Features.Auth;
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
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

}
