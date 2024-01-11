using Microsoft.VisualBasic;

namespace TireHouse.Facade.LoginException;

public class LoginException : Exception
{
    public LoginException(string username) : base("Login Failed")
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }

    public string Username { get; set; }

    public DateTime FailedDate => DateTime.Now;
}
