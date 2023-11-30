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

    /// <example>34</example>
    [Required]
    public int InventoryId { get; set; }
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