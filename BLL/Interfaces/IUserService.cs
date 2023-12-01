using BLL.Models.Forms;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IUserService
{
    IEnumerable<UserDTO> GetAll();
    UserDTO? GetByNickname(string nickname);
    IEnumerable<UserDTO> GetAllFriends(int id);
    bool CreateFriendRequest(AddFriendForm form);
    IEnumerable<UserDTO> GetFriendsRequest(int id);
    bool Update(int id, UserForm entity);
    bool AcceptFriendRequest(int id);
    UserDTO Create(UserForm entity);
}