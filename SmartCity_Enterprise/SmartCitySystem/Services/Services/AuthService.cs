using Domain.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
using Services.DTOs.IdentityDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class AuthService : BaseService
    {
        private readonly UserManager<SystemCityUser> _userManager;
        private readonly RoleManager<SystemCityRole> _roleManager;
        private readonly ITokenService _token; 

        public AuthService(UserManager<SystemCityUser> user, 
            RoleManager<SystemCityRole> role, 
            IServiceProvider provider,
            ITokenService token) : base(provider)
        {
            _userManager = user;
            _roleManager = role;
            _token = token;
        }


        public async Task Register(RegisterDto dto)
        {
            await ValidateAsync(dto);

            var user = new SystemCityUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration Failed: {errors}");
            }


            if(!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new SystemCityRole("User") { RoleDescription = "Standard role for everybody"}); 
            }

            await _userManager.AddToRoleAsync(user, "User");    

        }


    

    public async Task<UserDto> Login(LoginDto dto)
        {
            await ValidateAsync(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) throw new Exception("Invalid Email or Password");

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!result) throw new Exception("Invalid Email or Password");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _token.CreateToken(user, roles);

            return new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Roles = roles,
                Token = token
            };


        }

        public async Task AssignAdminRole(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found");

            if(!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new SystemCityRole("Admin")
                {
                    RoleDescription = "Role with all privileges"
                }); 
              
            }
            await _userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
