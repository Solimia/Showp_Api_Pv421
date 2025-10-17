using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetAll()
        {


            return Ok(productsService.GetAll());
        }
        [HttpGet]
        public IActionResult Get(int id)
        { 
            return Ok(productsService.Get(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorMessages());
            }


            var result = productsService.Create(model);


            return CreatedAtAction(
                nameof(Get),
                new { id = result.Id },
                result
                );
        }

        [HttpPut]
        public IActionResult Edit(EditProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorMessages());
            }

            productsService.Edit(model);

            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
        
            productsService.Delete(id);  

            return NoContent();
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
        }
    }
}
