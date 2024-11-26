
using StockSimulator.API.Entities;

namespace StockSimulator.API.DataStructures;

public class StockPriceDictionary
{
    private readonly Dictionary<string, decimal> stockPrices = [];

    public void AddOrUpdateStockPrice(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        ArgumentNullException.ThrowIfNull(transaction.Stock);

        var stock = transaction.Stock;
        stockPrices[transaction.Stock.Symbol] = stock.Price;
        Console.WriteLine($"Update price {transaction.Stock.Symbol}: {stock.Price:C}");
    }

    public decimal GetStockPrice(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty.");

        if (stockPrices.TryGetValue(symbol, out var price))
            return price;
        else
        {
            Console.WriteLine($"Stock {symbol} not found.");
            return 0m;
        }
    }
}
