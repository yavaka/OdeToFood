using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();

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

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants
                .OrderBy(n => n.Name)
                .ToList();
        }
    }
}
