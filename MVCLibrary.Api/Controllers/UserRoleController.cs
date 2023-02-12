using Microsoft.AspNetCore.Mvc;
using MVCLibrary.Services.ServiceInterfaces;
using System.Threading.Tasks;
using System;
using MVCLibrary.DTO;
using MVCLibrary.Services.ServiceClasses;
using MVCLibrary.Data;
using System.Linq;

namespace MVCLibrary.Api.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService _userRoleServic;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleServic = userRoleService;
        }



        public async Task<IActionResult> Index()
        {
            var data = await _userRoleServic.GetUserRoleies();

            return View(data);
        }



        public async Task<IActionResult> Details(int? id)
        {
            UserRoleDTO userRole = await _userRoleServic.GetUserRole(id.Value);
            return View(userRole);
        }




        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] UserRoleDTO userRole)
        {

            userRole.Users = new System.Collections.Generic.List<UserDTO?>();
            if (ModelState.IsValid)
            {
                await _userRoleServic.AddUserRole(userRole);
                return RedirectToAction(nameof(Index));
            }
            var data = ModelState.Values.SelectMany(v => v.Errors);

            return View(userRole);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            var userRole = await _userRoleServic.GetUserRole(id.Value);
            if (userRole == null)
            {
                return NotFound();
            }
            return View(userRole);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] UserRoleDTO userRole)
        {
            if (id != userRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userRoleServic.UpdatUserRole(userRole);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userRole);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _userRoleServic.GetUserRole(id.Value);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletConfirmed(int id)
        {
            await _userRoleServic.RemoveUserRole(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllUserRoleWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentFilter = searchString;

            var datas = _userRoleServic.GetAllUserRoleWithPagination(sortOrder, currentFilter, searchString, page, pageSize);
            return View(nameof(Index), datas);
        }
    }
}
