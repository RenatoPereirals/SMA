using StockSimulator.API.Entities;

namespace StockSimulator.API.Services;

public class UserServices(User user, List<User> users, StockService stockService)
{
    private readonly StockService _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
    private readonly User _user = user ?? throw new ArgumentNullException(nameof(user));
    private readonly List<User> _users = users ?? throw new ArgumentNullException(nameof(users));

    // Method to get user by name
    public User GetUserByName(string name) // This method is not correct. It should get a user by name in the list of users
    {
        var user = _users.FirstOrDefault(
            u => u.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
            u.LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
            ?? throw new Exception("User not found.");

        return user;
    }

    // Method to get the quantity of a stock in the portfolio
    public int GetStockQuantity(User user, Stock stock)
    {
        var existingStock = _user.GetPortfolio(user).FirstOrDefault(s => s.Symbol == stock.Symbol);
        return existingStock != null ? existingStock.Quantity : 0;
    }

    // Method to check if the user has a stock in the portfolio
    public bool HasStockInPortfolio(Stock stock, int quantity)
    {
        var existingStock = _user.GetPortfolio(user).FirstOrDefault(s => s.Symbol == stock.Symbol);
        return existingStock != null && existingStock.Quantity >= quantity;
    }

    // Method to get the total invested in a stock
    public decimal GetTotalInvestedInStock(User user, string stockSymbol)
    {
        var stock = _user.GetPortfolio(user).FirstOrDefault(s => s.Symbol == stockSymbol);
        if (stock != null)
            return stock.Quantity * stock.Price;

        return 0;
    }

    // Method to get user's Portfolio
    public ICollection<Stock> GetPortfolio(User user)
    {
       var portfolio = _stockService.GetPortfolioByUser(user) ?? throw new Exception("Stocks not found.");

        return portfolio;
    }
}