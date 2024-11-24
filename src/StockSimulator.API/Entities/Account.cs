namespace StockSimulator.API.Entities;

public class Account(string password, string email)
{
    public Guid AccountId { get; private set; } = Guid.NewGuid();
    public string Password { get; set; } = password ?? throw new ArgumentNullException(nameof(password));
    public string Email { get; set; } = email ?? throw new ArgumentNullException(nameof(email));

    // Method to get the Account ID
    public Guid GetAccountId()
    {
        return AccountId;
    }

    // Method to get the user's email
    public bool AuthenticateUserEmail(string email)
    {
        return Email == email;
    }

    // Method to authenticate the user's password
    public bool AuthenticateUserPassword(string password)
    {
        return Password == password;
    }
}
