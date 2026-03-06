using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Creditcard.Application.Api.Models;

public class Application
{
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string ApplicationNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string PanNumber { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal AnnualIncome { get; set; }

    public int CreditScore { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? CreditLimit { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = "Pending";

    public DateTime AppliedDate { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}
