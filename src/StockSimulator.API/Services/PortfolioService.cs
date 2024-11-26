using StockSimulator.API.Entities;
using StockSimulator.API.Enums;

namespace StockSimulator.API.Services
{
    public class PortfolioService(Portfolio _portfolio)
    {
        private readonly Portfolio portfolio = _portfolio ?? throw new ArgumentNullException(nameof(portfolio));

        public void UpdateUserPortfolio(User user)
        {
            if (user.Transactions == null || user.Transactions.Count == 0)
                throw new InvalidOperationException("No transactions found for the user.");

            var transaction = user.Transactions[user.Transactions.Count - 1];

            ArgumentNullException.ThrowIfNull(transaction.Stock);

            var quantity = transaction.Type.HasFlag(TransactionType.Buy)
                ? transaction.Quantity
                : -transaction.Quantity;

            _portfolio.UpdateStock(transaction.Stock, quantity);
        }

        public void AddStockToPortfolio(Stock stock, int quantity)
        {
            ArgumentNullException.ThrowIfNull(stock);

            if (quantity <= 0)
                throw new ArgumentException("The quantity of stocks must be greater than zero.");

            _portfolio.AddStock(stock, quantity);
        }

        public void RemoveStockFromPortfolio(Stock stock, int quantity)
        {
            ArgumentNullException.ThrowIfNull(stock);

            if (quantity <= 0)
                throw new ArgumentException("The quantity of stocks must be greater than zero.");

            _portfolio.RemoveStock(stock, quantity);
        }
    }
}
