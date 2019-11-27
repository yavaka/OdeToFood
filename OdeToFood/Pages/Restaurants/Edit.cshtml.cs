﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        //Enum
        private readonly IHtmlHelper htmlHelper;

        public EditModel(IRestaurantData restaurantData,
            IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public IActionResult OnGet(int restaurantId)
        {
            //Get all enums
            this.Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            this.Restaurant = restaurantData.GetById(restaurantId);
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        //Model binding an HTTP POST
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                this.restaurantData.Update(Restaurant);
                this.restaurantData.Commit();
                //When the Save button is clicked it is redirect to detail page and pass Id
                //because Detail page needs Id parameter.
                return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
            }
            this.Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            return Page();
        }
    }
}