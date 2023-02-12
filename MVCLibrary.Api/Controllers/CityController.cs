using Microsoft.AspNetCore.Mvc;
using MVCLibrary.DTO;
using MVCLibrary.Services.ServiceInterfaces;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MVCLibrary.Api.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }


        /////////////////////////////////////index/////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            var data = await _cityService.GetCities();

            return View(data);
        }



        /////////////////////////////////////create/////////////////////////////////////////
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] CityDTO city)
        {

         
            if (ModelState.IsValid)
            {
                await _cityService.Create(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        /////////////////////////////////////Update/////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            var city = await _cityService.GetCity(id.Value);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,Name")] CityDTO city)
        {
            if (id != city.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cityService.Update(city);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }


        /// //////////////////////////////////Detail//////////////////////////////////////////////////

        public async Task<IActionResult> Details(int? id)
        {
            CityDTO city = await _cityService.GetCity(id.Value);
            return View(city);
        }

        /////////////////////////////////////Delete//////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _cityService.GetCity(id.Value);
            return View(data);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cityService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
