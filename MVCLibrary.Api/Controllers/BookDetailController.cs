using Microsoft.AspNetCore.Mvc;
using MVCLibrary.DTO;
using MVCLibrary.Services.ServiceInterfaces;
using System.Threading.Tasks;
using System;
using MVCLibrary.Data;
using System.Linq;

namespace MVCLibrary.Api.Controllers
{
    public class BookDetailController : Controller
    {
       

        private readonly IBookDetailService _bookDetailService;
        public BookDetailController(IBookDetailService bookDetailService)
        {
            _bookDetailService = bookDetailService;
        }


        /////////////////////////////////////index/////////////////////////////////////////////
        //public async Task<IActionResult> Index() => View(await _bookDetailService.GetBookDetails());
        public IActionResult Index()
        {
            var data = _bookDetailService.GetBookDetails();
            return View(data);
        }



        /////////////////////////////////////create/////////////////////////////////////////
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublishDateTime,CountEdition,Description,BookId")] BookDetailDTO bookDetail)
        {
            

            if (ModelState.IsValid)
            {
                await _bookDetailService.Create(bookDetail);
                return RedirectToAction(nameof(Index));
            }
           
            return View(bookDetail);
        }

        /////////////////////////////////////Update/////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            var bookDetail = await _bookDetailService.GetBookDetail(id.Value);
            if (bookDetail == null)
            {
                return NotFound();
            }
            return View(bookDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PublishDateTime,CountEdition,Description,BookId")] BookDetailDTO BookDetail)
        {
            if (id != BookDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookDetailService.Update(BookDetail);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(BookDetail);
        }


        /// //////////////////////////////////Detail//////////////////////////////////////////////////

        public async Task<IActionResult> Details(int? id)
        {
            BookDetailDTO bookDetail = await _bookDetailService.GetBookDetail(id.Value);
            return View(bookDetail);
        }

        /////////////////////////////////////Delete//////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _bookDetailService.GetBookDetail(id.Value);
            return View(data);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookDetailService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
