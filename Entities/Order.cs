using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

record OrderInformation
{
    /// <example>3</example>
    [Required]
    public int PaymentMethodId { get; set; }

    /// <example>1.5</example>
    [Required]
    [DataType(DataType.Currency)]
    public double Gratuity { get; set; }

    [Required]
    public IEnumerable<ItemOrder> Products { get; set; }

    /// <example>[33]</example>
    [Required]
    public IEnumerable<int> Appointments { get; set; }
}

record Order : OrderInformation
{
    /// <example>1024</example>
    [Required]
    public int Id { get; set; }

    /// <example>233</example>
    public int? WorkerId { get; set; }

    /// <example>84354</example>
    [Required]
    public int CustomerId { get; set; }

    /// <example>14.58</example>
    [Required]
    [DataType(DataType.Currency)]
    public double TotalAmount { get; set; }

    // <example>2023-11-21T18:06:40.6433801+00:00</example>
    [Required]
    public DateTimeOffset OrderDate { get; set; }

    /// <example>3.45</example>
    [Required]
    [DataType(DataType.Currency)]
    public double Tax { get; set; }

    [Required]
    public OrderStatus Status { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
enum OrderStatus
{
    Placed, Pending, Done
}

class ItemOrder
{
    /// <example>15</example>
    [Required]
    public int ItemId { get; set; }

    /// <example>1</example>
    [Required]
    public int Amount { get; set; }
}