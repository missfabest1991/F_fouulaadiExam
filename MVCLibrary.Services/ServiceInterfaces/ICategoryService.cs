using MVCLibrary.Data;
using MVCLibrary.DTO;

namespace MVCLibrary.Services.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<Category> MapToEntity(CategoryDTO category);
        Task<CategoryDTO> MapToDTO(Category category);
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategory(int id);
        Task<CategoryDTO> CreateCategory(CategoryDTO category);
        Task RemoveCategory(int categoryId);
        Task<CategoryDTO> UpdateCategory(CategoryDTO category);
        Task<IEnumerable<CategoryDTO>> GetAllCategoryWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize);
        
    }
}
