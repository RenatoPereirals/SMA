namespace StockSimulator.API.Entities;

public class Stock
{
    public string Symbol { get; set; } // Ticker symbol
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime Timestamp { get; set; }

    public Stock(string name, decimal price, string symbol, int initialQuantity)
    {
        if (price < 0) throw new ArgumentException("Price cannot be negative.");
        if (initialQuantity < 0) throw new ArgumentException("Initial quantity cannot be negative.");

        Name = name;
        Symbol = symbol;
        Price = price;
        Quantity = initialQuantity;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0) throw new ArgumentException("New price cannot be negative.");
        Price = newPrice;
    }

    public void AddQuantity(int amount)
    {
        if (amount < 0) throw new ArgumentException("Amount to add cannot be negative.");
        Quantity += amount;
    }

    public bool RemoveQuantity(int amount)
    {
        if (amount < 0) throw new ArgumentException("Amount to remove cannot be negative.");
        if (amount > Quantity) return false; // Insufficient quantity

        Quantity -= amount;
        return true;
    }
}
