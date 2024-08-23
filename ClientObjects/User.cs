public class User
{

    public string Username { get; set;}
    public string Password { get; set;}
    public int UserID { get; set;}

    public User(int userId, string username, string password)
    {
        UserID = userId;
        Username = username;
        Password = password;
    }

}