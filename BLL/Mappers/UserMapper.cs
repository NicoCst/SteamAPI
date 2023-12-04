using BLL.Models.Forms;
using DAL.Entities;

namespace BLL.Mappers;

public static class UserMapper
{
    public static User ToUser(this UserForm form)
    {
        return new User()
        {
            Id = 0,
            FirstName = form.FirstName,
            LastName = form.LastName,
            NickName = form.NickName,
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
            NickName = entity.NickName,
            Email = entity.Email,
            IsDev = entity.IsDev,
        };
    }
}