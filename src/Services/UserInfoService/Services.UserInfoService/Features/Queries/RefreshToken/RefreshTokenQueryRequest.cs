using MediatR;

namespace Services.UserInfoService.Features.Queries.RefreshToken
{
    public record RefreshTokenQueryRequest(
        string token
    ) : IRequest<RefreshTokenQueryResponse>;
}
