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

namespace Hotel.Tests
{
    [TestClass]
    public class PriceCategoryControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        IMapper mapper;
        Mock<IWorkUnit> EFWorkUnitMock;
        Mock<IService<PriceCategoryDTO>> PriceCategoryServiceMock;

        public PriceCategoryControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            PriceCategoryServiceMock = new Mock<IService<PriceCategoryDTO>>();
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                }).CreateMapper();
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
        }

        [TestMethod]
        public void PriceCategoryGetTest()
        {
            int id = 2;

            var priceCategory = DataForTests.PriceCategories[id - 1];
            var toDTO = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var priceCategoryDTO = toDTO.Map<PriceCategory, PriceCategoryDTO>(priceCategory);

            EFWorkUnitMock.Setup(x => x.PriceCategories.Get(id)).Returns(priceCategory);
            PriceCategoryServiceMock.Setup(a => a.Get(id)).Returns(priceCategoryDTO);

            var priceCategoryService = new PriceCategoryService(EFWorkUnitMock.Object);
            PriceCategoryController controller = new PriceCategoryController(PriceCategoryServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.Content.ReadAsAsync<PriceCategoryModel>();
            PriceCategoryModel expected = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryService.Get(id));

            Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void PriceCategoryGetNegativeTest()
        {
            int id = 10;

            PriceCategory priceCategory = null;
            PriceCategoryDTO priceCategoryDTO = null;

            EFWorkUnitMock.Setup(x => x.PriceCategories.Get(id)).Returns(priceCategory);
            PriceCategoryServiceMock.Setup(a => a.Get(id)).Returns(priceCategoryDTO);

            var priceCategoryService = new PriceCategoryService(EFWorkUnitMock.Object);
            PriceCategoryController controller = new PriceCategoryController(PriceCategoryServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.StatusCode;
            var expected = HttpStatusCode.NotFound;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PriceCategoryPostTest()
        {
            var categoryMapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Category, CategoryModel>()).CreateMapper();
            PriceCategoryModel priceCategoryModel = new PriceCategoryModel()
            {
                PriceCategoryID = 3,
                Price = 350,
                StartDate = DateTime.Parse("01.06.2021"),
                EndDate = DateTime.Parse("30.08.2021"),
                CategoryName = categoryMapper.Map<Category, CategoryModel>(DataForTests.Categories[2])
            };

            EFWorkUnitMock.Setup(x => x.PriceCategories.Search(DataForTests.PriceCategories[0])).Returns(true);

            var toDTO = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var priceCategoryDTO = toDTO.Map<PriceCategory, PriceCategoryDTO>(DataForTests.PriceCategories[0]);
            PriceCategoryServiceMock.Setup(a => a.Check(priceCategoryDTO)).Returns(true);

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryServiceMock.Object);

            var httpResponse = controller.Post(httpRequest, priceCategoryModel);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void PriceCategoryPostNegativeTest()
        {
            var priceCategory = DataForTests.PriceCategories[0];

            EFWorkUnitMock.Setup(x => x.PriceCategories.Search(priceCategory)).Returns(true);

            var toDTO = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var priceCategoryDTO = toDTO.Map<PriceCategory, PriceCategoryDTO>(priceCategory);
            PriceCategoryServiceMock.Setup(a => a.Check(priceCategoryDTO)).Returns(true);

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryServiceMock.Object);

            var httpResponse = controller.Post(httpRequest, mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.Conflict, result);
        }

        [TestMethod]
        public void PriceCategoryPutTest()
        {
            var id = 1;
            var priceCategory = DataForTests.PriceCategories[id - 1];

            var toDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var priceCategoryDTO = toDTOMapper.Map<PriceCategory, PriceCategoryDTO>(priceCategory);


            PriceCategoryServiceMock.Setup(a => a.Get(id)).Returns(priceCategoryDTO);

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryServiceMock.Object);
            var httpResponse = controller.Put(httpRequest, id, mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void PriceCategoryPutNegativeTest()
        {
            var id = 1;
            PriceCategory priceCategory = null;

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<PriceCategory, PriceCategoryDTO>()).CreateMapper();
            var priceCategoryDTO = toDTOMapper.Map<PriceCategory, PriceCategoryDTO>(priceCategory);


            PriceCategoryServiceMock.Setup(a => a.Get(id)).Returns(priceCategoryDTO);

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryServiceMock.Object);
            var httpResponse = controller.Put(httpRequest, id, mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }

        [TestMethod]
        public void PriceCategoryDeleteTest()
        {
            var id = 1;
            var priceCategory = DataForTests.PriceCategories[id - 1];

            var toDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategory, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            var priceCategoryDTO = toDTOMapper.Map<PriceCategory, PriceCategoryDTO>(priceCategory);


            PriceCategoryServiceMock.Setup(a => a.Get(id)).Returns(priceCategoryDTO);

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryServiceMock.Object);
            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void PriceCategoryDeleteNegativeTest()
        {
            var id = 1;
            PriceCategory priceCategory = null;

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<PriceCategory, PriceCategoryDTO>()).CreateMapper();
            var priceCategoryDTO = toDTOMapper.Map<PriceCategory, PriceCategoryDTO>(priceCategory);


            PriceCategoryServiceMock.Setup(a => a.Get(id)).Returns(priceCategoryDTO);

            PriceCategoryController controller = new PriceCategoryController(PriceCategoryServiceMock.Object);
            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }
    }
}
