using dotNetLabs.Repositories;
using dotNetLabs.Server.Infrastructure;
using dotNetLabs.Server.Models.Models;
using dotNetLabs.Shared;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Server.Services
{
    public interface IUsersService
    {

        Task<OperationResponse<string>> RegisterUserAsync(RegisterRequest model);

        Task<LoginResponse> GenerateTokenAsync(LoginRequest model);

    }

    public class UsersService : IUsersService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthOptions _authOptions; 

        public UsersService(IUnitOfWork unitOfWork, AuthOptions authOptions)
        {
            _unitOfWork = unitOfWork;
            _authOptions = authOptions;
        }

        public async Task<LoginResponse> GenerateTokenAsync(LoginRequest model)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(model.Email); 
            if(user == null)
            {
                return new LoginResponse
                {
                    Message = "Invalid username or password",
                    IsSuccess = false
                };
            }

            if(!(await _unitOfWork.Users.CheckPasswordAsync(user, model.Password)))
            {
                return new LoginResponse
                {
                    Message = "Invalid username or password",
                    IsSuccess = false
                };
            }

            var userRole = await _unitOfWork.Users.GetUserRoleAsync(user);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userRole),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
            var expireDate = DateTime.Now.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                claims: claims,
                expires: expireDate,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse
            {
                Message = "Welcome to dotNet Labs",
                IsSuccess = true,
                AccessToken = tokenAsString,
                ExpiryDate = expireDate
            };
        }

        public async Task<OperationResponse<string>> RegisterUserAsync(RegisterRequest model)
        {
            var userByEmail = await _unitOfWork.Users.GetUserByEmailAsync(model.Email); 
            if(userByEmail != null)
            {
                return new OperationResponse<string>
                {
                    IsSuccess = false,
                    Message = "User is already exists",
                };
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            await _unitOfWork.Users.CreateUserAsync(user, model.Password, "User");

            return new OperationResponse<string>
            {
                Message = "Welcome to donNet Labs",
                IsSuccess = true
            };
        }
    }
}
