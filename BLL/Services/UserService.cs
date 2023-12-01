using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models.Forms;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public UserDTO Create(UserForm form)
    {
        form.Password = BCrypt.Net.BCrypt.HashPassword(form.Password);
        form.Email = form.Email.ToLower();
        return _userRepository.Create(form.ToUser()).ToUserDto();
    }
    
    public IEnumerable<UserDTO> GetAll()
    {
        return _userRepository.GetAll().Select(x => x.ToUserDto());
    }

    public IEnumerable<UserDTO> GetAllFriends(int id)
    {
        return _userRepository.GetAllFriends(id).Select(x => x.ToUserDto());
    }

    public bool CreateFriendRequest(AddFriendForm form1, AddFriendForm form2)
    {
        User user1 = _userRepository.GetByNickname(form1.UserNickname);
        User user2 = _userRepository.GetByNickname(form2.FriendNickname);
        return _userRepository.CreateFriendRequest(user1, user2);
    }
    
    public IEnumerable<UserDTO> GetFriendsRequest(int id)
    {
        return _userRepository.GetFriendsRequests(id).Select(x => x.ToUserDto());
    }

    public bool Update(UserForm entity)
    {
        throw new NotImplementedException();
    }

    public bool Update(int id, UserForm form)
    {
        form.Password = BCrypt.Net.BCrypt.HashPassword(form.Password);
        form.Email = form.Email.ToLower();

        User user = form.ToUser();

        user.Id = id;

        return _userRepository.Update(user);
    }

    public bool AcceptFriendRequest(int id)
    {
        return _userRepository.AcceptFriendRequest(id);
    }
}