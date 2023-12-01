using System.ComponentModel.DataAnnotations;

record ItemInformation
{
    /// <example>Super delicious hot dog</example>
    [Required]
    public string Name { get; set; }

    /// <example>3.99</example>
    [Required]
    [DataType(DataType.Currency)]
    public double Price { get; set; }

    /// <example>Savor the symphony of flavors with our Super Delicious Hot Dog – a perfect union of grilled, premium sausage, golden toasted bun, and a medley of condiments. Elevate your hot dog experience with our signature toppings, from melted cheese to crispy bacon, creating a culinary masterpiece that promises to delight your taste buds on every bite.</example>
    [Required]
    public string Description { get; set; }
}

record Item : ItemInformation
{
    /// <example>15</example>
    [Required]
    public int Id { get; set; }

    /// <example>0.84</example>
    [Required]
    [DataType(DataType.Currency)]
    public double Tax { get; set; }
}

record ItemOptionInformation
{
    /// <example>15</example>
    [Required]
    public string ItemId { get; set; }

    /// <example>Extra onions</example>
    [Required]
    public string Name { get; set; }

    /// <example>0.50</example>
    [Required]
    [DataType(DataType.Currency)]
    public double Price { get; set; }
}

/// <summary>
/// Specifies an "extra" that you can add on to an item
/// </summary>
record ItemOption : ItemOptionInformation
{
    /// <example>63</example>
    [Required]
    public int Id { get; set; }

    /// <example>0.10</example>
    [Required]
    [DataType(DataType.Currency)]
    public double Tax { get; set; }
}

record InventoryInformation
{
    /// <example>78</example>
    [Required]
    public int Amount { get; set; }

    /// <example>15</example>
    [Required]
    public int LowStockThreshold { get; set; }
}

record InventoryCreationInformation : InventoryInformation
{
    /// <example>15</example>
    [Required]
    public int ItemId { get; set; }

    /// <example>18</example>
    [Required]
    public int StoreId { get; set; }
}

/// <summary>Represents the physical amount of a specific item in a specific store</summary>
record Inventory : InventoryCreationInformation
{
    /// <example>684</example>
    [Required]
    public int Id { get; set; }
}