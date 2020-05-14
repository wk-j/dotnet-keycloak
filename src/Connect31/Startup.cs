using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Connect31 {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddIdentityService(new IdentityServiceOptions {
                Authority = "https://keycloak.bcecm.com:8443/auth/realms/master",
                ClientId = "dotnet",
                ClientSecret = "d0b074b0-7914-4b89-a2da-657d19409951",
                CallbackPath = new PathString("/login"),
                Events = new OpenIdConnectEvents {
                    OnTokenResponseReceived = context => {
                        var ticket = context.ProtocolMessage.AccessToken;
                        // var json = JsonConvert.SerializeObject(context.ProtocolMessage, Formatting.Indented);
                        // Console.WriteLine(json);
                        Console.WriteLine(ticket);
                        return Task.CompletedTask;
                    }
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
