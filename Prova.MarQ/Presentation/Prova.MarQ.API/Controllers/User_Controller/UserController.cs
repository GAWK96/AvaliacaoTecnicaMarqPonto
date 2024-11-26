using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Service.Employee_Service;
using Prova.MarQ.Infra.Service.User_Service;

namespace Prova.MarQ.API.Controllers.User_Controller
{
    public class UserController : ControllerBase, IUserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userService.AddUser(user);
            return Ok("User added successfully!");
        }

        [HttpGet("GetUserByName")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var getUserByName = await _userService.GetUserByName(name);
            return Ok(getUserByName);
        }
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            await _userService.UserLogin(username, password);
            return Ok("User logged successfully");
        }
    }
}
