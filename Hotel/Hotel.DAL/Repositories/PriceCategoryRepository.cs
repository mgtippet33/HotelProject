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
    class PriceCategoryRepository : IRepository<PriceCategory>
    {
        private HotelModel db;

        public PriceCategoryRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<PriceCategory> GetAll()
        {
            return db.PriceCategories;
        }

        public PriceCategory Get(int priceCategoryID)
        {
            return db.PriceCategories.Find(priceCategoryID);
        }

        public void Create(PriceCategory priceCategory)
        {
            priceCategory.CategoryName = db.Categories.Find(priceCategory.CategoryName.CategoryID);
            db.PriceCategories.Add(priceCategory);
        }

        public void Delete(int priceCategoryID)
        {
            PriceCategory priceCategory = Get(priceCategoryID);
            if (priceCategory != null)
            {
                db.PriceCategories.Remove(priceCategory);
            }
        }

        public void Update(int priceCategoryID, PriceCategory value)
        {
            var priceCategoryUpdate = db.PriceCategories.FirstOrDefault(m => m.PriceCategoryID == priceCategoryID);
            if (priceCategoryUpdate != null)
            {
                priceCategoryUpdate.CategoryName = db.Categories.Find(value.CategoryName.CategoryID) ?? priceCategoryUpdate.CategoryName;
                priceCategoryUpdate.PriceCategoryID = priceCategoryID;
                priceCategoryUpdate.Price = value.Price != priceCategoryUpdate.Price ? value.Price : priceCategoryUpdate.Price;
                priceCategoryUpdate.StartDate = value.StartDate != priceCategoryUpdate.StartDate ? value.StartDate : priceCategoryUpdate.StartDate;
                priceCategoryUpdate.EndDate = value.EndDate != priceCategoryUpdate.EndDate ? value.EndDate : priceCategoryUpdate.EndDate;
            }
        }

        public bool Search(PriceCategory priceCategory)
        {
            PriceCategory data = db.PriceCategories.SingleOrDefault(item => item.CategoryName.CategoryID == priceCategory.CategoryName.CategoryID &&
            item.StartDate == priceCategory.StartDate && item.EndDate == priceCategory.EndDate);
            if (data != null)
                return true;
            return false;
        }
    }
}
