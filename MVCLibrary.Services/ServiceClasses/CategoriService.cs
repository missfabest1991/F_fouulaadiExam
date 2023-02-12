using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Infrastructure;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.Services.ServiceClasses
{
    public class CategoriService : ICategoryService
    {



        private readonly IUnitOfWork _unitOfWork;
        public CategoriService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /////////////////////////////////////////////////MapToDTO///////////////////////////////////////////////////////////

        public async Task<CategoryDTO> MapToDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };


        }
        /////////////////////////////////////////////////MapToEntity///////////////////////////////////////////////////////////

        public async Task<Category> MapToEntity(CategoryDTO category)
        {
            return await Task.Run(() => new Category()
            {
                Id = category.Id,
                Name = category.Name

            });
        }
        /////////////////////////////////////////////////CreateCategory///////////////////////////////////////////////////////////

        public async Task<CategoryDTO> CreateCategory(CategoryDTO category)
        {
            var data = await MapToEntity(category);
            await _unitOfWork.CategoryRepository.Add(data);
            await _unitOfWork.Commit();
            return category;
        }
        /////////////////////////////////////////////////GetAllCategoryWithPagination///////////////////////////////////////////////////////////

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoryWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var categories = await _unitOfWork.CategoryRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(s => s.Name);
                    break;
                default:
                    categories = categories.OrderBy(s => s.Name);
                    break;
            }
            pageSize = pageSize.HasValue ? pageSize : 0;
            int pageNumber = (page ?? 1);
            categories = categories.Skip(pageSize.Value * pageNumber).Take(pageSize.Value);
            var ttt = categories != null && categories.Any() ? categories.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<CategoryDTO>();
            return ttt;
        }

        /////////////////////////////////////////////////GetCategories///////////////////////////////////////////////////////////

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            IEnumerable<Category> datas = await _unitOfWork.CategoryRepository.GetAll();
            var ttt = datas != null && datas.Any() ? datas.Select(x => MapToDTO(x).Result).AsEnumerable() : Enumerable.Empty<CategoryDTO>();
            return ttt;
        }

        /////////////////////////////////////////////////GetCategory///////////////////////////////////////////////////////////


        public async Task<CategoryDTO> GetCategory(int id)
        {
            var data = await _unitOfWork.CategoryRepository.GetById(id);
            return await MapToDTO(data);
        }

        /////////////////////////////////////////////////UpdateCategory///////////////////////////////////////////////////////////


        public async Task<CategoryDTO> UpdateCategory(CategoryDTO category)
        {
            var data = await _unitOfWork.CategoryRepository.Add(await MapToEntity(category));
            _unitOfWork.Commit();
            return await MapToDTO(data);
        }

        /////////////////////////////////////////////////RemoveCategory///////////////////////////////////////////////////////////


        public async Task RemoveCategory(int categoryId)
        {
            await _unitOfWork.CategoryRepository.Delete(await _unitOfWork.CategoryRepository.GetById(categoryId));
            await _unitOfWork.Commit();
        }


    }
}
