using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// Specifies generic order information
/// </summary>
record OrderInformation
{
    /// <example>3</example>
    [Required]
    public int PaymentMethodId { get; set; }

    /// <example>1.5</example>
    /// <summary>How much the user is willing to leave as a tip</summary>
    [Required]
    [DataType(DataType.Currency)]
    public double Gratuity { get; set; }

    /// <example>[33]</example>
    /// <summary>List of appointment IDs</summary>
    [Required]
    public IEnumerable<int> Appointments { get; set; }
}

/// <summary>
/// Alongside generic order information, also specifies items that should be included in the order when creating it
/// </summary>
record OrderRequest : OrderInformation
{
    /// <summary>
    /// ItemOrders for this Order should be generated from these items
    /// </summary>
    [Required]
    public IEnumerable<ItemOrderInformation> Items { get; set; }
}

/// <summary>
/// Models an existing Order (one that has already been requested)
/// </summary>
record Order : OrderInformation
{
    /// <example>1024</example>
    [Required]
    public int Id { get; set; }

    /// <example>233</example>
    /// <summary>Specifies the employee that administers the Order</summary>
    public int? WorkerId { get; set; }

    /// <example>84354</example>
    [Required]
    public int CustomerId { get; set; }

    /// <example>[46434]</example>
    /// <summary>Specifies the ItemOrders of this Order</summary>
    [Required]
    public IEnumerable<int> ItemOrders { get; set; }

    /// <example>14.58</example>
    /// <summary>This value is derived from the prices of Items and Appointments</summary>
    [Required]
    [DataType(DataType.Currency)]
    public double TotalAmount { get; set; }

    // <example>2023-11-21T18:06:40.6433801+00:00</example>
    [Required]
    public DateTimeOffset OrderDate { get; set; }

    /// <example>3.45</example>
    /// <summary>This value is summed up from the taxes of Items and Appointments</summary>
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

record ItemOrderInformation
{
    /// <example>15</example>
    [Required]
    public int ItemId { get; set; }

    /// <example>[63]</example>
    public IEnumerable<int>? ItemOptions { get; set; }
}

record ItemOrder : ItemOrderInformation
{
    /// <example>46434</example>
    [Required]
    public int Id { get; set; }

    [Required]
    public ItemOrderStatus Status { get; set; }

    /// <example>72</example>
    /// <summary>Specifies the employee that takes care of this particular ItemOrder, e.g. a chef</summary>
    public int? WorkerId { get; set; }
}


[JsonConverter(typeof(JsonStringEnumConverter))]
enum ItemOrderStatus
{
    Placed, Preparing, Done, Delivered
}