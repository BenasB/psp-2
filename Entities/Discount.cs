using System.ComponentModel.DataAnnotations;

/// <summary>Holds the basic information about a discount</summary>
public record DiscountInformation
{
    /// <example>20</example>
    [Required]
    public double DiscountPercentage { get; set; }

    /// <example>2023-12-31</example>
    public DateTime? ValidUntil { get; set; }

    /// <example>Minimum purchase of $10</example>
    public string Conditions { get; set; }
}

/// <summary>Holds the discount information directly linked to item</summary>
public record ItemDiscount : DiscountInformation
{
    /// <example>15</example>
    [Required]
    public int ItemId { get; set; } // Link to the specific item
}

/// <summary>Holds the discount information directly linked to service</summary>
public record ServiceDiscount : DiscountInformation
{
    /// <example>15</example>
    [Required]
    public int ServiceId { get; set; } // Link to the specific item
}