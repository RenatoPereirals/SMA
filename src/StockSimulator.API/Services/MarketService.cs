
using StockSimulator.API.Entities;

namespace StockSimulator.API.Services;

public class MarketService(ICollection<Stock> listStocks, UserServices userServices)
{
    private readonly ICollection<Stock> _listStocks = listStocks ?? throw new ArgumentNullException(nameof(listStocks));
    private readonly UserServices _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));

    // Method to add a stock to the market
    public void AddStock(Stock stock)
    {
        _listStocks.Add(stock);
    }

    // Method to update the prices of all stocks
    public void UpdatePrices()
    {
        foreach (var stock in _listStocks)
        {
            // Simulating price fluctuation with a variation of -5% to +5%
            Random rand = new();
            decimal fluctuation = (decimal)(rand.NextDouble() * 0.1 - 0.05);
            stock.Price += stock.Price * fluctuation;

            // Ensure the price is not negative
            if (stock.Price < 0)
                stock.Price = 0;

            stock.Timestamp = DateTime.Now;  // Update the fluctuation date
        }
    }

// Method to calculate profit or loss of a user    
    public decimal CalculateProfitOrLoss(User user)
    {
        decimal totalProfitOrLoss = 0;

        foreach (var stock in user.GetPortfolio(user))
        {
            decimal currentMarketValue = stock.Price * stock.Quantity;

            decimal totalInvestment = _userServices.GetTotalInvestedInStock(user, stock.Symbol);

            totalProfitOrLoss += currentMarketValue - totalInvestment;
        }

        return totalProfitOrLoss;
    }
}