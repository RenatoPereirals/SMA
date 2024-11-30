using FluentAssertions;
using Moq;
using StockSimulator.API.Entities;
using StockSimulator.API.Repository;
using StockSimulator.API.Services;

namespace StockSimulator.Tests.Unit.ServiceTests;

public class AccountServiceTests
{
    private readonly Mock<UserRepository> _mockUserRepository = new Mock<UserRepository>();
    private readonly AccountService _accountService;
    private readonly User _user;

    public AccountServiceTests()
    {
        Guid portfolioId = Guid.NewGuid();
        var stock = new Stock("StockName", 200, "StockSymbol", 20);

        _user = CreateUserWithPortfolio(stock);

        _mockUserRepository
            .Setup(u => u.GetUserById(It.IsAny<Guid>()))
            .Returns(_user);

        _accountService = new AccountService(_mockUserRepository.Object);
    }

    private User CreateUserWithPortfolio(Stock stock)
    {
        var user = new User("FirstName", "LastName", new Account("password", "email"), 0m);

        Portfolio portfolio = new(user.UserId);
        portfolio.AddStock(stock, 1);

        user.Portfolio = portfolio;
        _ = new Stock("StockName", 200, "StockSymbol", 20);
        return user;
    }

    [Fact]
    public void GetTotalInvestedInStock_ShouldReturnCorrectTotal()
    {
        // Arrange
        Stock testStock = new Stock("StockName", 200, "StockSymbol", 20);

        // Act
        var totalInvested = _accountService.GetTotalInvestedInStock(_user.UserId, testStock);

        // Assert
        totalInvested.Should().Be(4000);
    }

    [Fact]
    public void GetTotalInvestedInStock_ShouldReturnZero_WhenStockNotInPortfolio()
    {
        // Arrange
        var stock = new Stock("NoStockName", 200, "NoStockSymbol", 1);

        // Act
        var totalInvested = _accountService.GetTotalInvestedInStock(_user.UserId, stock);

        // Assert
        Assert.Equal(0, totalInvested);
    }

    [Fact]
    public void GetTotalInvestedInStock_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        _ = _mockUserRepository
            .Setup(u => u.GetUserById(It.IsAny<Guid>()))
            .Returns(() => null!);

        // Act
        Action act = () => _accountService.GetTotalInvestedInStock(Guid.NewGuid(), new Stock("StockName", 200, "StockSymbol", 20));

        // Assert
        act.Should().Throw<Exception>().WithMessage("User not found.");
    }

    [Fact]
    public void GetTotalInvestedInStock_ShouldThrowException_WhenStockIsNull()
    {
        // Arrange & Act
        Action act = () => _accountService.GetTotalInvestedInStock(_user.UserId, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("Stock cannot be null. (Parameter 'stock')");
    }
}