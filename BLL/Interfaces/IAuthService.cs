using BLL.Models.Forms;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IAuthService
{
    User? Login(LoginForm form);
}