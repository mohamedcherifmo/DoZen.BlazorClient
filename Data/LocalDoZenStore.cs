using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace DoZen.BlazorClient.Data
{
    public class LocalDoZenStore
    {
        private readonly HttpClient httpClient;
        private readonly IJSRuntime js;

        public LocalDoZenStore(HttpClient httpClient, IJSRuntime js)
        {
            this.httpClient = httpClient;
            this.js = js;
        }

        public ValueTask SaveUserAccountAsync(ClaimsPrincipal user)
        {
            Console.WriteLine("Entered Sve User Account Async");
            return user != null
                ? PutAsync("metadata", "userAccount", user.Claims.Select(c => new ClaimData { Type = c.Type, Value = c.Value }))
                : DeleteAsync("metadata", "userAccount");
        }
        public async Task<ClaimsPrincipal> LoadUserAccountAsync()
        {
            var storedClaims = await GetAsync<ClaimData[]>("metadata", "userAccount");
            return storedClaims != null
                ? new ClaimsPrincipal(new ClaimsIdentity(storedClaims.Select(c => new Claim(c.Type, c.Value)), "appAuth"))
                : new ClaimsPrincipal(new ClaimsIdentity());
        }
        ValueTask<T> GetAsync<T>(string storeName, object key)
           => js.InvokeAsync<T>("localDoZenStore.get", storeName, key);
        ValueTask PutAsync<T>(string storeName, object key, T value)
            => js.InvokeVoidAsync("localDoZenStore.put", storeName, key, value);
        ValueTask DeleteAsync(string storeName, object key)
            => js.InvokeVoidAsync("localDoZenStore.delete", storeName, key);
        class ClaimData
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }
    }
}
