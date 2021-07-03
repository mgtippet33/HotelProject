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
    public class CategoryService : IService<CategoryDTO>
    {
        private IWorkUnit DataBase { get; set; }
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public CategoryService(IWorkUnit database)
        {
            this.DataBase = database;
            toDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            fromDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();
        }

        public bool Check(CategoryDTO category)
        {
            var data = fromDTOMapper.Map<CategoryDTO, Category>(category);
            var result = DataBase.Categories.Search(data);
            return result;
        }

        public void Create(CategoryDTO item)
        {
            var data = fromDTOMapper.Map<CategoryDTO, Category>(item);
            DataBase.Categories.Create(data);
            DataBase.Save();
        }

        public void Delete(int id)
        {
            DataBase.Categories.Delete(id);
            DataBase.Save();
        }

        public CategoryDTO Get(int id)
        {
            var category = DataBase.Categories.Get(id);
            return toDTOMapper.Map<Category, CategoryDTO>(category);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return toDTOMapper.Map<IEnumerable<Category>, List<CategoryDTO>>(DataBase.Categories.GetAll());
        }

        public void Update(int id, CategoryDTO item)
        {
            var data = fromDTOMapper.Map<CategoryDTO, Category>(item);
            DataBase.Categories.Update(id, data);
            DataBase.Save();
        }
    }
}
