using AutoMapper;
using BusinessLogic.DTOs;
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
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public ProductsController(ShopDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
            
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
           var items = ctx.Products
                .Include(x => x.Category) 
                .ToList();

            return Ok(mapper.Map<IEnumerable<ProductDto>>(items));
        }
        [HttpGet]
        public IActionResult Get(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id must be greater than zero");
            }
            var item = ctx.Products.Find(id);

            if (item == null)
            {
                return NotFound($"Item with id  not found");
            }

            return Ok(mapper.Map<ProductDto>(item));
        }
        [HttpPost]
        public IActionResult Create([FromBody]CreateProductDto model)
        {  

            var entity = mapper.Map<Product>(model);

            ctx.Products.Add(entity);
            ctx.SaveChanges();

            var result = mapper.Map<ProductDto>(entity);
  

            return CreatedAtAction(
                nameof(Get),
                new { id = result.Id },
                result
                );
        }

        [HttpPut]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorMessages());
            }

            ctx.Products.Update(mapper.Map<Product>(model));
            ctx.SaveChanges();

            return Ok();
        }

        //public IActionResult Edit()
        //{

        //}


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id must be greater than zero");
            }
            var item = ctx.Products.Find(id);

            if (item == null)
            {
                return NotFound("Product not found");
            }

            ctx.Products.Remove(item);
            ctx.SaveChanges(true);

            return NoContent();
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
        }
    }
}
