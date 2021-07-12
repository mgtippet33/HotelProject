using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Hotel.Tests.ServiceTests
{
    [TestClass]
    public class ReservationServiceTest
    {
        IMapper toDTOMapper, fromDTOMapper;
        Mock<IWorkUnit> EFWorkUnitMock;

        public ReservationServiceTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            fromDTOMapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<ReservationDTO, Reservation>().ReverseMap();
                   cfg.CreateMap<RoomDTO, Room>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategory>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, Category>().ReverseMap();
                   cfg.CreateMap<ClientDTO, Client>().ReverseMap();
               }).CreateMapper();

            toDTOMapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();
        }

        [TestMethod]
        public void ReservationGetAllTest()
        {
            var resevations = DataForTests.Reservations;
            EFWorkUnitMock.Setup(x => x.Reservations.GetAll()).Returns(resevations);

            var reservationService = new ReservationService(EFWorkUnitMock.Object);
            var result = reservationService.GetAll();
            var expected = toDTOMapper.Map<List<Reservation>, List<ReservationDTO>>(resevations);

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void ReservationGetTest()
        {
            int id = 1;

            var reservations = DataForTests.Reservations;
            EFWorkUnitMock.Setup(x => x.Reservations.Get(id)).Returns(reservations[id - 1]);

            var reservationService = new ReservationService(EFWorkUnitMock.Object);
            var result = reservationService.Get(id);
            var expected = toDTOMapper.Map<Reservation, ReservationDTO>(reservations[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReservationCreateTest()
        {
            var roomMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var clientMapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var reservationDTO = new ReservationDTO()
            {
                ReservationID = 5,
                ReservationDate = DateTime.Parse("12.07.2021"),
                ArrivalDate = DateTime.Parse("01.08.2021"),
                DepatureDate = DateTime.Parse("11.08.2021"),
                SettledIn = false,
                RoomReservation = roomMapper.Map<Room, RoomDTO>(DataForTests.Rooms[0]),
                ClientResevation = clientMapper.Map<Client, ClientDTO>(DataForTests.Clients[2])
            };


            EFWorkUnitMock.Setup(x => x.Reservations.Create(fromDTOMapper.Map<ReservationDTO, Reservation>(reservationDTO)));

            var reservationService = new ReservationService(EFWorkUnitMock.Object);
            reservationService.Create(reservationDTO);

            EFWorkUnitMock.Verify(x => x.Reservations.Create(fromDTOMapper.Map<ReservationDTO, Reservation>(reservationDTO)));
        }

        [TestMethod]
        public void ReservationUpdateTest()
        {
            var id = 2;
            var reservation = DataForTests.Reservations[id - 1];
            var reservationDTO = toDTOMapper.Map<Reservation, ReservationDTO>(reservation);

            EFWorkUnitMock.Setup(x => x.Reservations.Update(id, reservation));

            var reservationService = new ReservationService(EFWorkUnitMock.Object);
            reservationService.Update(id, reservationDTO);

            EFWorkUnitMock.Verify(x => x.Reservations.Update(id, reservation));
        }

        [TestMethod]
        public void ReservationDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(x => x.Reservations.Delete(id));

            var reservationService = new ReservationService(EFWorkUnitMock.Object);
            reservationService.Delete(id);

            EFWorkUnitMock.Verify(x => x.Reservations.Delete(id));
        }
    }
}
