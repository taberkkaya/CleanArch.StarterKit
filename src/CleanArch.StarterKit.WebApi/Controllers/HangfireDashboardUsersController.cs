using CleanArch.StarterKit.Application.Features.HangfireDashboardUsersRepository;
using CleanArch.StarterKit.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.StarterKit.WebApi.Controllers;

public class HangfireDashboardUsersController : ApiController
{
    public HangfireDashboardUsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateHangfireDashboardUsersRepositoryCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
