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
    public class ProvinceController : Controller
    {
        private readonly IProvinceService _provinceService;
        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        // GET: Province
        public async Task<IActionResult> Index()
        {
            var data = await _provinceService.GetProvinces();

            return View(data);
        }

        // GET: Province/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProvinceName")] ProvinceDTO province)
        {
            province.Cities = new System.Collections.Generic.List<CityDTO?>();
            if (ModelState.IsValid)
            {
                await _provinceService.AddProince(province);
                return RedirectToAction(nameof(Index));
            }
            var data = ModelState.Values.SelectMany(v => v.Errors);
            return View(province);
        }

        // GET: Province/Edit/5
        public async Task<IActionResult> Edit(int? provinceId)
        {
            var province = await _provinceService.GetProvince(provinceId.Value);
            if (province == null)
            {
                return NotFound();
            }
            return View(province);
        }

        // POST: Province/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int provinceId, [Bind("ProvinceId,ProvinceName")] ProvinceDTO province)
        {
            if (provinceId != province.ProvinceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _provinceService.UpdateProvince(province);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(province);
        }

        // GET: Province/Delete/5
        public async Task<IActionResult> Delete(int? provinceId)
        {
            var data = await _provinceService.GetProvince(provinceId.Value);
            return View(data);
        }

        // POST: Province/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int provinceId)
        {
            await _provinceService.RemoveProvince(provinceId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProvinceWithPagination(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentFilter = searchString;

            var datas = _provinceService.GetAllProvinceWithPagination(sortOrder, currentFilter, searchString, page, pageSize);
            return View(nameof(Index), datas);
        }
    }
}
