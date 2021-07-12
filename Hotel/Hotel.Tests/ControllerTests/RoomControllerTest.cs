using System;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Hotel.API.Controllers;
using Hotel.API.Models;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http;
using System.Collections.Generic;

namespace Hotel.Tests
{
    [TestClass]
    public class RoomControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        IMapper mapper;
        Mock<IWorkUnit> EFWorkUnitMock;
        Mock<IService<RoomDTO>> RoomServiceMock;
        Mock<IService<ReservationDTO>> ReservationServiceMock;

        public RoomControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            RoomServiceMock = new Mock<IService<RoomDTO>>();
            ReservationServiceMock = new Mock<IService<ReservationDTO>>();
            mapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
               }).CreateMapper();
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
        }

        [TestMethod]
        public void RoomGetTest()
        {
            int id = 2;

            var room = DataForTests.Rooms[id - 1];
            var toDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
               }).CreateMapper();
            var roomDTO = toDTO.Map<Room, RoomDTO>(room);

            EFWorkUnitMock.Setup(x => x.Rooms.Get(id)).Returns(room);
            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);

            var roomService = new RoomService(EFWorkUnitMock.Object);
            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.Content.ReadAsAsync<RoomModel>();
            RoomModel expected = mapper.Map<RoomDTO, RoomModel>(roomService.Get(id));

            Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void RoomGetNegativeTest()
        {
            int id = 10;

            Room room = null;
            RoomDTO roomDTO = null;

            EFWorkUnitMock.Setup(x => x.Rooms.Get(id)).Returns(room);
            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);

            var roomService = new RoomService(EFWorkUnitMock.Object);
            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.StatusCode;
            var expected = HttpStatusCode.NotFound;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RoomPostTest()
        {
            var roomCategoryMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategory, PriceCategoryModel>().ReverseMap();
                    cfg.CreateMap<Category, CategoryModel>().ReverseMap();
                }).CreateMapper();
            RoomModel roomModel = new RoomModel()
            {
                RoomID = 4,
                RoomName = "111",
                Active = true,
                RoomCategory = roomCategoryMapper.Map<PriceCategory, PriceCategoryModel>(DataForTests.PriceCategories[0])
            };

            EFWorkUnitMock.Setup(x => x.Rooms.Search(DataForTests.Rooms[0])).Returns(true);

            var toDTO = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var roomDTO = toDTO.Map<Room, RoomDTO>(DataForTests.Rooms[0]);
            RoomServiceMock.Setup(a => a.Check(roomDTO)).Returns(true);

            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.Post(httpRequest, roomModel);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void RoomPostNegativeTest()
        {
            var room = DataForTests.Rooms[0];

            EFWorkUnitMock.Setup(x => x.Rooms.Search(room)).Returns(true);

            var toDTO = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var roomDTO = toDTO.Map<Room, RoomDTO>(room);
            RoomServiceMock.Setup(a => a.Check(roomDTO)).Returns(true);

            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.Post(httpRequest, mapper.Map<RoomDTO, RoomModel>(roomDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.Conflict, result);
        }

        [TestMethod]
        public void RoomPutTest()
        {
            var id = 1;
            var room = DataForTests.Rooms[id - 1];

            var toDTO = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var roomDTO = toDTO.Map<Room, RoomDTO>(room);


            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);

            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.Put(httpRequest, id, mapper.Map<RoomDTO, RoomModel>(roomDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void RoomPutNegativeTest()
        {
            var id = 1;
            Room room = null;

            var toDTO = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var roomDTO = toDTO.Map<Room, RoomDTO>(room);


            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);

            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.Put(httpRequest, id, mapper.Map<RoomDTO, RoomModel>(roomDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }

        [TestMethod]
        public void RoomDeleteTest()
        {
            var id = 1;
            var room = DataForTests.Rooms[id - 1];

            var toDTO = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                     cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                     cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                 }).CreateMapper();
            var roomDTO = toDTO.Map<Room, RoomDTO>(room);


            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);

            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void RoomDeleteNegativeTest()
        {
            var id = 1;
            Room room = null;

            var toDTO = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                     cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                     cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                 }).CreateMapper();
            var roomDTO = toDTO.Map<Room, RoomDTO>(room);


            RoomServiceMock.Setup(a => a.Get(id)).Returns(roomDTO);

            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }

        [TestMethod]
        public void GetFreeRoomTest()
        {
            DateTime arrivalDate = DateTime.Parse("2021-09-01"); 
            DateTime depatureDate = DateTime.Parse("2021-09-21");
            var freeRooms = new List<Room>()
                {
                    new Room()
                    {
                        RoomID = 1,
                        RoomName = "100",
                        PriceCategoryID = 1,
                        Active = true,
                        RoomCategory = DataForTests.PriceCategories[0]
                    },
                    new Room()
                    {
                        RoomID = 2,
                        RoomName = "200",
                        PriceCategoryID = 2,
                        Active = true,
                        RoomCategory = DataForTests.PriceCategories[1]
                    },
                    new Room()
                    {
                        RoomID = 3,
                        RoomName = "300",
                        PriceCategoryID = 3,
                        Active = true,
                        RoomCategory = DataForTests.PriceCategories[2]
                    },
                };

            var toReservationDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();

            var toRoomDTO = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                     cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                     cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                 }).CreateMapper();

            var toRoomModel = new MapperConfiguration(
                 cfg =>
                 {
                     cfg.CreateMap<Room, RoomModel>().ReverseMap();
                     cfg.CreateMap<PriceCategory, PriceCategoryModel>().ReverseMap();
                     cfg.CreateMap<Category, CategoryModel>().ReverseMap();
                 }).CreateMapper();


            var reservations = toReservationDTO.Map<List<Reservation>, List<ReservationDTO>>(DataForTests.Reservations);
            var rooms = toRoomDTO.Map<List<Room>, List<RoomDTO>>(DataForTests.Rooms);

            ReservationServiceMock.Setup(x => x.GetAll()).Returns(reservations);
            RoomServiceMock.Setup(x => x.GetAll()).Returns(rooms);

            RoomController controller = new RoomController(RoomServiceMock.Object, ReservationServiceMock.Object);

            var httpResponse = controller.GetFreeRoom(httpRequest, arrivalDate, depatureDate);
            var resultCode = httpResponse.StatusCode;

            var expected = toRoomModel.Map<List<Room>, List<RoomModel>>(freeRooms);
            var resultContent = httpResponse.Content.ReadAsAsync<List<RoomModel>>();

            Assert.AreEqual(HttpStatusCode.OK, resultCode);
            CollectionAssert.AreEqual(expected, resultContent.Result);
        }
    }

}
