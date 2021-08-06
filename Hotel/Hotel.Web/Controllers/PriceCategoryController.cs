using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Web.Controllers
{
    public class PriceCategoryController : Controller
    {
        private IService<PriceCategoryDTO> priceCategoryService;
        private IService<CategoryDTO> categoryService;
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;
        private IMapper fromDTOCategoryMapper;

        public PriceCategoryController(IService<PriceCategoryDTO> priceCategoryService,
            IService<CategoryDTO> categoryService)
        {
            this.priceCategoryService = priceCategoryService;
            this.categoryService = categoryService;
            toDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>().ReverseMap();
                    cfg.CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
                }).CreateMapper();
            fromDTOMapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                }).CreateMapper();
            fromDTOCategoryMapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }

        // GET: PriceCategory
        [Authorize]
        public ActionResult Index()
        {
            var data = fromDTOMapper.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(priceCategoryService.GetAll());
            return View(data);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var data = fromDTOMapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryService.Get(id));
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var categories = fromDTOCategoryMapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryService.GetAll());
            SelectList categoriesList = new SelectList(categories, "CategoryID", "CategoryName");
            ViewBag.Categories = categoriesList;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(PriceCategoryModel priceCategory)
        {
            if (ModelState.IsValid)
            {
                priceCategory.ActionUserName = User.Identity.Name;
                priceCategory.ActionType = "Create";
                priceCategory.ActionTime = DateTime.Now;
                var modelDTO = toDTOMapper.Map<PriceCategoryModel, PriceCategoryDTO>(priceCategory);
                if (!priceCategoryService.Check(modelDTO))
                {
                    priceCategoryService.Create(modelDTO);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "This category of price already exists");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var categories = fromDTOCategoryMapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(categoryService.GetAll());
            SelectList categoriesList = new SelectList(categories, "CategoryID", "CategoryName");
            ViewBag.Categories = categoriesList;
            var data = fromDTOMapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategoryService.Get(id));
            return View(data);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(PriceCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                model.ActionUserName = User.Identity.Name;
                model.ActionType = "Edit";
                model.ActionTime = DateTime.Now;
                model.PriceCategoryID = Int32.Parse(Request.Url.Segments[3]);
                var modelDTO = toDTOMapper.Map<PriceCategoryModel, PriceCategoryDTO>(model);
                priceCategoryService.Update(modelDTO.PriceCategoryID, modelDTO);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            priceCategoryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}