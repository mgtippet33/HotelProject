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
    public class PriceCategoryTest
    {
        IMapper toDTOMapper, fromDTOMapper;
        Mock<IWorkUnit> EFWorkUnitMock;

        public PriceCategoryTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
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

        [TestMethod]
        public void PriceCategoryGetAllTest()
        {
            var priceCategories = DataForTests.PriceCategories;
            EFWorkUnitMock.Setup(x => x.PriceCategories.GetAll()).Returns(priceCategories);

            var priceCategoryService = new PriceCategoryService(EFWorkUnitMock.Object);
            var result = priceCategoryService.GetAll();
            var expected = toDTOMapper.Map<List<PriceCategory>, List<PriceCategoryDTO>>(priceCategories);

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void PriceCategoryGetTest()
        {
            int id = 1;

            var priceCategories = DataForTests.PriceCategories;
            EFWorkUnitMock.Setup(x => x.PriceCategories.Get(id)).Returns(priceCategories[id - 1]);

            var priceCategoryService = new PriceCategoryService(EFWorkUnitMock.Object);
            var result = priceCategoryService.Get(id);
            var expected = toDTOMapper.Map<PriceCategory, PriceCategoryDTO>(priceCategories[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PriceCategoryCreateTest()
        {
            var categoryMapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            var priceCategoryDTO = new PriceCategoryDTO()
            {
                PriceCategoryID = 3,
                Price = 350,
                StartDate = DateTime.Parse("01.06.2021"),
                EndDate = DateTime.Parse("30.08.2021"),
                CategoryName = categoryMapper.Map<Category, CategoryDTO>(DataForTests.Categories[2])
            };


            EFWorkUnitMock.Setup(x => x.PriceCategories.Create(fromDTOMapper.Map<PriceCategoryDTO, PriceCategory>(priceCategoryDTO)));

            var priceCategoryService = new PriceCategoryService(EFWorkUnitMock.Object);
            priceCategoryService.Create(priceCategoryDTO);

            EFWorkUnitMock.Verify(x => x.PriceCategories.Create(fromDTOMapper.Map<PriceCategoryDTO, PriceCategory>(priceCategoryDTO)));
        }

        [TestMethod]
        public void PriceCategoryUpdateTest()
        {
            var id = 1;
            var priceCategory = DataForTests.PriceCategories[id - 1];
            var priceCategoryDTO = toDTOMapper.Map<PriceCategory, PriceCategoryDTO>(priceCategory);

            EFWorkUnitMock.Setup(x => x.PriceCategories.Update(id, priceCategory));

            var priceCategoryService = new PriceCategoryService(EFWorkUnitMock.Object);
            priceCategoryService.Update(id, priceCategoryDTO);

            EFWorkUnitMock.Verify(x => x.PriceCategories.Update(id, priceCategory));
        }

        [TestMethod]
        public void PriceCategoryDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(x => x.PriceCategories.Delete(id));

            var priceCategoryService = new PriceCategoryService(EFWorkUnitMock.Object);
            priceCategoryService.Delete(id);

            EFWorkUnitMock.Verify(x => x.PriceCategories.Delete(id));
        }
    }
}
