using BLL.Models.Forms;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IUserService
{
    IEnumerable<UserDTO> GetAll();
    IEnumerable<UserDTO> GetAllFriends(int id);
    IEnumerable<UserDTO> GetFriendsRequest(int id);
    bool Update(int id, UserForm entity);
    bool AcceptFriendRequest(int id);
    UserDTO Create(UserForm entity);
}