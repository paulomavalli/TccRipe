using System.Linq;
using System.Security.Claims;

namespace RIPE.CrossCutting.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetCustomerId(this ClaimsPrincipal claimsPrincipal)
                    => claimsPrincipal.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name)?.Value;

        public static string GetGivenName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.FirstOrDefault(a => a.Type == ClaimTypes.GivenName)?.Value;

        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
                    => claimsPrincipal.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name)?.Value;

    }
}