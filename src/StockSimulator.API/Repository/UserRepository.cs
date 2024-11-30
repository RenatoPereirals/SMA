using StockSimulator.API.Entities;

namespace StockSimulator.API.Repository;

public class UserRepository
{
    private readonly List<User> _users = new List<User>();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public virtual User GetUserById(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("The user ID cannot be empty.");

        return _users.FirstOrDefault(u => u.UserId == userId) ?? throw new KeyNotFoundException($"User with ID {userId} not found.");
    }

    public void Update(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var existingUser = GetUserById(user.UserId);
        if (existingUser != null)
        {
            _users.Remove(existingUser);
            _users.Add(user);
        }
    }

    public void Delete(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("The user ID cannot be empty.");

        var user = GetUserById(userId);
        if (user != null)
            _users.Remove(user);
    }
}