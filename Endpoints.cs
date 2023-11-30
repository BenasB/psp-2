
using Microsoft.OpenApi.Models;

internal static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var cinematicGroup = app.MapGroup("cinematic").MapGroup("{companyId}");

        MapUsersEndpoints(cinematicGroup);
        MapOrdersEndpoints(cinematicGroup);
        MapServicesEndpoints(cinematicGroup);
        MapItemsEndpoints(cinematicGroup);
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
                Summary = "Create an order. This should also create ItemOrders for each of the specified item in the request",
            })
            .Accepts<OrderRequest>("application/json")
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

        ordersGroup.MapDelete("{orderId}", (string companyId, int orderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete an order",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        ordersGroup.MapPost("{orderId}/orderItems/{itemOrderId}", (string companyId, int orderId, int itemOrderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Add an additional item order to a specific order. This might be needed if a user wants to add something that they didn't think of when requesting the order initially",
            })
            .Accepts<OrderRequest>("application/json")
            .RequireAuth()
            .Produces<Order>(StatusCodes.Status201Created);

        ordersGroup.MapDelete("{orderId}/orderItems/{itemOrderId}", (string companyId, int orderId, int itemOrderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete an item order belonging to a specific order. This might be needed if a user changes up their mind and no longer wants this item order as part of their order.",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        var itemOrdersGroup = group.MapGroup("itemOrders")
            .WithTags("Orders");

        itemOrdersGroup.MapGet("", (string companyId, int itemOrderId, bool? onlyWithoutWorkers) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get item orders with a possibility to retrieve only the ones without any worker assigned.",
            })
            .Produces<IEnumerable<ItemOrder>>(StatusCodes.Status200OK)
            .RequireAuth();

        itemOrdersGroup.MapGet("{itemOrderId}", (string companyId, int itemOrderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific item order",
            })
            .Produces<ItemOrder>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status404NotFound);

        itemOrdersGroup.MapPut("{itemOrderId}/status", (string companyId, int itemOrderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Update the status of a specific item order",
            })
            .Accepts<ItemOrderStatus>("application/json")
            .Produces<ItemOrder>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        itemOrdersGroup.MapPut("{itemOrderId}/assign", (string companyId, int itemOrderId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Assign a worker a specific item order",
            })
            .Accepts<int>("application/json")
            .Produces<Order>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);
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
                Summary = "Sign in a user"
            })
            .Accepts<UserLoginInformation>("application/json")
            .Produces<SignInResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        usersGroup.MapPost("resetPassword", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Reset the current user's password",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status200OK);

        var rolesGroup = group.MapGroup("role")
            .WithTags("Users");

        rolesGroup.MapGet("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all roles",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<IEnumerable<Role>>(StatusCodes.Status200OK);

        rolesGroup.MapGet("{roleId}", (string companyId, string roleId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific role",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Role>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        rolesGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a role",
            })
            .Accepts<RoleInformation>("application/json")
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Role>(StatusCodes.Status201Created);

        rolesGroup.MapPut("{roleId}", (string companyId, int roleId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edit a role",
            })
            .Accepts<RoleInformation>("application/json")
            .Produces<Role>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        rolesGroup.MapDelete("{roleId}", (string companyId, int roleId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete a role",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static void MapServicesEndpoints(RouteGroupBuilder group)
    {
        var servicesGroup = group.MapGroup("services")
            .WithTags("Services");

        servicesGroup.MapGet("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all services",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<IEnumerable<Service>>(StatusCodes.Status200OK);

        servicesGroup.MapGet("{serviceId}", (string companyId, string serviceId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific service",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Service>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        servicesGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a service",
            })
            .Accepts<ServiceInformation>("application/json")
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Service>(StatusCodes.Status201Created);

        servicesGroup.MapPut("{serviceId}", (string companyId, int serviceId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edit a service",
            })
            .Accepts<ServiceInformation>("application/json")
            .Produces<Service>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        servicesGroup.MapDelete("{serviceId}", (string companyId, int serviceId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete a service",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        var appointmentsGroup = group.MapGroup("appointments")
            .WithTags("Services");

        appointmentsGroup.MapGet("", (string companyId, DateOnly? date) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all appointments with a possibility to show all appointments for a certain day",
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
                Summary = "Create an appointment. This is done by a manager/employee to create a \"time slot\" for a specific service.",
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
    }

    private static void MapItemsEndpoints(RouteGroupBuilder group)
    {
        var itemsGroup = group.MapGroup("items")
            .WithTags("Items");

        itemsGroup.MapGet("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all items",
            })
            .RequireAuth()

            .Produces<IEnumerable<Item>>(StatusCodes.Status200OK);

        itemsGroup.MapGet("{itemId}", (string companyId, string itemId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific item",
            })
            .RequireAuth()
            .Produces<Item>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        itemsGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a item",
            })
            .Accepts<ItemInformation>("application/json")
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Item>(StatusCodes.Status201Created);

        itemsGroup.MapPut("{itemId}", (string companyId, int itemId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edit a item",
            })
            .Accepts<ItemInformation>("application/json")
            .Produces<Item>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        itemsGroup.MapDelete("{itemId}", (string companyId, int itemId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete a item",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        var itemOptionsGroup = group.MapGroup("itemOptions")
            .WithTags("Items");

        itemOptionsGroup.MapGet("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all item options",
            })
            .RequireAuth()
            .Produces<IEnumerable<ItemOption>>(StatusCodes.Status200OK);

        itemOptionsGroup.MapGet("{itemOptionId}", (string companyId, string itemOptionId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get an item option",
            })
            .RequireAuth()
            .Produces<ItemOption>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        itemOptionsGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create an item option",
            })
            .Accepts<ItemOptionInformation>("application/json")
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<ItemOption>(StatusCodes.Status201Created);

        itemOptionsGroup.MapPut("{itemOptionId}", (string companyId, int itemOptionId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edit an item option",
            })
            .Accepts<ItemOptionInformation>("application/json")
            .Produces<ItemOption>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        itemOptionsGroup.MapDelete("{itemOptionId}", (string companyId, int itemOptionId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete an item option",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        var inventoryGroup = group.MapGroup("inventory")
            .WithTags("Items");

        inventoryGroup.MapGet("", (string companyId, int? item, int? store) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "List all inventories with a possibility to filter based on the item or the store",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<IEnumerable<Inventory>>(StatusCodes.Status200OK);

        inventoryGroup.MapGet("{inventoryId}", (string companyId, string inventoryId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a specific inventory",
            })
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Inventory>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        inventoryGroup.MapPost("", (string companyId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create an inventory. When a store wants to sell some item, it must create an inventory (for that store/item pair).",
            })
            .Accepts<InventoryCreationInformation>("application/json")
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces<Inventory>(StatusCodes.Status201Created);

        inventoryGroup.MapPut("{inventoryId}", (string companyId, int inventoryId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Edit an inventory",
            })
            .Accepts<InventoryInformation>("application/json")
            .Produces<Inventory>(StatusCodes.Status200OK)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

        inventoryGroup.MapDelete("{inventoryId}", (string companyId, int inventoryId) => Results.Ok())
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete an inventory",
            })
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuth()
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);
    }
}