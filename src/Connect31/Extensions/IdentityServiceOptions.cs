using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Connect31 {
    public class IdentityServiceOptions {
        public string Authority { get; internal set; }
        public string ClientId { get; internal set; }
        public string ClientSecret { get; internal set; }
        public string CallbackPath { get; internal set; }
        public OpenIdConnectEvents Events { set; get; }
    }
}