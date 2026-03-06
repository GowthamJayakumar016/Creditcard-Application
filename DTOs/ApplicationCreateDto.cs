using System.ComponentModel.DataAnnotations;

namespace Creditcard.Application.Api.DTOs;

public class ApplicationCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string PanNumber { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Range(1, double.MaxValue)]
    public decimal AnnualIncome { get; set; }
}
