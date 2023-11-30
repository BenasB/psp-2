using Microsoft.OpenApi.Models;

public static class AuthExension
{
    public static RouteHandlerBuilder RequireAuth(this RouteHandlerBuilder builder) =>
        builder.WithOpenApi(operation => new(operation)
        {
            Security = new List<OpenApiSecurityRequirement>() {
                    new() {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    }
                },
        }).Produces(StatusCodes.Status401Unauthorized);
}