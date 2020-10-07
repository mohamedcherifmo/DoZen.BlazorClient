using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoZen.BlazorClient.Data
{
    public class OfflineAccountClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        private readonly IServiceProvider services;

        public OfflineAccountClaimsPrincipalFactory(IServiceProvider services, IAccessTokenProviderAccessor accessor) : base(accessor)
        {
            this.services = services;
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            Console.WriteLine("Entered Create User Async");
            var localDoZenStore = services.GetRequiredService<LocalDoZenStore>();

            var result = await base.CreateUserAsync(account, options);
            if (result.Identity.IsAuthenticated)
            {
                Console.WriteLine("Entered Create User Async:  Is Authenticated");
                await localDoZenStore.SaveUserAccountAsync(result);
            }
            else
            {
                Console.WriteLine("Entered Create User Async:  Else");
                result = await localDoZenStore.LoadUserAccountAsync();
            }
            Console.WriteLine("Entered Create User Async:  Returning Result");
            return result;
        }
    }
}
