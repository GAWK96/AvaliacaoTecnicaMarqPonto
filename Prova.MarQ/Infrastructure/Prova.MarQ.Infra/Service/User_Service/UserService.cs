using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Prova.MarQ.Domain.Entities;
using Prova.MarQ.Infra.Loader.User_Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.MarQ.Infra.Service.User_Service
{
    public class UserService : IUserService
    {
        private readonly IUserLoader _userLoader;
        private readonly PasswordHasher<string> _passwordHasher;
        private readonly JwtTokenGenerator _tokenGenerator;

        public UserService(IUserLoader userLoader, JwtTokenGenerator tokenGenerator)
        {
            _userLoader = userLoader;
            _passwordHasher = new PasswordHasher<string>();
            _tokenGenerator = tokenGenerator;
        }

        public async Task AddUser(User user)
        {
            if (user.UserName == null || user.Password == null || user.Role == null)
            {
                throw new InvalidOperationException("All fields must be filled!");
            }
            await _userLoader.AddUser(user);
        }


        public async Task<User?> GetUserByName(string userName)
        {
            var getUserName = await _userLoader.GetUserByName(userName);
            if (getUserName == null)
            {
                throw new InvalidOperationException("User not found!");
            }
            return getUserName;
        }

        public async Task<string> UserLogin(string userName, string password)
        {
            var getUser = await _userLoader.GetUserByName(userName);
            string token = "Token empty";
            if (getUser == null)
            {
                throw new InvalidOperationException("Invalid username or password.");
            }
            var verifyHashedPassword = _userLoader.VerifyHashedPassword(getUser, password);
            if (verifyHashedPassword == PasswordVerificationResult.Success)
            {
                Console.WriteLine("hash password matched!");
                token = _tokenGenerator.GenerateToken(getUser, getUser.Role);
                return token;
            }
            if (verifyHashedPassword != PasswordVerificationResult.Success || getUser == null)
            {   
               throw new InvalidOperationException("User or login invalid!");
            }
            return token;
        }
    }
}
