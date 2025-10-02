using DataAccess.Data;
using DataAccess.Data.Entities;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Showp_Api_Pv421.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    { 
        private readonly ShopDbContext ctx;
        public ProductsController(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
           var items = ctx.Products.ToList();

              return Ok(items);
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
        public IActionResult Create([FromBody]Product model)
        {  

            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorMessages());
            }

            ctx.Products.Add(model);
            ctx.SaveChanges();


            return CreatedAtAction(nameof(Get),
                new { id = model.Id },
                model
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
