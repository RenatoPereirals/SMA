namespace StockSimulator.API.Entities;

public class Market(List<Stock> stocks)
{
    private readonly List<Stock> _stocks = stocks ?? [];
    public IReadOnlyList<Stock> Stocks => _stocks.AsReadOnly();
}
