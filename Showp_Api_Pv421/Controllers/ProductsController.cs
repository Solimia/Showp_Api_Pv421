using BusinessLogic.DTOs;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
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
            if (id <= 0)
            {
                return BadRequest("Id must be greater than zero");
            }
            var item = ctx.Products.Find(id);

            if (item == null)
            {
                return NotFound($"Item with id  not found");
            }

            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(CreateProductDto model)
        {  

            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorMessages());
            }

            //var entity = new Product()
            //{
            //    Title = model.Title,
            //    ImageUrl = model.ImageUrl,
            //    Price = model.Price,
            //    Discount = model.Discount,
            //    Quantity = model.Quantity,
            //    Description = model.Description,
            //    CategoryId = model.CategoryId
            //};

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

            ctx.Products.Update(model);
            ctx.SaveChanges();

            return Ok();
        }

        //public IActionResult Edit()
        //{

        //}
        //public IActionResult Delete(int, id)
        //{

        //}

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
        }
    }
}
