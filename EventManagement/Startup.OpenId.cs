using AspNet.Security.OpenIdConnect.Primitives;
using EventManagement.Core.Context;
using EventManagement.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement
{
    public partial class Startup
    {
        public void ConfigureOpenIddict(IServiceCollection services)
        {
            var authSettings = new AuthSettings();
            Configuration.Bind(nameof(AuthSettings), authSettings);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.SecretKey));

            services.Configure<IdentityOptions>(options => {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            var x509Certificate = new X509Certificate2(Path.Combine(
                          HostingEnvironment.ContentRootPath, "auth.pfx")
                      , "idsrv3test");

            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<EventMgtDbContext>();
                })
                .AddServer(options =>
                {
                    options.RegisterScopes(OpenIdConnectConstants.Scopes.Email,
                        OpenIdConnectConstants.Scopes.Profile,
                        OpenIdConnectConstants.Scopes.Address,
                        OpenIdConnectConstants.Scopes.Phone,
                        OpenIddictConstants.Scopes.Roles,
                        OpenIdConnectConstants.Scopes.OfflineAccess,
                        OpenIdConnectConstants.Scopes.OpenId
                    );


                    options.EnableTokenEndpoint("/api/connect/token")
                        .AllowRefreshTokenFlow()
                        .AcceptAnonymousClients()
                        .AllowPasswordFlow()
                        .SetAccessTokenLifetime(TimeSpan.FromMinutes(60))
                        .SetIdentityTokenLifetime(TimeSpan.FromMinutes(60))
                        .SetRefreshTokenLifetime(TimeSpan.FromMinutes(120))
                        .AddSigningCertificate(x509Certificate)
                        .UseJsonWebTokens();
                });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            services.AddAuthentication().AddJwtBearer(options => {
                options.Authority = options.Authority = authSettings.Authority;
                options.RequireHttpsMetadata = authSettings.RequireHttps;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = OpenIdConnectConstants.Claims.Name,
                    RoleClaimType = OpenIdConnectConstants.Claims.Role,
                    IssuerSigningKey = signingKey,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });
        }
    }
}
