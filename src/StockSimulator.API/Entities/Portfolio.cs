namespace StockSimulator.API.Entities;

public class Portfolio(Guid userId)
{
    public Guid UserId { get; private set; } = userId;
    public List<Stock> Stocks { get; private set; } = new List<Stock>();

    public void UpdateStock(Stock stock, int quantity)
    {
        if (quantity > 0)
            AddStock(stock, quantity);
        else
            RemoveStock(stock, -quantity);
    }

    public void AddStock(Stock stock, int quantity)
    {
        var existingStock = Stocks.FirstOrDefault(s => s.Symbol == stock.Symbol);
        if (existingStock != null)
        {
            existingStock.Quantity += quantity;
        }
        else
        {
            Stocks.Add(stock);
        }
    }

    public void RemoveStock(Stock stock, int quantity)
    {
        var existingStock = Stocks.FirstOrDefault(s => s.Symbol == stock.Symbol);
        if (existingStock != null)
        {
            if (existingStock.Quantity < quantity)
                throw new InvalidOperationException("Not enough stock to sell.");

            existingStock.Quantity -= quantity;
            if (existingStock.Quantity == 0)
            {
                Stocks.Remove(existingStock);
            }
        }
        else
        {
            throw new InvalidOperationException("Stock not found in portfolio.");
        }
    }

    public List<Stock> GetPortfolio()
    {
        return Stocks;
    }

    public decimal GetPortfolioValue()
    {
        return Stocks.Sum(stock => stock.Price * stock.Quantity);
    }
}
