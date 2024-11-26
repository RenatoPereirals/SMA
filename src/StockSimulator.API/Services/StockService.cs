using StockSimulator.API.Entities;

namespace StockSimulator.API.Services;

public class StockService(List<Stock> listStocks)
{
    private readonly ICollection<Stock> _listStocks = listStocks ?? throw new ArgumentNullException(nameof(listStocks));

    public decimal GetStockPrice(Stock stock)
    {
        ArgumentNullException.ThrowIfNull(stock, "Stock not found.");

        var stockPrice = _listStocks.Where(s => s == stock).Select(s => s.Price).FirstOrDefault();

        return stock.Price;
    }

    public decimal GetStockPriceBySymbol(string symbol)
    {
        var stockPrice = _listStocks.FirstOrDefault(s => s.Symbol == symbol)
            ?? throw new Exception($"Stock {symbol} not found in the market.");

        return stockPrice.Price;
    }

    public decimal GetStockPriceByName(string name)
    {
        var stockPrice = _listStocks.FirstOrDefault(s => s.Name == name)
            ?? throw new Exception($"Stock {name} not found in the market.");

        return stockPrice.Price;
    }

    public Portfolio GetPortfolioByUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user, "User not found.");

        var portfolio = user.Portfolio ?? throw new Exception("Stocks not found.");
        return portfolio;
    }

    public ICollection<Stock> GetPortfolioByUserAndDate(User user, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(user, "User not found.");

        var portfolio = user.Portfolio ?? throw new Exception("Stocks not found.");

        return portfolio.Stocks.Where(s => s.Timestamp.Date == date.Date).ToList();
    }
}