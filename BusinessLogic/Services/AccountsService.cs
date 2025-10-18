using AutoMapper;
using BusinessLogic.DTOs.Accounts;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        //private readonly ShopDbContext ctx;

        public AccountsService(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public Task Login(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task Logout(LogoutModel model)
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterModel model)
        {
            var user = mapper.Map<User>(model);

            userManager.CreateAsync(model, model.Password);

        }

    }
}
