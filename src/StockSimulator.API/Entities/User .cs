namespace StockSimulator.API.Entities;

public class User(string firstName, string lastName, Account account, decimal initialBalance)
{
    public Guid UserId { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public string FullName { get; private set; } = $"{firstName} {lastName}";
    public decimal Balance { get; private set; } = initialBalance >= 0 ? initialBalance : throw new ArgumentException("Initial balance cannot be negative.");
    public Account Account { get; private set; } = account;
    private List<Stock> Portfolio { get; set; } = [];

    // Method to get user by Id
    public Guid GetUserById(Guid userId)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        if (userId == UserId)
        {
            return userId;
        }
        else
        {
            throw new Exception("User not found.");
        }
    }

    // Method to add a stock to the user's portfolio
    public void AddStockToPortfolio(Stock stock)
    {
        Portfolio.Add(stock);
    }

    // Method to remove a stock from the user's portfolio
    public void RemoveStockFromPortfolio(Stock stock)
    {
        Portfolio.Remove(stock);
    }

    // Method to get the user's full name
    public string GetFullName()
    {
        return FullName;
    }

    // Method to get the user's portfolio
    public ICollection<Stock> GetPortfolio(User user)
    {
        return user.Portfolio;
    }

    //Method to get the user's account
    public Account GetUserAccount(User user)
    {
        return user.Account;
    }

    // Method to set the user's balance
    public void SetBalance(decimal value)
    {
        if (value < 0) throw new ArgumentException("Balance cannot be negative.");
        Balance = value;
    }

    public decimal GetBalance(User user)
    {
        return user.Balance;
    }

    // Method to withdraw money from the account
    public decimal GetPortfolioValue(User user)
    {
        decimal totalValue = 0;
        var portfolio = GetPortfolio(user);

        foreach (var stock in portfolio)
            totalValue += stock.Price * stock.Quantity;

        return totalValue;
    }
}

