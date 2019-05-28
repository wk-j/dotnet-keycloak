using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Connect22 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication(options => {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options => {
                    // http://localhost:5000/api/hello/hello
                    // "http://localhost:8080/auth/realms/master/protocol/openid-connect/auth";
                    // "http://localhost:8080/auth/realms/master/.well-known/openid-configuration/
                    options.Authority = "http://localhost:8080/auth/realms/master";
                    options.RequireHttpsMetadata = false;
                    options.ClientId = "hello";
                    //options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    options.SignedOutRedirectUri = "/sigin";

                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
