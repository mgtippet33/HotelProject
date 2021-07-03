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
    public class RoomService : IService<RoomDTO>
    {
        private IWorkUnit DataBase { get; set; }
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public RoomService(IWorkUnit database)
        {
            this.DataBase = database;
            fromDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RoomDTO, Room>().ReverseMap();
                    cfg.CreateMap<PriceCategoryDTO, PriceCategory>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, Category>().ReverseMap();
                }).CreateMapper();

            toDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
        }

        public IEnumerable<RoomDTO> GetAll()
        {
            return toDTOMapper.Map<IEnumerable<Room>, List<RoomDTO>>(DataBase.Rooms.GetAll());
        }

        public RoomDTO Get(int roomID)
        {
            var room = DataBase.Rooms.Get(roomID);
            return toDTOMapper.Map<Room, RoomDTO>(room);
        }

        public void Create(RoomDTO room)
        {
            var data = fromDTOMapper.Map<RoomDTO, Room>(room);
            DataBase.Rooms.Create(data);
            DataBase.Save();
        }

        public void Update(int roomID, RoomDTO room)
        {
            var data = fromDTOMapper.Map<RoomDTO, Room>(room);
            DataBase.Rooms.Update(roomID, data);
            DataBase.Save();
        }

        public void Delete(int roomID)
        {
            DataBase.Rooms.Delete(roomID);
            DataBase.Save();
        }

        public bool Check(RoomDTO room)
        {
            var data = fromDTOMapper.Map<RoomDTO, Room>(room);
            var result = DataBase.Rooms.Search(data);
            return result;
        }
    }
}
