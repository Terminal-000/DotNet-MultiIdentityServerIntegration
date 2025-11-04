using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MultiIdentityServerIntegration.API
{
    /// <summary>
    /// Entry point for the MultiIdentityServerIntegration Web API.
    /// 
    /// This application demonstrates authentication using multiple Identity Servers
    /// by configuring two distinct JWT Bearer authentication schemes:
    /// 
    /// Schemes:
    /// - <b>ClientBearer</b>: Handles authentication for standard users through the
    ///   primary IdentityServer instance.
    /// - <b>InternalBearer</b>: Handles authentication for client applications or 
    ///   machine-to-machine (M2M) communication through a secondary IdentityServer endpoint.
    /// 
    /// This pattern is common in complex microservice architecture like banking applications
    /// 
    /// Each controller can be mapped to the appropriate Identity Server by applying 
    /// authorization policies. These policies link the controller’s required claims 
    /// to the corresponding JWT Bearer configuration.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ------------------------------------------------------------
            // Service Configuration
            // ------------------------------------------------------------

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ------------------------------------------------------------
            // Authentication Configuration
            // ------------------------------------------------------------

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("InternalBearer", options =>
                {
                    options.Authority = Environment.GetEnvironmentVariable("InternalIdentityServerBaseUrl");
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // You can fine-tune your validation settings here
                        ValidateIssuer = true,
                        ValidateAudience = false // Disable if your tokens are audience-agnostic
                    };
                })
                .AddJwtBearer("ClientBearer", options =>
                {
                    options.Authority = Environment.GetEnvironmentVariable("ClientIdentityServerBaseUrl");
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            // ------------------------------------------------------------
            // Mediator injection
            // ------------------------------------------------------------

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // ------------------------------------------------------------
            // Authorization Policies
            // ------------------------------------------------------------

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

            // ------------------------------------------------------------
            // Application Pipeline
            // ------------------------------------------------------------

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
