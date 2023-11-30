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

    /// <example>233</example>
    /// <summary>Specifies the worker that will be fulfilling this appointment, e. g. masseuse, barber</summary>
    [Required]
    public int WorkerId { get; set; }
}

/// <summary>
/// Acts as a time slot for a specific service.
/// </summary>
record Appointment : AppointmentInformation
{
    /// <example>33</example>
    [Required]
    public int Id { get; set; }

    /// <example>1024</example>
    /// <summary>If there is no OrderId specified, this appointment (time slot) should be considered unoccupied</summary>
    public int? OrderId { get; set; }
}