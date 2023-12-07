using DAL.Entities;

namespace BLL.Interfaces;

public interface IJwtService
{
    string CreateToken(User user);
}