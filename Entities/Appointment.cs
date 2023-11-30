using System.ComponentModel.DataAnnotations;

record AppointmentInformation
{
    /// <example>43</example>
    [Required]
    public int ServiceId { get; set; }

    /// <example>16</example>
    [Required]
    public int StoreId { get; set; }

    // <example>2023-12-22T14:15:00.0000000+00:00</example>
    [Required]
    public DateTimeOffset StartDate { get; set; }

    // <example>2023-12-22T14:45:00.0000000+00:00</example>
    [Required]
    public DateTimeOffset EndDate { get; set; }
}

record Appointment : AppointmentInformation
{
    /// <example>65423</example>
    [Required]
    public int Id { get; set; }

    /// <example>1024</example>
    public int? OrderId { get; set; }

    /// <example>233</example>
    public int? WorkerId { get; set; }
}