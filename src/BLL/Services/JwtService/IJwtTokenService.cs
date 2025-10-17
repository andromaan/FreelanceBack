using System.Security.Claims;
using Domain.Models.Auth.Users;
using Domain.ViewModels;
using Domain.ViewModels.Auth;
using Google.Apis.Auth;

namespace BLL.Services.JwtService;

public interface IJwtTokenService
{
    Task<JwtVm> GenerateTokensAsync(User user, CancellationToken token = default);
    ClaimsPrincipal GetPrincipals(string accessToken);
    Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginVm vm);
}