using System.Security.Claims;
using Domain.Models.Auth.Users;
using Domain.ViewModels;
using Google.Apis.Auth;

namespace BLL.Services.JwtService;

public interface IJwtTokenService
{
    Task<JwtModel> GenerateTokensAsync(User user, CancellationToken token = default);
    ClaimsPrincipal GetPrincipals(string accessToken);
    Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginModel model);
    string GenerateEmailConfirmationToken(User user, int minutes = 30);
}