using CleanArch.StarterKit.Application.Features.Identity.Roles;
using CleanArch.StarterKit.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.StarterKit.WebApi.Controllers.Identity;

/// <summary>
/// Controller providing endpoints for managing application roles.
/// </summary>
public class RolesController : ApiController
{
    public RolesController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves all roles.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Update(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Deletes a role by its identifier.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteByIdRoleCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
