using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IUserRepository : IRepository<int, User>
{
    IEnumerable<User> GetAllFriends(int id);
    IEnumerable<User> GetFriendsRequests(int id);
    bool AcceptFriendRequest(int id);
}