using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a physical store location
/// </summary>
record StoreInformation
{
    /// <example>Mingle In Flavor</example>
    [Required]
    public string Name { get; set; }

    /// <example>Didlauko g. 47, Vilnius</example>
    [Required]
    public string Address { get; set; }
}

record Store : StoreInformation
{
    /// <example>18</example>
    [Required]
    public int Id { get; set; }
}