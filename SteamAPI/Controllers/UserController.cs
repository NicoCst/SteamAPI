using BLL.Interfaces;
using BLL.Models.Forms;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ASteamAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("GetAllFriends/{id:int}")]
    public ActionResult<IEnumerable<UserDTO>> GetAllFriends(int id)
    {
        return Ok(_userService.GetAllFriends(id));
    }
    
    [HttpGet("GetFriendsRequest/{id:int}")]
    public ActionResult<IEnumerable<UserDTO>> GetFriendsRequest(int id)
    {
        return Ok(_userService.GetFriendsRequest(id));
    }
    
    [HttpPost("Register")]
    public ActionResult<UserDTO> Create(UserForm form) 
    { 
        UserDTO user = _userService.Create(form);

        return user == null ? BadRequest() : Ok(user);
    }

    [HttpPost("AddFriend")]
    public ActionResult CreateFriendRequest(CreateFriendRequestForm requestForm)
    {
        bool result = _userService.CreateFriendRequest(requestForm);

        return result ? NoContent() : StatusCode(422, "Unable to process the request");
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UserForm form)
    {
        bool result = _userService.Update(id, form);

        return result ? NoContent() : BadRequest();
    }
}