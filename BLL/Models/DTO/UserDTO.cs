namespace BLL.Models.Forms;

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public int IsDev { get; set; }
}