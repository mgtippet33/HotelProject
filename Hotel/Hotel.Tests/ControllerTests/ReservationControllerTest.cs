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
using System.Linq;

namespace Hotel.Tests
{
    [TestClass]
    public class ReservationControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        IMapper mapper;
        Mock<IWorkUnit> EFWorkUnitMock;
        Mock<IService<ReservationDTO>> ReservationServiceMock;

        public ReservationControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            ReservationServiceMock = new Mock<IService<ReservationDTO>>();
            mapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<ReservationDTO, ReservationModel>().ReverseMap();
                   cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                   cfg.CreateMap<ClientDTO, ClientModel>().ReverseMap();
               }).CreateMapper();
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
        }

        [TestMethod]
        public void ReservationGetTest()
        {
            int id = 2;

            var reservation = DataForTests.Reservations[id - 1];
            var toDTO = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                    cfg.CreateMap<Client, ClientDTO>().ReverseMap();
                }).CreateMapper();
            var reservationDTO = toDTO.Map<Reservation, ReservationDTO>(reservation);

            EFWorkUnitMock.Setup(x => x.Reservations.Get(id)).Returns(reservation);
            ReservationServiceMock.Setup(a => a.Get(id)).Returns(reservationDTO);

            var reservationService = new ReservationService(EFWorkUnitMock.Object);
            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.Content.ReadAsAsync<ReservationModel>();
            ReservationModel expected = mapper.Map<ReservationDTO, ReservationModel>(reservationService.Get(id));

            Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void ReservationGetNegativeTest()
        {
            int id = 10;

            Reservation reservation = null;
            ReservationDTO reservationDTO = null;

            EFWorkUnitMock.Setup(x => x.Reservations.Get(id)).Returns(reservation);
            ReservationServiceMock.Setup(a => a.Get(id)).Returns(reservationDTO);

            var reservationService = new ReservationService(EFWorkUnitMock.Object);
            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.StatusCode;
            var expected = HttpStatusCode.NotFound;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReservationPostTest()
        {
            var roomMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Room, RoomModel>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryModel>().ReverseMap();
                    cfg.CreateMap<Category, CategoryModel>().ReverseMap();
                }).CreateMapper();
            var clientMapper = new MapperConfiguration(
               cfg => cfg.CreateMap<Client, ClientModel>()).CreateMapper();
            ReservationModel reservationModel = new ReservationModel()
            {
                ReservationID = 5,
                ReservationDate = DateTime.Parse("12.07.2021"),
                ArrivalDate = DateTime.Parse("01.08.2021"),
                DepatureDate = DateTime.Parse("11.08.2021"),
                SettledIn = false,
                RoomReservation = roomMapper.Map<Room, RoomModel>(DataForTests.Rooms[0]),
                ClientResevation = clientMapper.Map<Client, ClientModel>(DataForTests.Clients[2])
            };

            EFWorkUnitMock.Setup(x => x.Reservations.Search(DataForTests.Reservations[0])).Returns(true);

            var toDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();
            var reservationDTO = toDTO.Map<Reservation, ReservationDTO>(DataForTests.Reservations[0]);
            ReservationServiceMock.Setup(a => a.Check(reservationDTO)).Returns(true);

            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.BookingPost(httpRequest, reservationModel);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void ReservationCheckInPostTest()
        {
            var id = 1;
            var reservation = DataForTests.Reservations[id - 1];

            var toDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();
            var reservationDTO = toDTO.Map<Reservation, ReservationDTO>(reservation);

            ReservationServiceMock.Setup(a => a.Get(id)).Returns(reservationDTO);

            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.CheckInPost(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void ReservationCheckInPostNegativeTest()
        {
            var id = 1;
            Reservation reservation = null;

            var toDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();
            var reservationDTO = toDTO.Map<Reservation, ReservationDTO>(reservation);
            ReservationServiceMock.Setup(a => a.Get(id)).Returns(reservationDTO);

            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.CheckInPost(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }

        [TestMethod]
        public void ReservationPostNegativeTest()
        {
            var reservation = DataForTests.Reservations[0];

            EFWorkUnitMock.Setup(x => x.Reservations.Search(reservation)).Returns(true);

            var toDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();
            var reservationDTO = toDTO.Map<Reservation, ReservationDTO>(DataForTests.Reservations[0]);
            ReservationServiceMock.Setup(a => a.Check(reservationDTO)).Returns(true);

            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.BookingPost(httpRequest, mapper.Map<ReservationDTO, ReservationModel>(reservationDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.Conflict, result);
        }

        

        [TestMethod]
        public void ReservationDeleteTest()
        {
            var id = 1;
            var reservation = DataForTests.Reservations[id - 1];

            var toDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();
            var reservationDTO = toDTO.Map<Reservation, ReservationDTO>(reservation);
            ReservationServiceMock.Setup(a => a.Get(id)).Returns(reservationDTO);

            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void ReservationDeleteNegativeTest()
        {
            var id = 1;
            Reservation reservation = null;

            var toDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();
            var reservationDTO = toDTO.Map<Reservation, ReservationDTO>(DataForTests.Reservations[0]);
            ReservationServiceMock.Setup(a => a.Check(reservationDTO)).Returns(true);

            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }


        public void AssertSameDictionary<TKey, TValue>(Dictionary<TKey, TValue> expected, Dictionary<TKey, TValue> actual)
        {
            string d1 = "expected";
            string d2 = "actual";
            Dictionary<TKey, TValue>.KeyCollection keys1 = expected.Keys;
            Dictionary<TKey, TValue>.KeyCollection keys2 = actual.Keys;
            if (actual.Keys.Count > expected.Keys.Count)
            {
                string tmp = d1;
                d1 = d2;
                d2 = tmp;
                Dictionary<TKey, TValue>.KeyCollection tmpkeys = keys1;
                keys1 = keys2;
                keys2 = tmpkeys;
            }

            foreach (TKey key in keys1)
            {
                Assert.IsTrue(keys2.Contains(key), $"key '{key}' of {d1} dict was not found in {d2}");
            }
            foreach (TKey key in expected.Keys)
            {
                //already ensured they both have the same keys
                Assert.AreEqual(expected[key], actual[key], $"for key '{key}'");
            }
        }

        public bool CompareDictionary(Dictionary<string, decimal> D1, Dictionary<string, decimal> D2)
        {
            var d1Keys = D1.Keys.ToList();
            var d2Keys = D2.Keys.ToList();
            if(d1Keys.Count == d2Keys.Count)
            {
                for(int i = 0; i < d1Keys.Count; ++i)
                {
                    if (D1[d1Keys[i]] != D2[d2Keys[i]])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        [TestMethod]
        public void MonthlyProfitGetTest()
        {
            var year = 2021;
            var toDTO = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
                   cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                   cfg.CreateMap<Client, ClientDTO>().ReverseMap();
               }).CreateMapper();
            var reservations = toDTO.Map<List<Reservation>, List<ReservationDTO>>(DataForTests.Reservations);
            ReservationServiceMock.Setup(x => x.GetAll()).Returns(reservations);
            ReservationController controller = new ReservationController(ReservationServiceMock.Object);

            var httpResponse = controller.MothlyProfitGet(httpRequest, year);
            var result = httpResponse.StatusCode;

            var resultContent = httpResponse.Content.ReadAsAsync<Dictionary<string, decimal>>().Result;
            var expected = new Dictionary<string, decimal>()
            {
                { "Июль", 7100 },
                { "Август", 6650 }
            };
            var assertResult = CompareDictionary(expected, resultContent);

            Assert.AreEqual(HttpStatusCode.OK, result);
            Assert.IsTrue(assertResult);
        }

    }

}
