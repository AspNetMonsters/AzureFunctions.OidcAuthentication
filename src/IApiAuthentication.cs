using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AzureFunctions.OidcAuthentication
{
    public interface IApiAuthentication
    {
        Task<ApiAuthenticationResult> AuthenticateAsync(IHeaderDictionary httpRequestHeaders);
        Task<HealthCheckResult> HealthCheckAsync();
    }
}