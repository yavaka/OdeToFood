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
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        int Commit();
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



        //Detail page: gets the restaurant by Id
        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(i => i.Id == id);
        }

        //Restaurant list page: gets all restaurants by name given in the search bar, 
        //If name equal to null return all restaurants.
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

        //Edit page: Model binding an HTTP POST
        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(i =>i.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        //Create page which is part of Edit page: 
        public Restaurant Add(Restaurant newRestaurant)
        {
            this.restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(i => i.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
