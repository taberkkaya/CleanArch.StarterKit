using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CleanArch.StarterKit.WebApi.Abstractions;

/// <summary>
/// Base API controller providing common configurations, such as authorization,
/// rate limiting, and MediatR support.
/// </summary>
[Authorize]
[ApiController]
[EnableRateLimiting("fixed")]
[Route("api/[controller]/[action]")]
public abstract class ApiController : ControllerBase
{
    /// <summary>
    /// The MediatR mediator instance.
    /// </summary>
    protected readonly IMediator _mediator;

    protected ApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
