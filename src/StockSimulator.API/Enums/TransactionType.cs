namespace StockSimulator.API.Enums
{
    [Flags]
    public enum TransactionType
    {
        None = 0,
        Buy = 1,
        Sell = 2,
        Withdraw = 4,
        Deposit = 8,

        Decrease = Buy | Withdraw,   // Transactions that decrease the balance
        Increase = Sell | Deposit    // Transaction that increase the balance
    }
}
