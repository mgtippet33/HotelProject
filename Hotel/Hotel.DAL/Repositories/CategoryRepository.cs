using Hotel.DAL.EF;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    class CategoryRepository : IRepository<Category>
    {
        private HotelModel db;

        public CategoryRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public Category Get(int categoryID)
        {
            return db.Categories.Find(categoryID);
        }

        public void Create(Category category)
        {
            db.Categories.Add(category);
        }

        public void Delete(int categoryID)
        {
            Category category = Get(categoryID);
            if (category != null)
            {
                db.Categories.Remove(category);
            }
        }

        public void Update(int categoryID, Category value)
        {
            var categoryUpdate = db.Categories.FirstOrDefault(m => m.CategoryID == categoryID);
            if (categoryUpdate != null)
            {
                categoryUpdate.CategoryID = categoryID;
                categoryUpdate.CategoryName = value.CategoryName ?? categoryUpdate.CategoryName;
                categoryUpdate.Capacity = value.Capacity != categoryUpdate.Capacity ? value.Capacity : categoryUpdate.Capacity;
            }
        }

        public bool Search(Category category)
        {
            Category data = db.Categories.SingleOrDefault(cat => cat.CategoryName == category.CategoryName);
            if (data != null)
                return true;
            return false;
        }

    }
}
