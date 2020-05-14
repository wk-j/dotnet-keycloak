using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Connect31 {
    public static class ServiceExtensions {
        public static void AddIdentityService(this IServiceCollection services, IdentityServiceOptions opt) {
            services
              .AddAuthentication(options => {
                  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
              })
              .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            //   .AddJwtBearer(cfg => {
            //       cfg.RequireHttpsMetadata = false;
            //       cfg.Authority = opt.Authority;
            //       cfg.IncludeErrorDetails = true;
            //       cfg.TokenValidationParameters =
            //           new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
            //               ValidateAudience = false,
            //               ValidateIssuerSigningKey = true,
            //               ValidateIssuer = true,
            //               ValidIssuer = opt.Authority,
            //               ValidateLifetime = true
            //           };

            //       cfg.Events = new JwtBearerEvents();
            //       cfg.Events.OnAuthenticationFailed += (c) => {
            //           c.NoResult();
            //           c.Response.StatusCode = 401;
            //           c.Response.ContentType = "text/plain";
            //           return c.Response.WriteAsync(c.Exception.ToString());
            //       };
            //   }
            // )
          .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) => {
              options.Authority = opt.Authority;
              options.ClientId = opt.ClientId;
              options.ClientSecret = opt.ClientSecret;
              options.CallbackPath = new PathString(opt.CallbackPath);
              options.ResponseType = OpenIdConnectResponseType.CodeIdTokenToken;
              options.RequireHttpsMetadata = false;
              options.SaveTokens = true;
              options.Events = opt.Events;
              options.GetClaimsFromUserInfoEndpoint = true;
          });
        }
    }
}
