﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        public DeleteModel(IRestaurantData restaurantData)
        {
            this._restaurantData = restaurantData;
        }

        public Restaurant Restaurant{ get; set; }

        public IActionResult OnGet(int restaurantId)
        {
            this.Restaurant = _restaurantData.GetById(restaurantId);

            if (this.Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int restaurantId)
        {
            var restaurant = _restaurantData.Delete(restaurantId);
            _restaurantData.Commit();

            if (restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            //Message which says that restaurant is deleted.
            TempData["Message"] = $"{restaurant.Name} deleted!";
            return RedirectToPage("./List");
        }
    }
}