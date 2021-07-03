using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public class PriceCategoryService : IService<PriceCategoryDTO>
    {
        private IWorkUnit DataBase { get; set; }
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public PriceCategoryService(IWorkUnit database)
        {
            this.DataBase = database;
            fromDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategoryDTO, PriceCategory>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, Category>().ReverseMap();
                }).CreateMapper();

            toDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
        }

        public IEnumerable<PriceCategoryDTO> GetAll()
        {
            return toDTOMapper.Map<IEnumerable<PriceCategory>, List<PriceCategoryDTO>>(DataBase.PriceCategories.GetAll());
        }

        public PriceCategoryDTO Get(int id)
        {
            var priceCategory = DataBase.PriceCategories.Get(id);
            return toDTOMapper.Map<PriceCategory, PriceCategoryDTO>(priceCategory);
        }

        public void Create(PriceCategoryDTO item)
        {
            var data = fromDTOMapper.Map<PriceCategoryDTO, PriceCategory>(item);
            DataBase.PriceCategories.Create(data);
            DataBase.Save();
        }

        public void Update(int id, PriceCategoryDTO item)
        {
            var data = fromDTOMapper.Map<PriceCategoryDTO, PriceCategory>(item);
            DataBase.PriceCategories.Update(id, data);
            DataBase.Save();
        }

        public void Delete(int id)
        {
            DataBase.PriceCategories.Delete(id);
            DataBase.Save();
        }

        public bool Check(PriceCategoryDTO item)
        {
            var data = fromDTOMapper.Map<PriceCategoryDTO, PriceCategory>(item);
            var result = DataBase.PriceCategories.Search(data);
            return result;
        }
    }
}
