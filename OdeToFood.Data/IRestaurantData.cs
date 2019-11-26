using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);

    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant {Id=1, Name="Domino`s Pizza", Location = "High Wycombe", Cuisine = CuisineType.Italian },
                new Restaurant {Id=2, Name="Bulgarian Village Kitchen", Location = "Birmingham", Cuisine = CuisineType.Bulgarian},
                new Restaurant {Id=3, Name="Peri Peri", Location = "High Wycombe", Cuisine = CuisineType.Indian},
            };
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            if (name != null)
            {
                return restaurants
                    .Where(n => n.Name.StartsWith(name))
                    .OrderBy(n => n.Name)
                    .ToList();
            }
            else
            {
                return restaurants
                    .OrderBy(n => n.Name)
                        .ToList();
            }
        }
    }
}
