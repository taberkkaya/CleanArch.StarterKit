using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CleanArch.StarterKit.WebApi.Abstractions;

[Authorize]
[ApiController]
[EnableRateLimiting("fixed")]
[Route("api/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
    public readonly IMediator _mediator;

    public ApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
