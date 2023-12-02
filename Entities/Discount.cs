using System.ComponentModel.DataAnnotations;

/// <summary>Holds the basic information about a discount</summary>
public record DiscountInformation
{
    /// <example>20</example>
    [Required]
    public double DiscountPercentage { get; set; }

    /// <example>2023-11-22T21:06:40.6855801+00:00</example>
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

/// <summary>Holds the discount information directly linked to order</summary>
public record OrderDiscount : DiscountInformation
{
    /// <example>15</example>
    [Required]
    public int OrderId { get; set; } // Link to the specific item
}

/// <summary>Holds the information on offers for loyalty programme</summary>
record LoyaltyOffer
{
    /// <example>15</example>
    [Required]
    public int OfferId { get; set; }

    /// <example>A free baguette</example>
    [Required]
    public string Description { get; set; }

    /// <example>550</example>
    [Required]
    public int PointsRequired { get; set; }

    /// <example>2023-11-22T21:06:40.6855801+00:00</example>
    [Required]
    public DateTimeOffset ValidUntil { get; set; }
}


/// <summary>Holds information on status of offer redemption</summary>
record RedeemLoyaltyOffer
{
    /// <example>5</example>
    [Required]
    public int UserId { get; set; }

    /// <example>15</example>
    [Required]
    public int OfferId { get; set; }

    /// <example>2023-11-22T21:06:40.6855801+00:00</example>
    public DateTimeOffset RedeemedOn { get; set; }
}
