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
    public class CategoryControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        IMapper mapper;
        Mock<IWorkUnit> EFWorkUnitMock;
        Mock<IService<CategoryDTO>> CategoryServiceMock;

        public CategoryControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            CategoryServiceMock = new Mock<IService<CategoryDTO>>();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
        }

        [TestMethod]
        public void CategoryGetTest()
        {
            int id = 2;

            EFWorkUnitMock.Setup(x => x.Categories.Get(id)).Returns(new Category());
            CategoryServiceMock.Setup(a => a.Get(id)).Returns(new CategoryDTO());

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.Content.ReadAsAsync<CategoryModel>();
            CategoryModel expected = mapper.Map<CategoryDTO, CategoryModel>(categoryService.Get(id));

            Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void CategoryGetNegativeTest()
        {
            int id = 10;

            Category category = null;
            CategoryDTO categoryDTO = null;

            EFWorkUnitMock.Setup(x => x.Categories.Get(id)).Returns(category);
            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.StatusCode;
            var expected = HttpStatusCode.NotFound;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CategoryPostTest()
        {
            CategoryModel categoryModel = new CategoryModel()
            {
                CategoryID = 3,
                CategoryName = "Lux",
                Capacity = 6
            };

            EFWorkUnitMock.Setup(x => x.Categories.Search(DataForTests.Categories[0])).Returns(true);

            var toDTO = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            var categoryDTO = toDTO.Map<Category, CategoryDTO>(DataForTests.Categories[0]);
            CategoryServiceMock.Setup(a => a.Check(categoryDTO)).Returns(true);

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var httpResponse = controller.Post(httpRequest, categoryModel);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void CategoryPostNegativeTest()
        {
            var category = DataForTests.Categories[0];

            EFWorkUnitMock.Setup(x => x.Categories.Search(category)).Returns(true);

            var toDTO = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            var categoryDTO = toDTO.Map<Category, CategoryDTO>(category);
            CategoryServiceMock.Setup(a => a.Check(categoryDTO)).Returns(true);

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);

            var httpResponse = controller.Post(httpRequest, mapper.Map<CategoryDTO, CategoryModel>(categoryDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.Conflict, result);
        }

        [TestMethod]
        public void CategoryPutTest()
        {
            var id = 1;
            var category = DataForTests.Categories[id - 1];

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            var categoryDTO = toDTOMapper.Map<Category, CategoryDTO>(category);


            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);
            var httpResponse = controller.Put(httpRequest, id, mapper.Map<CategoryDTO, CategoryModel>(categoryDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void CategoryPutNegativeTest()
        {
            var id = 1;
            Category category = null;

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            var categoryDTO = toDTOMapper.Map<Category, CategoryDTO>(category);


            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);
            var httpResponse = controller.Put(httpRequest, id, mapper.Map<CategoryDTO, CategoryModel>(categoryDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }

        [TestMethod]
        public void CategoryDeleteTest()
        {
            var id = 1;
            var category = DataForTests.Categories[id - 1];

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            var categoryDTO = toDTOMapper.Map<Category, CategoryDTO>(category);


            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);
            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void CategoryDeleteNegativeTest()
        {
            var id = 1;
            Category category = null;

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            var categoryDTO = toDTOMapper.Map<Category, CategoryDTO>(category);


            CategoryServiceMock.Setup(a => a.Get(id)).Returns(categoryDTO);

            CategoryController controller = new CategoryController(CategoryServiceMock.Object);
            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }
    }
}
