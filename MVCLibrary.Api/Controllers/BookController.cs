using Microsoft.AspNetCore.Mvc;
using MVCLibrary.Data;
using MVCLibrary.DTO;
using MVCLibrary.Services.ServiceClasses;
using MVCLibrary.Services.ServiceInterfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCLibrary.Api.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        /////////////////////////////////////index/////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            var data = await _bookService.GetBooks();
            return View(data);
        }

        /////////////////////////////////////create/////////////////////////////////////////
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AuthorName,Price,LibraryId,CategoryId")] BookDTO book)
        {
           book.BookDetails = new System.Collections.Generic.List<BookDetailDTO?>();

            if (ModelState.IsValid)
            {
                await _bookService.Create(book);
                return RedirectToAction(nameof(Index));
            }
            var data = ModelState.Values.SelectMany(v => v.Errors);
            return View(book);
        }

        /////////////////////////////////////Update/////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            var book = await _bookService.GetBook(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AuthorName,Price,LibraryId,CategoryId")] BookDTO book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.Update(book);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }


        /// //////////////////////////////////Detail//////////////////////////////////////////////////

        public async Task<IActionResult> Details(int? id)
        {
            BookDTO book = await _bookService.GetBook(id.Value);
            return View(book);
        }

        /////////////////////////////////////Delete//////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _bookService.GetBook(id.Value);
            return View(data);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
