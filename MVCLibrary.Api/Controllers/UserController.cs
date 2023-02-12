using Microsoft.AspNetCore.Mvc;
using MVCLibrary.DTO;
using MVCLibrary.Services.ServiceClasses;
using MVCLibrary.Services.ServiceInterfaces;
using System;
using System.Threading.Tasks;

namespace MVCLibrary.Api.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userServic;
        public UserController(IUserService userService)
        {
            _userServic = userService;
        }


        public async Task<IActionResult> Index()
        {
            var data = await _userServic.GetUseries();

            return View(data);
        }

        public async Task<IActionResult> Details(int? id)
        {
            UserDTO user = await _userServic.GetUser(id.Value);
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userServic.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userServic.GetUser(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserRoleId")] UserDTO user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userServic.UpdatUser(user);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _userServic.GetUser(id.Value);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletConfirmed(int id)
        {
            await _userServic.RemoveUser(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllUserWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentFilter = searchString;

            var datas = _userServic.GetAllUserWithPagination(sortOrder, currentFilter, searchString, page, pageSize);
            return View(nameof(Index), datas);
        }
    }
}
