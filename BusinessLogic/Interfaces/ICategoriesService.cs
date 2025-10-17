namespace BusinessLogic.Interfaces
{
    public interface ICategoriesService
    {
        Task<IList<CategoryDto>> Get(int pageIndex);

        Task<CategoryDto> GetById(int id);

        Task<CategoryDto> Create(CategoryDto model);

        Task Edit(CategoryDto model);

        Task Delete(int id);
    }
}