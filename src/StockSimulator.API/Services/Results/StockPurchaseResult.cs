namespace StockSimulator.API.Services.Results;

public class StockPurchaseResult(bool isSuccessful, string message, int availableQuantity)
{
    public bool IsSuccessful { get; set; } = isSuccessful;
    public string Message { get; set; } = message;
    public int AvailableQuantity { get; set; } = availableQuantity;
}
