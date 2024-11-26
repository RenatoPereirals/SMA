

using StockSimulator.API.Entities;

namespace StockSimulator.API.DataStructures;

public class TransactionBinarySearchTree
{
    private Node<Transaction>? root;

    // Insert a transaction in the tree
    public void Insert(Transaction transaction)
    {
        root = InsertRec(root, transaction);
    }

    private static Node<Transaction> InsertRec(Node<Transaction>? root, Transaction transaction)
    {
        if (root == null)
        {
            root = new Node<Transaction>(transaction);
            return root;
        }

        if (transaction.Timestamp < root.Data.Timestamp)
            root.Left = InsertRec(root.Left, transaction);
        else
            root.Right = InsertRec(root.Right, transaction);

        return root;
    }

    // Search a transaction by timestamp
    public Transaction Search(DateTime timestamp)
    {
        return SearchRec(root, timestamp);
    }

    private static Transaction SearchRec(Node<Transaction>? root, DateTime timestamp)
    {
        ArgumentNullException.ThrowIfNull(root);

        if (root.Data.Timestamp == timestamp)
            return root.Data;

        return timestamp < root.Data.Timestamp ?
            SearchRec(root.Left, timestamp) : SearchRec(root.Right, timestamp);
    }
}

public class Node<T>(T data)
{
    public T Data { get; set; } = data;
public Node<T>? Left { get; set; } = null;
public Node<T>? Right { get; set; } = null;
}
