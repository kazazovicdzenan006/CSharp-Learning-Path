using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class AuthService : BaseService
    {
        private readonly UserManager<SystemCityUser> _userManager;
        private readonly RoleManager<SystemCityRole> _roleManager;

        public AuthService(UserManager<SystemCityUser> user, 
            RoleManager<SystemCityRole> role, 
            IServiceProvider provider) : base(provider)
        {
            _userManager = user;
            _roleManager = role;
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


    

    public async Task<SystemCityUser> Login(LoginDto dto)
        {
            await ValidateAsync(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) throw new Exception("Invalid Email or Password");

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!result) throw new Exception("Invalid Email or Password");

            return user;


        }
    }
}
