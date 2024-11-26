
using StockSimulator.API.Entities;

namespace StockSimulator.API.DataStructures;

public class TransactionQueue
{
    private readonly Queue<Transaction> transactionQueue = new();

    public void EnqueueTransaction(Transaction transaction)
    {
        transactionQueue.Enqueue(transaction);
    }

    public void ProcessTransactions()
    {
        while (transactionQueue.Count > 0)
        {
            var transaction = transactionQueue.Dequeue();
        }
    }
}
