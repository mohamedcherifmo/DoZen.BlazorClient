using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace DoZen.BlazorClient.Data
{
    public class ApplicationAuthenticationState : RemoteAuthenticationState
    {
        public string Id { get; set; }
    }
}
