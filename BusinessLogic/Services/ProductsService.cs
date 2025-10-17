using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data;
using System.Net;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ShopDbContext ctx;
        private readonly IMapper mapper;

        public ProductsService(ShopDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        public ProductDto Create(CreateProductDto model)
        {
            var entity = mapper.Map<Product>(model);

            ctx.Products.Add(entity);
            ctx.SaveChanges();

            return mapper.Map<ProductDto>(entity);


        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new HttpException("Id can not negative", HttpStatusCode.BadRequest); 
            }
            var item = ctx.Products.Find(id);

            if (item == null)
            {
                throw new HttpException($"Product with id {id} not found", HttpStatusCode.NotFound);
            }

            ctx.Products.Remove(item);
            ctx.SaveChanges(true);

        }

        public void Edit(EditProductDto model)
        {
            ctx.Products.Update(mapper.Map<Product>(model));
            ctx.SaveChanges();
        }

        public void Edit(Product model)
        {
            throw new NotImplementedException();
        }

        public ProductDto? Get(int id)
        {

            if (id < 0)
            {
                return null;
            }
            var item = ctx.Products.Find(id);

            if (item == null)
            {
                return null;
            }

            return mapper.Map<ProductDto>(item);
        }

        public IList<ProductDto> GetAll()
        {
                var items = ctx.Products
                .Include(x => x.Category)
                .ToList();

            return mapper.Map<IList<ProductDto>>(items);
        }

    }
}
