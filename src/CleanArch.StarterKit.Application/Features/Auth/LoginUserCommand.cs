using MediatR;
using ResultKit;

namespace CleanArch.StarterKit.Application.Features.Auth;
public sealed record LoginUserCommand(
    string Email,
    string Password) : IRequest<Result<string>>;
