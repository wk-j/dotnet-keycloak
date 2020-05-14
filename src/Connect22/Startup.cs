using System;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Connect22 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication(options => {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options => {
                options.Authority = "http://localhost:8080/auth/realms/master";
                options.RequireHttpsMetadata = false;
                options.ClientId = "hello";
                options.ClientSecret = "3e95fbf4-09e7-4f28-bfdc-af567a7067a6";
                options.ResponseType = OpenIdConnectResponseType.CodeIdTokenToken;
                options.SignedOutRedirectUri = "/signin-oidc";
                options.Events = new OpenIdConnectEvents {
                    OnTicketReceived = async x => {
                        Console.WriteLine(x.Response);
                    },
                    OnUserInformationReceived = async x => {
                        Console.WriteLine(x.Response);
                    }
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
