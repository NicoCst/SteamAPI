using BLL.Models.Forms;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class UserMapper
    {
        /// <summary>
        /// Maps a UserForm to a User entity.
        /// </summary>
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

        /// <summary>
        /// Maps a User entity to a UserDTO.
        /// </summary>
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
}
