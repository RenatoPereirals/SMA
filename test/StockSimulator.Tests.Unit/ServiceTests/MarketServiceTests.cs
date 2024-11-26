    using StockSimulator.API.Data;
    using StockSimulator.API.Entities;
    using StockSimulator.API.Services;

    namespace StockSimulator.Tests.Unit.ServiceTests;

    public class MarketServiceTests
    {
        private readonly List<Stock> _stockList;
        private readonly MarketService _marketService;

        public MarketServiceTests()
        {
            _stockList = CreateTestStocks();
            _marketService = new MarketService(_stockList);
        }

        #region UpdateAllStockPrices Tests
        [Fact]
        public void UpdateAllStockPrices_ShouldUpdatePricesAndTimestamps()
        {
            // Arrange
            ResetStockPrices();

            // Act
            _marketService.UpdateAllStockPrices();

            // Assert
            Assert.All(_stockList, stock =>
            {
                Assert.NotEqual(100, stock.Price);
                Assert.NotEqual(200, stock.Price);
                Assert.True(stock.Price >= 0);
                Assert.True(stock.Timestamp > DateTime.Now.AddSeconds(-1));
            });

        }

        [Fact]
        public void UpdateAllStockPrices_MultipleUpdates_ShouldMaintainValidPrices()
        {
            ResetStockPrices();

            for (int i = 0; i < 10; i++)
            {
                _marketService.UpdateAllStockPrices();
            }

            Assert.All(_stockList, stock => Assert.True(stock.Price >= 0));
        }

        [Fact]
        public void UpdateAllStockPrices_ShouldNotSetNegativePrices()
        {
            // Arrange
            ResetStockPrices();

            // Act
            _marketService.UpdateAllStockPrices();

            // Assert
            Assert.All(_stockList, stock => { Assert.True(stock.Price >= 0); });
        }
        #endregion

        #region BuyStockFromMarket Tests
        [Fact]
        public void BuyStockFromMarket_StockIsNull_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _marketService.BuyStockFromMarket(null!, 1));
            Assert.Equal("The stock cannot be null. (Parameter 'stock')", exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void BuyStockFromMarket_InvalidQuantity_ThrowsArgumentException(int quantity)
        {
            // Arrange
            var stock = StockList.Stocks.First();

            // Act & Asset
            var exception = Assert.Throws<ArgumentException>(() => _marketService.BuyStockFromMarket(stock, quantity));
            Assert.Equal("The quantity of stocks must be greater than zero.", exception.Message);
        }


        [Fact]
        public void BuyStockFromMarket_StockNotFound_ReturnsStockPurchaseResultWithFalse()
        {
            var stock = new Stock("Stock C", 300, "C", 30);
            var result = _marketService.BuyStockFromMarket(stock, 1);

            Assert.False(result.IsSuccessful);
            Assert.Equal("Stock not found in the market.", result.Message);
        }

        [Fact]
        public void BuyStockFromMarket_InsufficientQuantity_ReturnsStockPurchaseResultWithFalse()
        {
            var stock = StockList.Stocks.First();
            var result = _marketService.BuyStockFromMarket(stock, 100000);

            Assert.False(result.IsSuccessful);
            Assert.Equal("Insufficient stock quantity in the market.", result.Message);
        }

        [Fact]
        public void BuyStockFromMarket_SufficientQuantity_ReturnsStockPurchaseResultWithTrue()
        {
            var stock = StockList.Stocks.First();
            var result = _marketService.BuyStockFromMarket(stock, 5);

            Assert.True(result.IsSuccessful);
            Assert.Equal("Stock successfully purchased.", result.Message);
        }
        #endregion

        #region SellStockToMarket Tests
        [Fact]
        public void SellStockToMarket_StockIsNull_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _marketService.SellStockToMarket(null!, 1));
            Assert.Equal("The stock cannot be null. (Parameter 'stock')", exception.Message);
        }

        [Fact]
        public void SellStockToMarket_QuantityIsZero_ThrowsArgumentException()
        {
            // Arrange
            var stock = StockList.Stocks.First();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _marketService.SellStockToMarket(stock, 0));
            Assert.Equal("The quantity of stocks must be greater than zero.", exception.Message);
        }

        [Fact]
        public void SellStockToMarket_NewStock_ShouldAddToMarket()
        {
            // Arrange
            var stock = new Stock("Stock C", 300, "C", 30);

            // Act
            var result = _marketService.SellStockToMarket(stock, 1);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Stock successfully sold.", result.Message);
        }

        [Fact]
        public void SellStockToMarket_SufficientQuantity_ReturnsStockPurchaseResultWithTrue()
        {
            // Arrange
            var stock = StockList.Stocks.First();

            // Act
            var result = _marketService.SellStockToMarket(stock, 5);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Stock successfully sold.", result.Message);
        }
        #endregion

        private List<Stock> CreateTestStocks()
        {
            return new List<Stock>
            {
                new Stock("Stock A", 100, "A", 10),
                new Stock("Stock B", 200, "B", 20)
            };
        }

        private void ResetStockPrices()
        {
            foreach (var stock in _stockList)
            {
                stock.Price = 100;
                stock.Timestamp = DateTime.Now.AddSeconds(-1);
            }
        }
    }