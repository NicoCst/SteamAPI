using ToolBox.Services;

namespace DAL.Entities;

public class User : IEntity<int>
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IsDev { get; set; }
    public int IsPlaying { get; set; }
    public float Wallet { get; set; }
}