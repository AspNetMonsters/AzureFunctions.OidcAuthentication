using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace AzureFunctions.OidcAuthentication
{
    internal interface IJwtSecurityTokenHandlerWrapper
    {
        ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters);
    }
}
