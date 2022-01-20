using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManager.AuthHandler
{
    // Swagger IOperationFilter implementation that will decide which api action needs authorization
    internal class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check for authorize attribute
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<ApiKeyAuthAttribute>()
                .Any();

            if (hasAuthorize)
            {

                var securityRequirement = new OpenApiSecurityRequirement()
                {
                    {

                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "API Key"
                            },
                            Scheme = "API Key",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Description = "Standard Authorization header using API key",
                            Type = SecuritySchemeType.ApiKey,
                        },
                        new List<string>()
                    }
                };

                operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            }
        }
    }
}