using System.Security.Claims;

namespace DoZen.BlazorClient.Data
{
    public static class UserExtensions
    {
        public static string Name(this ClaimsPrincipal user)
            => user.FindFirst("name").Value;

        public static string Email(this ClaimsPrincipal user)
            => user.Identity.Name;
    }
}
