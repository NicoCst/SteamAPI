using BLL.Interfaces;
using BLL.Models.Forms;
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
    
    [HttpPost]
    public ActionResult<UserDTO> Create(UserForm form) 
    { 
        UserDTO user = _userService.Create(form);

        return user == null ? BadRequest() : Ok(user);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UserForm form)
    {
        bool result = _userService.Update(id, form);

        return result ? NoContent() : BadRequest();
    }
}