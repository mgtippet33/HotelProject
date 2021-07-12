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
    public class RoomServiceTest
    {
        IMapper toDTOMapper, fromDTOMapper;
        Mock<IWorkUnit> EFWorkUnitMock;

        public RoomServiceTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            toDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();

            fromDTOMapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<RoomDTO, Room>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategory>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, Category>().ReverseMap();
               }).CreateMapper();
        }

        [TestMethod]
        public void RoomGetAllTest()
        {
            var rooms = DataForTests.Rooms;
            EFWorkUnitMock.Setup(x => x.Rooms.GetAll()).Returns(rooms);

            var roomService = new RoomService(EFWorkUnitMock.Object);
            var result = roomService.GetAll();
            var expected = toDTOMapper.Map<List<Room>, List<RoomDTO>>(rooms);

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void RoomGetTest()
        {
            int id = 1;

            var rooms = DataForTests.Rooms;
            EFWorkUnitMock.Setup(x => x.Rooms.Get(id)).Returns(rooms[id - 1]);

            var roomService = new RoomService(EFWorkUnitMock.Object);
            var result = roomService.Get(id);
            var expected = toDTOMapper.Map<Room, RoomDTO>(rooms[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RoomCreateTest()
        {
            var roomCategoryMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var roomDTO = new RoomDTO()
            {
                RoomID = 1,
                RoomName = "100",
                Active = true,
                RoomCategory = roomCategoryMapper.Map<PriceCategory, PriceCategoryDTO>(DataForTests.PriceCategories[0])
            };


            EFWorkUnitMock.Setup(x => x.Rooms.Create(fromDTOMapper.Map<RoomDTO, Room>(roomDTO)));

            var roomService = new RoomService(EFWorkUnitMock.Object);
            roomService.Create(roomDTO);

            EFWorkUnitMock.Verify(x => x.Rooms.Create(fromDTOMapper.Map<RoomDTO, Room>(roomDTO)));
        }

        [TestMethod]
        public void RoomUpdateTest()
        {
            var id = 2;
            var room = DataForTests.Rooms[id - 1];
            var roomDTO = toDTOMapper.Map<Room, RoomDTO>(room);

            EFWorkUnitMock.Setup(x => x.Rooms.Update(id, room));

            var roomService = new RoomService(EFWorkUnitMock.Object);
            roomService.Update(id, roomDTO);

            EFWorkUnitMock.Verify(x => x.Rooms.Update(id, room));
        }

        [TestMethod]
        public void RoomDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(x => x.Rooms.Delete(id));

            var roomService = new RoomService(EFWorkUnitMock.Object);
            roomService.Delete(id);

            EFWorkUnitMock.Verify(x => x.Rooms.Delete(id));
        }
    }
}
