using Microsoft.AspNetCore.Mvc;
using Prova.MarQ.Domain.Entities;

namespace Prova.MarQ.API.Controllers.User_Controller
{
    public interface IUserController
    {
        Task<IActionResult> AddUser(User user);
        Task<IActionResult> GetUserByName(string name);
        Task<IActionResult> Login(string username, string password);
    }
}
