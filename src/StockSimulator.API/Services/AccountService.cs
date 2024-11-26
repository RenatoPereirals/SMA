using StockSimulator.API.Entities;

namespace StockSimulator.API.Services
{
    public class AccountService
    {
        public decimal GetTotalInvestedInStock(User user, Stock stock)
        {
            return user.Portfolio.Stocks.Where(s => s == stock).Sum(s => s.Price * s.Quantity);
        }
    }
}