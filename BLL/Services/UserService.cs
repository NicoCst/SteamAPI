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

    public UserDTO GetByNickname(string nickname)
    {
        return _userRepository.GetByNickname(nickname).ToUserDto();
    }

    public UserDTO GetByEmail(string email)
    {
        return _userRepository.GetByEmail(email).ToUserDto();
    }

    public bool TogglePlayingStatut(User user, int statut)
    {
        if (user.IsPlaying == 0)
        {
            return _userRepository.TogglePlayingStatut(user, 1);
        }

        return _userRepository.TogglePlayingStatut(user, 0);
    }
    
    public IEnumerable<UserDTO> GetAllFriends(int id)
    {
        return _userRepository.GetAllFriends(id).Select(x => x.ToUserDto());
    }

    public bool CreateFriendRequest(CreateFriendRequestForm requestForm)
    {
        User? user1 = _userRepository.GetByNickname(requestForm.UserNickname);
        User? user2 = _userRepository.GetByNickname(requestForm.FriendNickname);

        if (user1 != null && user2 != null)
        {
            return _userRepository.CreateFriendRequest(user1, user2);
        }

        return false;
    }
    
    public IEnumerable<UserDTO> GetFriendsRequest(int id)
    {
        return _userRepository.GetFriendsRequests(id).Select(x => x.ToUserDto());
    }
    
    public bool Update(int id, UserForm form)
    {
        form.Password = BCrypt.Net.BCrypt.HashPassword(form.Password);
        form.Email = form.Email.ToLower();

        User user = form.ToUser();

        user.Id = id;

        return _userRepository.Update(user);
    }
    public bool AddMoney(AddMoneyForm form)
    {
        User user = _userRepository.GetByNickname(form.NickName);

        if (user != null)
        {
            float moneyToAdd = form.Money;
            float totalWallet = user.Wallet + moneyToAdd;
            return _userRepository.AddMoney(user, totalWallet);
        }

        return false;
    }
    
    public bool AcceptFriendRequest(AcceptFriendRequestForm form)
    {
        User? user1 = _userRepository.GetByNickname(form.UserNickname);
        User? user2 = _userRepository.GetByNickname(form.FriendNickname);
        
        if (user1 != null && user2 != null)
        {
            return _userRepository.AcceptFriendRequest(user1, user2);
        }

        return false;
    }
    
    public bool DeleteFriendRequest(DeleteFriendRequestForm form)
    {
        User? user1 = _userRepository.GetByNickname(form.UserNickname);
        User? user2 = _userRepository.GetByNickname(form.FriendNickname);
        
        if (user1 != null && user2 != null)
        {
            return _userRepository.DeleteFriendRequest(user1, user2);
        }

        return false;
    }
}