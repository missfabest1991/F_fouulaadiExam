using Microsoft.AspNetCore.Mvc;
using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Services.ServiceClasses;
using MVCLibrary.Services.ServiceInterfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCLibrary.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        // GET: Library

        public async Task<IActionResult> Index()
        {
            var data = await _libraryService.GetLibraries();

            return View(data);
        }


        // GET: Library/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            LibraryDTO library = await _libraryService.GetLibrary(id.Value);
            return View(library);
        }

        // GET: Library/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,PhoneNumber,EmailAddress,LibraryNumber")] LibraryDTO library)
        {
            library.Books = new System.Collections.Generic.List<BookDTO?>();
            if (ModelState.IsValid)
            {
                await _libraryService.AddLibrary(library);
                return RedirectToAction(nameof(Index));
            }
            var data = ModelState.Values.SelectMany(v => v.Errors);

            return View(library);
        }

        // GET: Library/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var library = await _libraryService.GetLibrary(id.Value);
            if (library == null)
            {
                return NotFound();
            }
            return View(library);
        }

        // POST: Library/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,PhoneNumber,EmailAddress,LibraryNumber")] LibraryDTO library)
        {
            if (id != library.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _libraryService.UpdateLibrary(library);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(library);
        }

        // GET: Library/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _libraryService.GetLibrary(id.Value);
            return View(data);
        }

        // POST: Library/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _libraryService.RemoveLibrary(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLibraryWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentFilter = searchString;

            var datas = _libraryService.GetAllLibraryWithPagination(sortOrder, currentFilter, searchString, page, pageSize);
            return View(nameof(Index), datas);
        }
    }
}
