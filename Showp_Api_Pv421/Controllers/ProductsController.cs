using DataAccess.Data;
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
        public IActionResult GetAll()
        {
           var items = ctx.Products.ToList();

              return Ok(items);
        }
        public IActionResult Get(int id)
        {
            var item = ctx.Products.Find(id);

            return Ok(item);
        }
        //public IActionResult Create()
        //{

        //}

        //public IActionResult Edit()
        //{

        //}
        //public IActionResult Delete(int, id)
        //{

        //}
    }
}
