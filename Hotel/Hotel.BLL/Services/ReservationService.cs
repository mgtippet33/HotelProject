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
    public class ReservationService : IService<ReservationDTO>
    {
        private IWorkUnit DataBase { get; set; }
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public ReservationService(IWorkUnit database)
        {
            this.DataBase = database;
            fromDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<ReservationDTO, Reservation>().ReverseMap();
                    cfg.CreateMap<RoomDTO, Room>().ReverseMap();
                    cfg.CreateMap<PriceCategoryDTO, PriceCategory>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, Category>().ReverseMap();
                    cfg.CreateMap<ClientDTO, Client>().ReverseMap();
                    cfg.CreateMap<UserDTO, User>().ReverseMap();
                }).CreateMapper();

            toDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                    cfg.CreateMap<Client, ClientDTO>().ReverseMap();
                    cfg.CreateMap<User, UserDTO>().ReverseMap();
                }).CreateMapper();
        }

        public IEnumerable<ReservationDTO> GetAll()
        {
            return toDTOMapper.Map<IEnumerable<Reservation>, List<ReservationDTO>>(DataBase.Reservations.GetAll());
        }

        public ReservationDTO Get(int id)
        {
            var reservation = DataBase.Reservations.Get(id);
            return toDTOMapper.Map<Reservation, ReservationDTO>(reservation);
        }

        public void Create(ReservationDTO item)
        {
            var data = fromDTOMapper.Map<ReservationDTO, Reservation>(item);
            DataBase.Reservations.Create(data);
            DataBase.Save();
        }

        public void Update(int id, ReservationDTO item)
        {
            var data = fromDTOMapper.Map<ReservationDTO, Reservation>(item);
            DataBase.Reservations.Update(id, data);
            DataBase.Save();
        }

        public void Delete(int id)
        {
            DataBase.Reservations.Delete(id);
            DataBase.Save();
        }

        public bool Check(ReservationDTO item)
        {
            var data = fromDTOMapper.Map<ReservationDTO, Reservation>(item);
            var result = DataBase.Reservations.Search(data);
            return result;
        }
    }
}
