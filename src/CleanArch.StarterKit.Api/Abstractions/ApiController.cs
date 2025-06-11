using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.StarterKit.Api.Abstractions;

/// <summary>
/// Serves as the base class for all API controllers, providing access to shared components such as MediatR.
/// </summary>
/// 
[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    private IMediator? _mediator;

    /// <summary>
    /// Gets the <see cref="IMediator"/> instance from the current request's service provider.
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
