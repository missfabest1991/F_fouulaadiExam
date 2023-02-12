using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Services.ServiceInterfaces;

namespace MVCLibrary.Services.ServiceClasses
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> MapToDTO(Category category)
        {
            return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<Category> MapToEntity(CategoryDTO category)
        {
            return await Task.Run(() => new Category()
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO category)
        {
            var data = await MapToEntity(category);
            await _categoryRepository.Add(data);
            await _categoryRepository.();
            return category;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var datas = await _categoryRepository.GetAll();
            return datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : null;
        }

        public async Task<CategoryDTO> GetCategory(int categoryId)
        {
            var data = await _categoryRepository.GetById(categoryId);
            return await GetCategory(categoryId);
        }

        public async Task RemoveCategory(int categoryId) => await _categoryRepository.Delete(await MapToEntity(await GetCategory(categoryId)));

        public async Task<CategoryDTO> UpdateCategory(CategoryDTO category)
        {
            var data = await _categoryRepository.Update(await MapToEntity(category));
            return await MapToDTO(data);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoryWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pagesize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var categories = await _categoryRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    categories = categories.OrderBy(s => s.Name).ToList();
                    break;
            }
            pagesize = pagesize.HasValue ? pagesize : 0;
            int pageNumber = (page ?? 1);
            categories = categories.Skip(pagesize.Value * pageNumber).Take(pagesize.Value);
            return categories != null && categories.Any() ? categories.Select(x => MapToDTO(x).Result).AsEnumerable() : null;
        }


    }
}
