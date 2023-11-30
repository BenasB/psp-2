
using Microsoft.OpenApi.Models;

internal static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var cinematicGroup = app.MapGroup("cinematic").MapGroup("{companyId}");

        MapUsersEndpoints(cinematicGroup);
        MapOrdersEndpoints(cinematicGroup);
    }

    private static void MapOrdersEndpoints(RouteGroupBuilder group)
    {
        var ordersGroup = group.MapGroup("orders")
            .WithTags("Orders");

        ordersGroup.MapGet("", (string companyId) => Results.Ok()) // TODO: filtering, sorting, pagination
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all orders",
            })
            .RequireAuth()
            .Produces<IEnumerable<Order>>(StatusCodes.Status200OK);

        ordersGroup.MapGet("{orderId}", (string companyId, int orderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific order",
            })
            .Produces<Order>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status404NotFound);

        ordersGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create an order",
            })
            .Accepts<OrderInformation>("application/json")
            .RequireAuth()
            .Produces<Order>(StatusCodes.Status201Created);

        ordersGroup.MapPut("{orderId}", (string companyId, int orderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Update an order",
            })
            .Accepts<OrderInformation>("application/json")
            .Produces<Order>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status404NotFound);

        ordersGroup.MapPut("{orderId}/status", (string companyId, int orderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Update the status of an order",
            })
            .Accepts<OrderStatus>("application/json")
            .Produces<Order>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden);

        ordersGroup.MapPut("{orderId}/assign", (string companyId, int orderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Assign an employee to an order",
            })
            .Accepts<int>("application/json")
            .Produces<Order>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden);
    }

    public static void MapUsersEndpoints(RouteGroupBuilder group)
    {
        var usersGroup = group.MapGroup("users")
            .WithTags("Users");

        usersGroup.MapGet("", (string companyId, string? role) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all users",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<IEnumerable<User>>(StatusCodes.Status200OK);

        usersGroup.MapGet("{userId}", (string companyId, int userId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific user",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<User>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        usersGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a user",
            })
            .Accepts<UserInformation>("application/json")
            .RequireAuth()
            .Produces<User>(StatusCodes.Status201Created);

        usersGroup.MapPut("{userId}", (string companyId, int userId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edit a user",
            })
            .Accepts<UserInformation>("application/json")
            .Produces<User>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        usersGroup.MapDelete("{userId}", (string companyId, int userId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Remove a user"
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        usersGroup.MapPost("signIn", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Sign in a user and receive a token",
                Security = null
            })
            .Accepts<UserLoginInformation>("application/json")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        usersGroup.MapPost("resetPassword", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Reset the current user's password",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status200OK);
    }
}