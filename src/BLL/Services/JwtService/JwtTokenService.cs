using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BLL.Common.Interfaces.Repositories;
using Domain.Models.Auth;
using Domain.Models.Auth.Users;
using Domain.ViewModels;
using Domain.ViewModels.Auth;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services.JwtService;

public class JwtTokenService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
    : IJwtTokenService
{
    private JwtSecurityToken GenerateAccessToken(User user)
    {
        var issuer = configuration["AuthSettings:issuer"];
        var audience = configuration["AuthSettings:audience"];
        var keyString = configuration["AuthSettings:key"];
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString!));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("id", user.Id.ToString()),
            new("email", user.Email),
            new("role", user.RoleId)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private string GenerateRefreshToken()
    {
        var bytes = new byte[32];

        using (var rnd = RandomNumberGenerator.Create())
        {
            rnd.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }

    private async Task<RefreshToken?> SaveRefreshTokenAsync(User userEntity, string refreshToken, string jwtId,
        CancellationToken cancellationToken)
    {
        var model = new RefreshToken
        {
            Id = Guid.NewGuid().ToString(),
            Token = refreshToken,
            JwtId = jwtId,
            CreateDate = DateTime.UtcNow,
            ExpiredDate = DateTime.UtcNow.AddDays(7),
            UserId = userEntity.Id
        };

        try
        {
            var tokenEntity = await refreshTokenRepository.CreateAsync(model, cancellationToken);
            return tokenEntity;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public ClaimsPrincipal GetPrincipals(string accessToken)
    {
        var jwtSecurityKey = configuration["AuthSettings:key"];

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principals = tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
        {
            throw new SecurityTokenException("Invalid access token");
        }

        return principals;
    }

    public async Task<JwtVm> GenerateTokensAsync(User user, CancellationToken token)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        await refreshTokenRepository.MakeAllRefreshTokensExpiredForUser(user.Id, token);

        var saveResult = await SaveRefreshTokenAsync(user, refreshToken, accessToken.Id, token);

        var tokens = new JwtVm
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken = refreshToken
        };

        return tokens;
    }

    public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginVm vm)
    {
        string clientId = configuration["GoogleAuthSettings:ClientId"]!;
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string> { clientId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(vm.Token, settings);
        return payload;
    }
}