using System;
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

        public IActionResult OnGet(int? restaurantId)
        {
            //Get all enums
            this.Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                this.Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                this.Restaurant = new Restaurant();
            }

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        //Model binding an HTTP POST
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                this.Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (this.Restaurant.Id > 0)
            {
                this.restaurantData.Update(Restaurant);
            }
            else
            {
                this.restaurantData.Add(Restaurant);
            }
            this.restaurantData.Commit();

            //Message which says that restaurant is saved.
            TempData["Message"] = "Restaurant saved!";

            //When the Save button is clicked it is redirect to detail page and pass Id
            //because Detail page needs Id parameter.
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}