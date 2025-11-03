using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MultiIdentityServerIntegration.API
{
    /// <summary>
    /// Program entry point for the MultiIdentityServerIntegration Web API.
    /// 
    /// This Web API supports authentication using two separate JWT Bearer schemes:
    /// 1. "InternalBearer": Used for standard users authenticated via the primary IdentityServer.
    /// 2. "ClientBearer": Used for machine-to-machine or client applications authenticated
    ///    via a separate IdentityServer endpoint.
    /// 
    /// To tell tje controllers in the application which Identity server to use for granting access we would need to add  
    /// 
    /// Each scheme has its own Authority URL and token validation configuration.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("InternalBearer", options =>
            {
                options.Authority = Environment.GetEnvironmentVariable("InternalIdentityServerBaseUrl");
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false

                };
            })
            .AddJwtBearer("ClientBearer", options =>
            {
                options.Authority = Environment.GetEnvironmentVariable("ClientIdentityServerBaseUrl");
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false

                };
            });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("InternalScope", policy =>
                {
                    policy.AddAuthenticationSchemes("InternalBearer");
                    policy.RequireClaim("scope", "AllowedClaimValue");
                });

                options.AddPolicy("ClientScope", policy =>
                {
                    policy.AddAuthenticationSchemes("ClientBearer");
                    policy.RequireClaim("client_id", "AllowedClaimValue");
                });
            });

            var app = builder.Build();

            // Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.Run();
        }
    }
}
