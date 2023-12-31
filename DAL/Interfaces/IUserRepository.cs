﻿using DAL.Entities;
using ToolBox.Services;

namespace DAL.Interfaces;

public interface IUserRepository : IRepository<int, User>
{
    IEnumerable<User> GetAllFriends(int id);
    IEnumerable<User> GetFriendsRequests(int id);
    bool CreateFriendRequest(User entity1, User entity2);
    bool AcceptFriendRequest(User entity1, User entity2);
    bool DeleteFriendRequest(User entity1, User entity2);
    User? GetByNickname(string nickname);
    User? GetByEmail(string email);
    bool UpdateWallet(int userId, float newWalletAmount);
    bool TogglePlayingStatut(User user, int statut);
    bool AddMoney(User entity, float money);
}