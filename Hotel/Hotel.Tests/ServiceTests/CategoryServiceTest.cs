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
    public class CategoryServiceTest
    {
        IMapper toDTOMapper, fromDTOMapper;
        Mock<IWorkUnit> EFWorkUnitMock;

        public CategoryServiceTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            toDTOMapper = new MapperConfiguration(cfg =>
            cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            fromDTOMapper = new MapperConfiguration(cfg =>
            cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();
        }

        [TestMethod]
        public void CategoryGetAllTest()
        {
            var categories = DataForTests.Categories;
            EFWorkUnitMock.Setup(x => x.Categories.GetAll()).Returns(categories);

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            var result = categoryService.GetAll();
            var expected = toDTOMapper.Map<List<Category>, List<CategoryDTO>>(categories);

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void CategoryGetTest()
        {
            int id = 1;

            var categories = DataForTests.Categories;
            EFWorkUnitMock.Setup(x => x.Categories.Get(id)).Returns(categories[id - 1]);

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            var result = categoryService.Get(id);
            var expected = toDTOMapper.Map<Category, CategoryDTO>(categories[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CategoryCreateTest()
        {
            var categoryDTO = new CategoryDTO()
            {
                CategoryName = "Test",
                Capacity = 1
            };


            EFWorkUnitMock.Setup(x => x.Categories.Create(fromDTOMapper.Map<CategoryDTO, Category>(categoryDTO)));

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            categoryService.Create(categoryDTO);

            EFWorkUnitMock.Verify(x => x.Categories.Create(fromDTOMapper.Map<CategoryDTO, Category>(categoryDTO)));
        }

        [TestMethod]
        public void CategoryUpdateTest()
        {
            var id = 1;
            var category = DataForTests.Categories[id - 1];
            var categoryDTO = toDTOMapper.Map<Category, CategoryDTO>(category);

            EFWorkUnitMock.Setup(x => x.Categories.Update(id, category));

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            categoryService.Update(id, categoryDTO);

            EFWorkUnitMock.Verify(x => x.Categories.Update(id, category));
        }

        [TestMethod]
        public void CategoryDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(x => x.Categories.Delete(id));

            var categoryService = new CategoryService(EFWorkUnitMock.Object);
            categoryService.Delete(id);

            EFWorkUnitMock.Verify(x => x.Categories.Delete(id));
        }
    }
}
