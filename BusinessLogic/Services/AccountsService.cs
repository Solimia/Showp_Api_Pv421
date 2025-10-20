using AutoMapper;
using BusinessLogic.DTOs.Accounts;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        //private readonly ShopDbContext ctx;

        public AccountsService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new HttpException("Invalid email or password", HttpStatusCode.BadRequest);
            }

            await signInManager.SignInAsync(user, true);
        }

        public async Task Logout(LogoutModel model)
        {
            await signInManager.SignOutAsync();
        }

        public async Task Register(RegisterModel model)
        {
           var user = mapper.Map<User>(model);

           var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new HttpException(result.Errors.FirstOrDefault()?.Description ?? "Error", HttpStatusCode.BadRequest);
            }

        }

    }
}
