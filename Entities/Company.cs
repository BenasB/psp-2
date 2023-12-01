using System.ComponentModel.DataAnnotations;

record CompanyInformation
{
    /// <example>SavorGroup International, Inc.</example>
    [Required]
    public string Name { get; set; }

    /// <example>info@savorgroup.com</example>
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

/// <summary>
/// Represents a legal entity that has made a deal with the PoS system and has been onboarded to the PoS system 
/// </summary>
record Company : CompanyInformation
{
    /// <example>3</example>
    [Required]
    public int Id { get; set; }
}