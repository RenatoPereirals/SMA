
using StockSimulator.API.Entities;

namespace StockSimulator.API.DataStructures
{
    public class TransactionLinkedList
    {
        private readonly LinkedList<Transaction> _transactions = new();

        // Adding a transaction to the linked list
        public void AddTransaction(Transaction transaction)
        {
            _transactions.AddLast(transaction);
        }
    }
}
