﻿using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models.Forms;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;
    public GameService(IGameRepository gameRepository, IUserRepository userRepository)
    {
        _gameRepository = gameRepository;
        _userRepository = userRepository;
    }
    
    // Global Games Functions
    
    public GameDTO Create(GameForm form)
    {
        return _gameRepository.Create(form.ToGame()).ToGameDto();
    }

    public bool Update(int id, GameForm form)
    {
        Game game = form.ToGame();

        game.Id = id;

        return _gameRepository.Update(game);
    }
    
    // GamesList Functions
    
    public IEnumerable<GameDTO> GetAllMyGames(int userId)
    {
        return _gameRepository.GetAllMyGames(userId).Select(x => x.ToGameDto());
    }

    public float GetPrice(string Title)
    {
        return _gameRepository.GetPrice(Title);
    }
    
    public bool BuyGame(BuyGameForm form)
    {
        User user1 = _userRepository.GetByNickname(form.Buyer);
        User user2 = _userRepository.GetByNickname(form.Reciever);
        Game game = _gameRepository.GetByTitle(form.Game);

        if (user1 != null && user2 != null && game != null)
        {
            if (_gameRepository.IsGameInUserList(user2, game) && _gameRepository.IsGameInWishList(user2, game))
            {
                float gamePriceWish = _gameRepository.GetPrice(form.Game);

                if (user1.Wallet >= gamePriceWish)
                {
                    user1.Wallet -= gamePriceWish;
                    
                    _userRepository.UpdateWallet(user1.Id, user1.Wallet);

                    return _gameRepository.BuyWishedGame(user1, user2, game);
                }

                return false;
            }

            if (_gameRepository.IsGameInUserList(user2, game))
            {
                return false;
            }
            
            float gamePrice = _gameRepository.GetPrice(form.Game);

            if (user1.Wallet >= gamePrice)
            {
                user1.Wallet -= gamePrice;

                if (_gameRepository.BuyGame(user1, user2, game))
                {
                    _userRepository.UpdateWallet(user1.Id, user1.Wallet);

                    return true;
                }
            }
        }
        return false;
    }

    public bool RefundGame(RefundGameForm form)
    {
        if (form.Title == null || form.NickName == null)
        {
            return false;
        }

        User user = _userRepository.GetByNickname(form.NickName);
        Game game = _gameRepository.GetByTitle(form.Title);
        
        bool isRefundSuccessful = _gameRepository.RefundGame(user, game);

        if (isRefundSuccessful)
        {
            float gamePrice = _gameRepository.GetPrice(game.Name);
            user.Wallet += gamePrice;

            _userRepository.UpdateWallet(user.Id, user.Wallet);
        }

        return isRefundSuccessful;
    }

    public bool SetToWishlist(AddToWishlistForm form)
    {
        if (form.Title == null || form.NickName == null)
        {
            return false;
        }

        User user = _userRepository.GetByNickname(form.NickName);
        Game game = _gameRepository.GetByTitle(form.Title);

        if (_gameRepository.IsGameInUserList(user, game))
        {
            return _gameRepository.SetToWishlist(user, game);
        }

        return _gameRepository.AddToWishlist(user, game);
    }
    
    // PriceList Functions

    public bool SetNewPrice(SetNewPriceForm form)
    {
        if (form.Title == null || form.Price == null)
        {
            return false;
        }

        Game game = _gameRepository.GetByTitle(form.Title);

        if (_gameRepository.GetPrice(form.Title) == form.Price)
        {
            return false;
        }

        return _gameRepository.SetNewPrice(game, form.Price);
    }
}
