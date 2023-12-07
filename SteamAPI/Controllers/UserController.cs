using BLL.Interfaces;
using BLL.Models.Forms;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ASteamAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User/GetAllFriends/{id}
        [HttpGet("GetAllFriends/{id:int}")]
        public ActionResult<IEnumerable<UserDTO>> GetAllFriends(int id)
        {
            var friends = _userService.GetAllFriends(id);
            return Ok(friends);
        }

        // GET: api/User/GetFriendsRequest/{id}
        [HttpGet("GetFriendsRequest/{id:int}")]
        public ActionResult<IEnumerable<UserDTO>> GetFriendsRequest(int id)
        {
            var friendRequests = _userService.GetFriendsRequest(id);
            return Ok(friendRequests);
        }

        // POST: api/User/Register
        [HttpPost("Register")]
        public ActionResult<UserDTO> Create(UserForm form) 
        { 
            var user = _userService.Create(form);
            return user != null ? Ok(user) : BadRequest();
        }

        // POST: api/User/AddFriend
        [HttpPost("AddFriend")]
        public ActionResult CreateFriendRequest(CreateFriendRequestForm requestForm)
        {
            var result = _userService.CreateFriendRequest(requestForm);
            return result ? NoContent() : StatusCode(422, "Unable to process the request");
        }

        // PUT: api/User/{id}
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, UserForm form)
        {
            var result = _userService.Update(id, form);
            return result ? NoContent() : BadRequest();
        }

        // PUT: api/User/AddMoney
        [HttpPut("AddMoney")]
        public IActionResult AddMoney(AddMoneyForm form)
        {
            var result = _userService.AddMoney(form);
            return result ? Ok() : StatusCode(422, "Unable to process the request"); 
        }

        // PATCH: api/User/AcceptFriend
        [HttpPatch("AcceptFriend")]
        public IActionResult AcceptFriendRequest(AcceptFriendRequestForm form)
        {
            var result = _userService.AcceptFriendRequest(form);
            return result ? NoContent() : StatusCode(422, "Unable to process the request");
        }

        // DELETE: api/User/DeleteFriend
        [HttpDelete("DeleteFriend")]
        public IActionResult DeleteFriendRequest(DeleteFriendRequestForm form)
        {
            var result = _userService.DeleteFriendRequest(form);
            return result ? NoContent() : StatusCode(422, "Unable to process the request");
        }
    }
}
