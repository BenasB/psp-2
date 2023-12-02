using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
enum PaymentMethod
{
    Cash, CreditCard, GiftCard
}

[JsonConverter(typeof(JsonStringEnumConverter))]
enum PaymentStatus
{
    Completed, Failed, Pending
}

record PaymentInformation
{
    /// <example>5</example>
    [Required]
    public int OrderID { get; set; }

    /// <example>15.55</example>
    [Required]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    [Required]
    public PaymentMethod PaymentMethod { get; set; }

}

record Payment : PaymentInformation
{
    /// <example>10</example>
    [Required]
    public int PaymentId { get; set; }

    /// <example>2023-11-21T18:06:40.6433801+00:00</example>
    [Required]
    public DateTimeOffset PaymentDate { get; set; }

    [Required]
    public PaymentStatus Status { get; set; }
}

class Receipt
{
    /// <example>5</example>
    [Required]
    public int ReceiptId { get; set; }

    /// <example>2023-11-21T18:06:40.6433801+00:00</example>
    [Required]
    public DateTimeOffset DateIssued { get; set; }

    public string Details { get; set; } // e.g. itemized list of products/appointments

    /// <example>21.47</example>
    [Required]
    [DataType(DataType.Currency)]
    public decimal TotalAmount { get; set; }
}

class Refund
{
    /// <example>2</example>
    [Required]
    public int RefundId { get; set; }

    /// <example>10</example>
    [Required]
    public int PaymentId { get; set; }

    /// <example>2023-11-21T18:06:40.6433801+00:00</example>
    [Required]
    public DateTimeOffset RefundDate { get; set; }

    /// <example>12</example>
    [DataType(DataType.Currency)]
    public decimal RefundAmount { get; set; }

    /// <example>Bland food</example>
    public string Reason { get; set; } // Reason for the refund
}