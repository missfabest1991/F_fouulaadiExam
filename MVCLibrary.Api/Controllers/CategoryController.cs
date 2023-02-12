using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCLibrary.DTO;
using MVCLibrary.Services.ServiceInterfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCLibrary.Api.Controllers
{
    public class CategoryController : Controller
    {
        #region Ostad
        //private readonly ICategoryService _CategoryService;

        //public CategoryController(ICategoryService categoryService)
        //{
        //    _CategoryService = categoryService;
        //}

        //public async Task<IActionResult> Index() => View(await _CategoryService.GetCategories());

        //public async Task<IActionResult> Details(int? id)
        //{
        //    CategoryDTO category = await _CategoryService.GetCategory(id.Value);
        //    return View(category);
        //}

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public async Task<IActionResult> Create([Bind("Id,name")] CategoryDTO category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _CategoryService.AddCategory(category);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        //}


        //public async Task<IActionResult> Edit(int? id)
        //{
        //    var category = await _CategoryService.GetCategory(id.Value);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}


        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,name")] CategoryDTO category)
        //{
        //    if (id != category.Id)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _CategoryService.UpdateCategory(category);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex.InnerException);
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        //}

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    var data = await _CategoryService.GetCategory(id.Value);
        //    return View(data);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _CategoryService.RemoveCategory(id);
        //    return RedirectToAction(nameof(Index));
        //}



        //public async Task<IActionResult> GetAllCategoryWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        //{
        //    ViewBag.Currentsort = sortOrder;
        //    ViewBag.Namesortparm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortparm = sortOrder == "Date" ? "date_desc" : "Date";
        //    ViewBag.CurrentFilter = searchString;

        //    var datas = _CategoryService.GetAllCategoryWithPagination(sortOrder, currentFilter, searchString, page, pageSize);
        //    return View(nameof(index), datas);
        //}
        #endregion


        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        /////////////////////////////////////index/////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            var data = await _categoryService.GetCategories();

            return View(data);
        }



        /////////////////////////////////////create/////////////////////////////////////////
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] CategoryDTO category)
        {

            category.Books = new System.Collections.Generic.List<BookDTO?>();
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            var data = ModelState.Values.SelectMany(v => v.Errors);
            return View(category);
        }

        /////////////////////////////////////Update/////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            var category = await _categoryService.GetCategory(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryDTO category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategory(category);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        /// //////////////////////////////////Detail//////////////////////////////////////////////////

        public async Task<IActionResult> Details(int? id)
        {
            CategoryDTO category = await _categoryService.GetCategory(id.Value);
            return View(category);
        }

        /////////////////////////////////////Delete//////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _categoryService.GetCategory(id.Value);
            return View(data);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.RemoveCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
