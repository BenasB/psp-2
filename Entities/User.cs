using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// Used to log in the user to gain a token
/// </summary>
record UserLoginInformation
{
    /// <example>john.d@gmail.com</example>
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    /// <example>superStrongPassword123</example>
    [Required]
    public string Password { get; set; }
}

record UserInformation : UserLoginInformation
{
    /// <example>John</example>
    [Required]
    public string FirstName { get; set; }

    /// <example>Doe</example>
    [Required]
    public string LastName { get; set; }

    /// <example>+37061234432</example>
    [Phone]
    [Required]
    public string Phone { get; set; }

    /// <example>Verkiu g. 1, Vilnius</example>
    [Required]
    public string Address { get; set; }

    /// <example>[4]</example>
    [Required]
    public IEnumerable<int> Roles { get; set; }
}

record User : UserInformation
{
    /// <example>42</example>
    [Required]
    public int Id { get; set; }

    // <example>2023-11-22T21:06:40.6855801+00:00</example>
    [Required]
    public DateTimeOffset CreateTime { get; set; }
}

record SignInResponse
{
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</example>
    /// <summary>JWT token (base64 encoded string). Alongside information about the user (name, email, id), it should also contain their roles.</summary>
    [Required]
    public string Token { get; set; }
}

record RoleInformation
{
    /// <example>Manager</example>
    [Required]
    public string Name { get; set; }

    /// <example>[ "UsersRead", "UsersManage", "InventoryRead", "InventoryManage", "ServicesRead", "ServicesManage", "ItemsRead", "ItemsManage", "PaymentsRead", "PaymentsManage" ]</example>
    [Required]
    public IEnumerable<Permissions> Permissions { get; set; }
}

record Role : RoleInformation
{
    /// <example>4</example>
    [Required]
    public int Id { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
enum Permissions
{
    UsersRead, UsersManage, RolesRead, RolesManage, LoyaltyRead, LoyaltyManage, OrdersRead, OrdersManage, ServicesRead, ServicesManage, ItemsRead, ItemsManage, InventoryRead, InventoryManage, PaymentsRead, PaymentsManage, StoresRead, StoresManage
}