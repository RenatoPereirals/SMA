namespace StockSimulator.API.Entities;

public class FinancialAccount
{
    public decimal Balance { get; private set; }

    public FinancialAccount(decimal initialBalance)
    {
        if (initialBalance < 0) throw new ArgumentException("Balance cannot be negative.");
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount < 0) throw new ArgumentException("Deposit amount cannot be negative.");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0) throw new ArgumentException("Withdrawal amount cannot be negative.");
        if (Balance < amount) throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
    }

    public decimal GetBalance()
    {
        return Balance;
    }
}