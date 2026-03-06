namespace Creditcard.Application.Api.DTOs;

public class AdminApplicationResponseDto
{
    public string ApplicationNumber { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PanNumber { get; set; } = string.Empty;
    public decimal AnnualIncome { get; set; }
    public int CreditScore { get; set; }
    public decimal? CreditLimit { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime AppliedDate { get; set; }
}
