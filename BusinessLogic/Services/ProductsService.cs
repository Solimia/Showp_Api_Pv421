using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> repo;
        private readonly IMapper mapper;

        public ProductsService(IRepository<Product> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task<ProductDto> Create(CreateProductDto model)
        {
            var entity = mapper.Map<Product>(model);

            await repo.AddAsync(entity);

            return mapper.Map<ProductDto>(entity);
        }

        public async Task Delete(int id)
        {
            if (id < 0)
                throw new HttpException("Id can not be negative.", HttpStatusCode.BadRequest); 

            var item = await repo.GetByIdAsync(id);

            if (item == null)
                throw new HttpException($"Product with id:{id} not found.", HttpStatusCode.NotFound); 

            await repo.DeleteAsync(item);
        }

        public async Task Edit(EditProductDto model)
        {

            await repo.UpdateAsync(mapper.Map<Product>(model));
        }

        public async Task<ProductDto?> Get(int id)
        {
            if (id < 0)
                return null; 

            var item = await repo.GetByIdAsync(id);

            if (item == null)
                return null;

            return mapper.Map<ProductDto>(item);
        }

        public async Task<IList<ProductDto>> GetAll(int? filterCategoryId, string? searchTitle) 
        {

            var filterEx = PredicateBuilder.New<Product>(true);

            if (filterCategoryId != null)
                filterEx = filterEx.And(x => x.CategoryId == filterCategoryId);

            if (!string.IsNullOrWhiteSpace(searchTitle))
                filterEx = filterEx.And(x => x.Title.ToLower().Contains(searchTitle.ToLower()));

            var items = await repo.GetAllAsync(filtering: filterEx, includes: nameof(Product.Category));

            return mapper.Map<IList<ProductDto>>(items);
        }
    }
}
