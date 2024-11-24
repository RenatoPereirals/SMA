using StockSimulator.API.Entities;

namespace StockSimulator.API.Services;

public class StockService(List<Stock> listStocks, User user)
{
    private readonly User _user = user ?? throw new ArgumentNullException(nameof(user));
    private readonly ICollection<Stock> _listStocks = listStocks ?? throw new ArgumentNullException(nameof(listStocks));

    // Method to get the stock price by stock
    public decimal GetStockPrice(Stock stock)
    {
        ArgumentNullException.ThrowIfNull(stock, "Stock not found.");

        var stockPrice = _listStocks.Where(s => s == stock).Select(s => s.Price).FirstOrDefault();

        return stock.Price;
    }

    // Method to get the stock price by symbol
    public decimal GetStockPriceBySymbol(string symbol)
    {
        var stockPrice = _listStocks.FirstOrDefault(s => s.Symbol == symbol)
            ?? throw new Exception($"Stock {symbol} not found in the market.");

        return stockPrice.Price;
    }

    // Method to get the stock price by name
    public decimal GetStockPriceByName(string name)
    {
        var stockPrice = _listStocks.FirstOrDefault(s => s.Name == name)
            ?? throw new Exception($"Stock {name} not found in the market.");

        return stockPrice.Price;
    }

    // Method to get the stock price by user
    public ICollection<Stock> GetPortfolioByUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user, "User not found.");

        var portfolio = user.GetPortfolio(user) ?? throw new Exception("Stocks not found.");
        return portfolio;
    }

    // Get all stock prices by user and date
    public ICollection<Stock> GetPortfolioByUserByDate(User user, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(user, "User not found.");
        
        return _user.GetPortfolio(user).Where(s => s.Timestamp.Date == date.Date).ToList();
    }
}