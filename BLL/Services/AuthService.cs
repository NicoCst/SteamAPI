using BLL.Interfaces;
using BLL.Models.Forms;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? Login(LoginForm form)
    {
        User? user = _userRepository.GetByEmail(form.Email);

        if (user is null)
        {
            return null;
        }

        if (BCrypt.Net.BCrypt.Verify(form.Password, user.Password))
        {
            return user;
        }

        return null;
    }
}