namespace Creditcard.Application.Api.DTOs;

public class ApplicationResponseDto
{
    public string ApplicationNumber { get; set; } = string.Empty;
    public DateTime AppliedDate { get; set; }
    public int CreditScore { get; set; }
    public decimal? CreditLimit { get; set; }
    public string Status { get; set; } = string.Empty;
}
