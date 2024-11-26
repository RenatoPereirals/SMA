
using StockSimulator.API.Entities;
using StockSimulator.API.Enums;

namespace StockSimulator.API.Entities;

public class User
{
    private readonly List<Transaction> _transactions = new List<Transaction>() ?? throw new ArgumentNullException(nameof(_transactions));

    public Guid UserId { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }
    public decimal Balance { get; private set; }
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
    public FinancialAccount FinancialAccount { get; set; }
    public Account Account { get; private set; }
    public Portfolio Portfolio { get; set; }

    public User(string firstName, string lastName, Account account, decimal initialBalance)
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = $"{firstName} {lastName}";
        Balance = initialBalance >= 0 ? initialBalance : throw new ArgumentException("Initial balance cannot be negative.");
        FinancialAccount = new FinancialAccount(initialBalance);
        Account = account;
        Portfolio = new Portfolio(UserId);
    }

    //Method to get the user's account
    public Account GetUserFinancialAccount(User user)
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

    public void AddTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        _transactions.Add(transaction);
    }
}

