using System.ComponentModel.DataAnnotations;

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