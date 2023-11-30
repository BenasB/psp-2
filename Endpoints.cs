
using Microsoft.OpenApi.Models;

internal static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var cinematicGroup = app.MapGroup("cinematic").MapGroup("{companyId}");

        MapUsersEndpoints(cinematicGroup);
        MapOrdersEndpoints(cinematicGroup);
        MapAppointmentsEndpoints(cinematicGroup);
        MapProductsEndpoints(cinematicGroup);
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

        ordersGroup.MapPost("{orderId}/assign", (string companyId, int orderId) => Results.Ok())
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

    private static void MapAppointmentsEndpoints(RouteGroupBuilder group)
    {
        var appointmentsGroup = group.MapGroup("appointments")
            .WithTags("Appointments");

        appointmentsGroup.MapGet("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all appointments",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<IEnumerable<Appointment>>(StatusCodes.Status200OK);

        appointmentsGroup.MapGet("{appointmentId}", (string companyId, string appointmentId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific appointment",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Appointment>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        appointmentsGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create an appointment",
            })
            .Accepts<AppointmentInformation>("application/json")
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Appointment>(StatusCodes.Status201Created);

        appointmentsGroup.MapPut("{appointmentId}", (string companyId, int appointmentId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edit an appointment",
            })
            .Accepts<AppointmentInformation>("application/json")
            .Produces<Appointment>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        appointmentsGroup.MapDelete("{appointmentId}", (string companyId, int appointmentId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete an appointment",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        appointmentsGroup.MapPost("{appointmentId}/assign", (string companyId, int appointmentId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Assign an employee to an appointment",
            })
            .Accepts<int>("application/json")
            .Produces<Appointment>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden);
    }

    private static void MapProductsEndpoints(RouteGroupBuilder group)
    {
        var productsGroup = group.MapGroup("products")
            .WithTags("Products");

        productsGroup.MapGet("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all products",
            })
            .RequireAuth()

            .Produces<IEnumerable<Item>>(StatusCodes.Status200OK);

        productsGroup.MapGet("{productId}", (string companyId, string productId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific product",
            })
            .RequireAuth()
            .Produces<Item>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        productsGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a product",
            })
            .Accepts<ItemInformation>("application/json")
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Item>(StatusCodes.Status201Created);

        productsGroup.MapPut("{productId}", (string companyId, int productId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edit a product",
            })
            .Accepts<ItemInformation>("application/json")
            .Produces<Item>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        productsGroup.MapDelete("{productId}", (string companyId, int productId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete a product",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);
    }
}