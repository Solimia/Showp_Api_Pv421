using BusinessLogic.DTOs.Accounts;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Showp_Api_Pv421.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase

    {
        private readonly IAccountsService accountsService;

        private string? CurrentIp => HttpContext.Connection.RemoteIpAddress?.ToString() ;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        [HttpGet("register")]

        public async Task<IActionResult> Register(RegisterModel model)
        {
            await accountsService.Register(model);
            return Ok();
        }
        [HttpGet("Login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var res = await accountsService.Login(model, CurrentIp);
            return Ok(res);
        }
        [HttpGet("logout")]

        public async Task<IActionResult> Logout(LogoutModel model)
        {
            await accountsService.Logout(model);
            return Ok();
        }


        [HttpGet("refresh")]

        public async Task<IActionResult> Refresh(RefreshRequest model)
        {
           
            return Ok(await accountsService.Refresh(model, CurrentIp));
        }
    }

}
