using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userDtos.Add(new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Telephone = user.PhoneNumber,
                    Roles = roles.ToList()
                });
            }

            return userDtos;
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Telephone = user.PhoneNumber,
                Roles = roles.ToList()
            };
        }

        public async Task CreateUserAsync(UserDTO userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.Telephone,
                EmailConfirmed = true 
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                if (userDto.Roles != null && userDto.Roles.Any())
                {
                    await _userManager.AddToRolesAsync(user, userDto.Roles);
                }
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Помилка створення користувача: {errors}");
            }
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null) throw new Exception("Користувача не знайдено");


            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.Telephone;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                throw new Exception($"Помилка оновлення: {errors}");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var newRoles = userDto.Roles ?? new List<string>();

            var rolesToAdd = newRoles.Except(currentRoles);

            var rolesToRemove = currentRoles.Except(newRoles);

            if (rolesToAdd.Any())
            {
                await _userManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToRemove.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task<UserDTO> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                };
            }
            return null;
        }
    }
}
