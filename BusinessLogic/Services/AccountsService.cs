using BusinessLogic.DTOs.Accounts;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly ShopDbContext ctx;

        public AccountsService(ShopDbContext ctx)
        {
            this.ctx = ctx;
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
            throw new NotImplementedException();
            //ctx.Users.Add
        }

    }
}
