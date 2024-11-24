using StockSimulator.API.Entities;

namespace StockSimulator.API.Data;

public class StockList
{
    // List of stocks available for trading
    public static readonly ICollection<Stock> Stocks =
    [
        // Name, Price, Symbol, Quantity
        new("Tesla", 500m, "TSLA", 1000),
        new("Apple", 150m, "AAPL", 1000),
        new("Amazon", 300m, "AMZN", 500),
        new("Google", 220m, "GOOG", 2000),
        new("Microsoft", 280m, "MSFT", 1500),
        new("Facebook", 320m, "FB", 800),
        new("Netflix", 400m, "NFLX", 1000),
        new("Twitter", 50m, "TWTR", 2000),
        new("Uber", 30m, "UBER", 5000),
        new("Lyft", 25m, "LYFT", 5000),
        new("Zoom", 100m, "ZM", 1000),
        new("Slack", 20m, "WORK", 2000),
        new("Shopify", 150m, "SHOP", 1000),
        new("Salesforce", 200m, "CRM", 1000),
        new("Oracle", 80m, "ORCL", 2000),
        new("IBM", 120m, "IBM", 1500),
        new("Intel", 50m, "INTC", 3000),
        new("AMD", 70m, "AMD", 2000)
    ];
}