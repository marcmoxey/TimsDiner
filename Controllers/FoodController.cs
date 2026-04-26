using DataLibrary.Data;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemoApp.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodData _foodData;

        public FoodController(IFoodData foodData)
        {
            _foodData = foodData;
        }

        public async Task<IActionResult> Index()
        {
            var food = await _foodData.GetFoods();
            return View(food);
        }
    }
}
