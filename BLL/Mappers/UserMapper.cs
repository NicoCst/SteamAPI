using BLL.Models.Forms;
using DAL.Entities;

namespace BLL.Mappers;

public static class UserMapper
{
    public static User ToUser(this UserForm form)
    {
        return new User()
        {
            Id = form.Id,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            Password = form.Password,
            IsDev = form.IsDev,
        };
    }
    
    public static UserDTO ToUserDto(this User entity)
    {
        return new UserDTO()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            IsDev = entity.IsDev,
        };
    }
}