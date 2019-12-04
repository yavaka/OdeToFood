using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this._db = db;
        }


        public Restaurant Add(Restaurant newRestaurant)
        {
            _db.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            //Add updated restaurant to Restaurants db set
            var entity = _db.Restaurants.Attach(updatedRestaurant);
            //Tell EF that this entity is changed
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                _db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return _db.Restaurants
                .Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            if (name != null)
            {
                return _db.Restaurants
                    .Where(n => n.Name.StartsWith(name))
                    .OrderBy(n => n.Name)
                    .ToList();
            }
            else
            {
                return _db.Restaurants
                    .OrderBy(n => n.Name)
                        .ToList();
            }
        }

        public int GetCountOfRestaurants()
        {
            return _db.Restaurants.Count();
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
        
    }
}
