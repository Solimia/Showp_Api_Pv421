using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showp_Api_PV421.Helpers;

namespace Showp_Api_Pv421.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int? filterCategoryId, string? searchTitle)
        {
            return Ok(await productsService.GetAll(filterCategoryId, searchTitle));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await productsService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            var result = await productsService.Create(model);

            return CreatedAtAction(
                nameof(Get),            
                new { id = result.Id }, 
                result                  
            );
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditProductDto model)
        {
   
            if (!ModelState.IsValid)
                return BadRequest(GetErrorMessages());

            await productsService.Edit(model);

            return Ok(); 
        }

        [Authorize(Roles = Roles.ADMIN, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await productsService.Delete(id);

            return NoContent(); 
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage);
        }
    }
}
