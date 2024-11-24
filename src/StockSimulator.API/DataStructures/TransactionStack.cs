
using StockSimulator.API.API.Entities;

namespace StockSimulator.API.DataStructures;

public class TransactionStack
{
    private readonly Stack<Transaction> transactionHistory = new();

    public void PushTransaction(Transaction transaction)
    {
        transactionHistory.Push(transaction);
    }

    public void UndoTransaction()
    {
        if (transactionHistory.Count > 0)
        {
            var lastTransaction = transactionHistory.Pop();
        }
        else
        {
            Console.WriteLine("No transaction to undo.");
        }
    }
}
