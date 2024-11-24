
using StockSimulator.API.Entities;
using StockSimulator.API.Enums;

namespace StockSimulator.API.API.Entities;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public TransactionType Type { get; }  // None, Buy or Sell
    public Stock Stock { get; }
    public int Quantity { get; }
    public decimal TotalValue { get; }
    public User User { get; }
    public DateTime Timestamp { get; }

    public Transaction(TransactionType type, Stock stock, int quantity, User user)
    {
        Type = type;

        if (quantity <= 0)
            throw new ArgumentException("The quantity of stocks must be greater than zero.");
            
        User = user ?? throw new ArgumentNullException(nameof(user), "The user cannot be null.");
        Stock = stock ?? throw new ArgumentNullException(nameof(stock), "The stock cannot be null.");

        Quantity = quantity;
        TotalValue = stock.Price * quantity;
        Timestamp = DateTime.Now;
    }

    public static void GetTransactionSummary(List<Transaction> transactions)
    {
        var total = transactions.Sum(t => t.TotalValue);

        foreach (var transaction in transactions)
        {
            var stock = transaction.Stock;

            Console.WriteLine($"{transaction.Timestamp} - {transaction.Type} {transaction.Quantity}" +
            $" {stock.Symbol} at {stock.Price:C}");
        }
    }

    // Method to deposit an amount into the account
    public void Deposit(User user, decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be positive.");
        user.SetBalance(amount);
    }

    // Method to withdraw an amount from the account
    public bool Withdraw(User user, decimal amount)
    {
        var balance = user.GetBalance(user);

        if (amount <= 0) throw new ArgumentException("Withdraw amount must be positive.");

        if (amount > balance) return false; // Insufficient balance
        balance -= amount;
        return true;
    }
}
