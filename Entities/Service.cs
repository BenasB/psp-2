using System.ComponentModel.DataAnnotations;

record ServiceInformation
{
    /// <example>Children (up to 14) haircut</example>
    [Required]
    public string Name { get; set; }

    /// <example>3.99</example>
    [Required]
    [DataType(DataType.Currency)]
    public double Price { get; set; }

    /// <example>Our skilled stylists create a welcoming atmosphere, ensuring a comfortable and enjoyable grooming session. With attention to detail and a playful touch, we craft trendy and age-appropriate hairstyles, making every visit a delightful adventure for your child.</example>
    [Required]
    public string Description { get; set; }
}

record Service : ServiceInformation
{
    /// <example>43</example>
    [Required]
    public int Id { get; set; }

    /// <example>0.84</example>
    [Required]
    [DataType(DataType.Currency)]
    public double Tax { get; set; }
}

record AppointmentInformation
{
    /// <example>43</example>
    [Required]
    public int ServiceId { get; set; }

    /// <example>18</example>
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