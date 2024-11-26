
using StockSimulator.API.Data;
using StockSimulator.API.Entities;
using StockSimulator.API.Services.Results;

namespace StockSimulator.API.Services;

public class MarketService(ICollection<Stock> listStocks)
{
    private readonly ICollection<Stock> _listStocks = listStocks ?? throw new ArgumentNullException(nameof(listStocks));

    public void UpdateAllStockPrices()
    {
        foreach (var stock in _listStocks)
        {
            // Simulating price fluctuation with a variation of -5% to +5%
            Random rand = new();
            decimal fluctuation = (decimal)(rand.NextDouble() * 0.1 - 0.05);
            stock.Price += stock.Price * fluctuation;

            stock.Price = stock.Price < 0 ? 0 : stock.Price; // Ensure price does not go negative

            stock.Timestamp = DateTime.Now;
        }
    }

    public StockPurchaseResult BuyStockFromMarket(Stock stock, int quantity)
    {
        if (stock == null)
            throw new ArgumentNullException(nameof(stock), "The stock cannot be null.");


        if (quantity <= 0)
            throw new ArgumentException("The quantity of stocks must be greater than zero.");

        var existingStock = StockList.Stocks.FirstOrDefault(s => s == stock);

        if (existingStock == null)
            return new StockPurchaseResult(false, "Stock not found in the market.", 0);

        bool sufficientStocks = existingStock.Quantity >= quantity;

        if (!sufficientStocks)
            return new StockPurchaseResult(false, "Insufficient stock quantity in the market.", existingStock.Quantity);

        existingStock.Quantity -= quantity;
        return new StockPurchaseResult(true, "Stock successfully purchased.", existingStock.Quantity);
    }

    public StockPurchaseResult SellStockToMarket(Stock stock, int quantity)
    {
        if (stock == null)
            throw new ArgumentNullException(nameof(stock), "The stock cannot be null.");


        if (quantity <= 0)
            throw new ArgumentException("The quantity of stocks must be greater than zero.");

        var existingStock = StockList.Stocks.FirstOrDefault(s => s == stock);

        if (existingStock == null)
        {
            AddStockToMarket(stock);
            existingStock = stock;
        }

        existingStock.Quantity += quantity;

        return new StockPurchaseResult(true, "Stock successfully sold.", existingStock.Quantity);
    }

    private static void AddStockToMarket(Stock stock)
    {
        StockList.Stocks.Add(stock);
    }
}
