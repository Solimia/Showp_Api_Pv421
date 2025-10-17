using BusinessLogic.DTOs.Accounts;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Showp_Api_Pv421.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase

    {
        private readonly IAccountsService accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        [HttpGet]

        public async Task<IActionResult> Register(RegisterModel model)
        {
            await accountsService.Register(model);
            return Ok();
        }
        [HttpGet]

        public async Task<IActionResult> Login(LoginModel model)
        {
            await accountsService.Login(model);
            return Ok();
        }
        [HttpGet]

        public async Task<IActionResult> Logout(LogoutModel model)
        {
            await accountsService.Logout(model);
            return Ok();
        }
    }

}
