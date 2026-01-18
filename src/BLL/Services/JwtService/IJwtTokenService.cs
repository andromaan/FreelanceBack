using System.Security.Claims;
using BLL.ViewModels;
using BLL.ViewModels.Auth;
using Domain.Models.Auth.Users;
using Google.Apis.Auth;

namespace BLL.Services.JwtService;

public interface IJwtTokenService
{
    Task<JwtVm> GenerateTokensAsync(User user, CancellationToken token = default);
    ClaimsPrincipal GetPrincipals(string accessToken);
    Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginVm vm);
}