﻿using BLL.Models.Forms;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IUserService
{
    IEnumerable<UserDTO> GetAll();
    UserDTO? GetByNickname(string nickname);
    UserDTO? GetByEmail(string email);
    IEnumerable<UserDTO> GetAllFriends(int id);
    bool CreateFriendRequest(CreateFriendRequestForm requestForm);
    IEnumerable<UserDTO> GetFriendsRequest(int id);
    bool Update(int id, UserForm entity);
    bool TogglePlayingStatut(User user, int statut);
    bool AddMoney(AddMoneyForm form);
    bool AcceptFriendRequest(AcceptFriendRequestForm form);
    bool DeleteFriendRequest(DeleteFriendRequestForm form);
    UserDTO Create(UserForm entity);
}