using StockSimulator.API.Entities;
using StockSimulator.API.Repository;

namespace StockSimulator.API.Services;

public class AccountService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public decimal GetTotalInvestedInStock(Guid userId, Stock stock)
    {
        if (stock == null) throw new ArgumentNullException(nameof(stock), "Stock cannot be null.");
        var user = _userRepository.GetUserById(userId) ?? throw new Exception("User not found.");

        var totalINvest = user.Portfolio.Stocks.Where(s => s.Symbol == stock.Symbol).Sum(s => s.Price * s.Quantity);
        return totalINvest;
    }
}