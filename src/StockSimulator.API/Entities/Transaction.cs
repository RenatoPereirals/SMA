
using StockSimulator.API.Enums;

namespace StockSimulator.API.Entities;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public TransactionType Type { get; }  // None, Buy or Sell
    public Stock? Stock { get; }
    public int Quantity { get; }
    public decimal Amount { get; }
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
        Amount = stock.Price * quantity;
        Timestamp = DateTime.Now;
    }

    public Transaction(TransactionType type, User user, decimal amount)
    {
        if (Amount <= 0)
            throw new ArgumentException("The Amount must be greater than zero.");

        Type = type;
        User = user;
        Amount = amount;
    }   
}

