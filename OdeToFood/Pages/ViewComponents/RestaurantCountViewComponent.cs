using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Pages.ViewComponents
{
    public class RestaurantCountViewComponent : ViewComponent
    {
        private readonly IRestaurantData _restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            this._restaurantData = restaurantData;
        }

        public IViewComponentResult Invoke()
        {
            var count = this._restaurantData.GetCountOfRestaurants();
            return View(count);
        }
    }
}
