using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemoApp.Models;

namespace MVCDemoApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;

        public OrdersController(IFoodData foodData, IOrderData orderData)
        {
            _foodData = foodData;
            _orderData = orderData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            // get food info 
            var food = await _foodData.GetFoods();

            OrderCreateModel model = new OrderCreateModel();

            food.ForEach(x =>
            {
                model.FoodItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Title });
            });
            
            return View(model);
        }

        [HttpPost]
        public async  Task<IActionResult> Create(OrderModel order)
        {
            if(ModelState.IsValid == false)
            {
                return View();
            }

            var food = await _foodData.GetFoods();

            order.Total = order.Quantity * food.Where(x => x.Id == order.FoodId).First().Price;

            int id = await _orderData.CreateOrder(order);

            return RedirectToAction("Create");
        }
    }
}
