using StockSimulator.API.Entities;
using StockSimulator.API.Enums;

namespace StockSimulator.API.Services;

public class TransactionService(UserService userService,
                                MarketService marketService)
{
    private readonly UserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    private readonly MarketService _marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));

    public void DepositMoneyIntoUserAccount(User user, decimal amount)
    {
        ValidateUserAndAmount(user, amount);

        var transaction = new Transaction(TransactionType.Deposit, user, amount);
        _userService.ApplyTransactionToUser(user.UserId, transaction);
    }

    public bool WithdrawMoneyFromUserAccount(User user, decimal amount)
    {
        ValidateUserAndAmount(user, amount);

        if (amount > user.Balance)
            return false; // Insufficient funds

        var transaction = new Transaction(TransactionType.Withdraw, user, amount);
        _userService.ApplyTransactionToUser(user.UserId, transaction);

        return true;
    }
    public Transaction BuyStockForUser(User user, Stock stock, int quantity)
    {
        ValidateStockTransaction(user, stock, quantity);

        var result = _marketService.BuyStockFromMarket(stock, quantity);

        if (!result.IsSuccessful)
        {
            throw new InvalidOperationException(
                $"{result.Message} Requested: {quantity}, Available: {result.AvailableQuantity}."
            );
        }

        var transaction = new Transaction(TransactionType.Buy, stock, quantity, user);
        _userService.ApplyTransactionToUser(user.UserId, transaction);

        return transaction;
    }

    public Transaction SellStockForUser(User user, Stock stock, int quantity)
    {
        ValidateStockTransaction(user, stock, quantity);

        var result = _marketService.SellStockToMarket(stock, quantity);

        if (!result.IsSuccessful)
        {
            throw new InvalidOperationException();
        }

        var transaction = new Transaction(TransactionType.Sell, stock, quantity, user);
        _userService.ApplyTransactionToUser(user.UserId, transaction);

        return transaction;
    }

    public void GetTransactionSummary(List<Transaction> transactions)
    {
        if (transactions == null || transactions.Count == 0)
            throw new ArgumentException("No transactions found.");

        var transactionFormatters = new Dictionary<TransactionType, Func<Transaction, string>>
        {
            { TransactionType.Buy, t => $"{t.Timestamp} - {t.Type} {t.Quantity} {t.Stock!.Symbol} at {t.Stock.Price:C}" },
            { TransactionType.Sell, t => $"{t.Timestamp} - {t.Type} {t.Quantity} {t.Stock!.Symbol} at {t.Stock.Price:C}" },
            { TransactionType.Withdraw, t => $"{t.Timestamp} - {t.Type} {t.Amount:C}" },
            { TransactionType.Deposit, t => $"{t.Timestamp} - {t.Type} {t.Amount:C}" }
        };

        foreach (var transaction in transactions)
        {
            if (transactionFormatters.TryGetValue(transaction.Type, out var formatter))
            {
                Console.WriteLine(formatter(transaction));
            }
        }
    }

    private void ValidateStockTransaction(User user, Stock stock, int quantity)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(stock);
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
    }

    private void ValidateUserAndAmount(User user, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(user);
        if (amount <= 0) throw new ArgumentException("Amount must be positive.");
    }
}
