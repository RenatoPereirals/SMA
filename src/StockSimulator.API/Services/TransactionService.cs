
using StockSimulator.API.API.Entities;
using StockSimulator.API.DataStructures;
using StockSimulator.API.Entities;
using StockSimulator.API.Enums;

namespace StockSimulator.API.Services;

public class TransactionService(TransactionService _transactionService, StockService _stockService, User user)
{
    private readonly TransactionService _transactionService = _transactionService ?? throw new ArgumentNullException(nameof(_transactionService));
    private readonly StockService _stockService = _stockService ?? throw new ArgumentNullException(nameof(_stockService));
    private readonly User _user = user ?? throw new ArgumentNullException(nameof(user));
    private readonly TransactionQueue _transactionQueue = new();
    private readonly TransactionStack transactionStack = new();
    private readonly TransactionBinarySearchTree transactionTree = new();
    private readonly StockPriceDictionary stockPriceDictionary = new();
    private readonly TransactionLinkedList transactionLinkedList = new();

    // Method to deposit money into the account
    public void DepositMoneyInAccount(User user, decimal value)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        var balance = user.GetBalance(user);
        var newBalance = balance + value;

        if (balance < 0)
            throw new InvalidOperationException("The balance cannot be negative.");

        _transactionService.DepositMoneyInAccount(user, newBalance);
    }

    // Method to add a transaction
    public void AddTransaction(Transaction transaction)
    {
        _transactionQueue.EnqueueTransaction(transaction);

        transactionStack.PushTransaction(transaction);

        transactionTree.Insert(transaction);

        stockPriceDictionary.AddOrUpdateStockPrice(transaction);

        transactionLinkedList.AddTransaction(transaction);
    }

    // Process all transactions in the queue
    public void ProcessAllTransactions()
    {
        _transactionQueue.ProcessTransactions();
    }

    // Undo the last transaction
    public void UndoLastTransaction()
    {
        transactionStack.UndoTransaction();
    }

    // Get a transaction by timestamp
    public Transaction GetTransactionByTimestamp(DateTime timestamp)
    {
        return transactionTree.Search(timestamp);
    }

    // Get all transactions for a specific stock
    public decimal GetAllStockForPrice(string symbol)
    {
        return stockPriceDictionary.GetStockPrice(symbol);
    }

    // Get a summary of the transaction
    public static void GetTransactionSummary(List<Transaction> transactions)
    {
        var total = transactions.Sum(t => t.TotalValue);
        foreach (var transaction in transactions)
        {
            var stock = transaction.Stock;
            Console.WriteLine($"{transaction.Timestamp}: {transaction.Type} {transaction.Quantity}" +
                $" stock of {stock.Symbol} at {stock.Price:C} (Total: {total:C})");
        }
    }

    // Method to execute a buy transaction
    public Transaction ExecuteBuyTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction, "Transaction not found.");

        if (IsValidTransaction(transaction))
            throw new InvalidOperationException("This operation is invalid.");

        var stock = transaction.Stock;
        var quantity = transaction.Quantity;
        decimal stockPrice = _stockService.GetStockPrice(stock);
        decimal balance = user.GetBalance(user);

        decimal totalValue = stockPrice * quantity;

        if (balance >= totalValue)
        {
            balance -= totalValue;
            _user.SetBalance(balance);

            user.GetPortfolio(user).Add(stock);

            return new Transaction(TransactionType.Buy, stock, quantity, user);
        }
        else
            throw new Exception("Insufficient funds.");

        throw new InvalidOperationException("This operation is invalid."); // If the transaction type is not Buy
    }

    // Method to execute a sell transaction
    public Transaction ExecuteSellTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction, "Transaction not found.");

        if (IsValidTransaction(transaction))
            throw new InvalidOperationException("This operation is invalid.");

        var stock = transaction.Stock;
        var quantity = transaction.Quantity;
        decimal stockPrice = _stockService.GetStockPrice(stock);
        decimal balance = user.GetBalance(user);

        decimal totalValue = stockPrice * quantity;

        if (balance >= totalValue)
        {
            balance += totalValue;
            _user.SetBalance(balance);

            user.GetPortfolio(user).Remove(stock);

            return new Transaction(TransactionType.Buy, stock, quantity, user);
        }
        else
            throw new Exception("Insufficient funds.");

        throw new InvalidOperationException("This operation is invalid."); // If the transaction type is not Sell
    }

    private bool IsValidTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction, "Transaction not found.");
        ArgumentNullException.ThrowIfNull(transaction.Stock, "Stock not found.");
        ArgumentNullException.ThrowIfNull(transaction.User, "User not found.");

        var quantity = transaction.Quantity;
        var transactionType = transaction.Type;

        if (quantity <= 0)
            throw new ArgumentException("The quantity of stocks must be greater than zero.");

        if (transactionType != TransactionType.Buy && transactionType != TransactionType.Sell)
            throw new InvalidOperationException("This operation is invalid.");

        return true;
    }
}
