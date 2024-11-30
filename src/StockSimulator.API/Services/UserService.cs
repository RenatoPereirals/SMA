using StockSimulator.API.Entities;
using StockSimulator.API.Enums;
using StockSimulator.API.Repository;

namespace StockSimulator.API.Services;

public class UserService(UserRepository userRepository,
                          PortfolioService portfolioService,
                          AccountService accountService)
{
    private readonly UserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly PortfolioService _portfolioService = portfolioService ?? throw new ArgumentNullException(nameof(portfolioService));
    private readonly AccountService _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

    public void UpdateBalance(User user, decimal value)
    {
        ArgumentNullException.ThrowIfNull(user);
        if (value < 0) throw new ArgumentException("Balance cannot be negative.");

        user.SetBalance(value);
        _userRepository.Update(user);
    }

    public void ApplyTransactionToUser(Guid userId, Transaction transaction)
    {
        var user = _userRepository.GetUserById(userId);
        ArgumentNullException.ThrowIfNull(user);

        ApplyTransactionByType(user, transaction);
        user.AddTransaction(transaction);

        _portfolioService.UpdateUserPortfolio(user);
        _userRepository.Update(user);
    }

    public void ApplyTransactionByType(User user, Transaction transaction)
    {
        switch (transaction.Type)
        {
            case TransactionType.Buy:
                HandleBuyTransaction(user, transaction);
                break;
            case TransactionType.Sell:
                HandleSellTransaction(user, transaction);
                break;
            case TransactionType.Deposit:
                user.SetBalance(user.Balance + transaction.Amount);
                break;
            case TransactionType.Withdraw:
                if (transaction.Amount > user.Balance)
                    throw new InvalidOperationException("Insufficient funds.");
                user.SetBalance(user.Balance - transaction.Amount);
                break;
            default:
                throw new ArgumentException("Invalid transaction type.");
        }

        user.AddTransaction(transaction);
        _userRepository.Update(user);
    }

    public decimal CalculateProfitOrLoss(User user)
    {
        ArgumentNullException.ThrowIfNull(user, "User not found.");

        decimal totalProfitOrLoss = 0;

        var portfolio = user.Portfolio;
        var stocks = portfolio.Stocks;

        foreach (var stock in stocks)
        {
            decimal currentMarketValue = stock.Price * stock.Quantity;

            decimal totalInvestment = _accountService.GetTotalInvestedInStock(user.UserId, stock);

            totalProfitOrLoss += currentMarketValue - totalInvestment;
        }

        return totalProfitOrLoss;
    }

    private void HandleBuyTransaction(User user, Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction.Stock);

        if (transaction.Quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        var totalCost = transaction.Stock.Price * transaction.Quantity;
        if (user.Balance < totalCost)
            throw new InvalidOperationException("Insufficient funds.");

        user.SetBalance(user.Balance - totalCost);
        _portfolioService.AddStockToPortfolio(transaction.Stock, transaction.Quantity);
    }

    private void HandleSellTransaction(User user, Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction.Stock);

        var totalValue = transaction.Stock.Price * transaction.Quantity;
        user.SetBalance(user.Balance + totalValue);
        _portfolioService.RemoveStockFromPortfolio(transaction.Stock, transaction.Quantity);
    }

}