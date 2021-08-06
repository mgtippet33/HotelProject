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
    public class CategoryController : Controller
    {
        private IService<CategoryDTO> service;
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public CategoryController(IService<CategoryDTO> service)
        {
            this.service = service;
            toDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
            fromDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }

        // GET: Category
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                var data = fromDTOMapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(service.GetAll());
                return View(data);
            }
            catch(Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CategoryModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.ActionUserName = User.Identity.Name;
                    category.ActionType = "Create";
                    category.ActionTime = DateTime.Now;
                    var modelDTO = toDTOMapper.Map<CategoryModel, CategoryDTO>(category);
                    if (!service.Check(modelDTO))
                    {
                        service.Create(modelDTO);
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "This category already exists");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var data = fromDTOMapper.Map<CategoryDTO, CategoryModel>(service.Get(id));
                return View(data);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ActionUserName = User.Identity.Name;
                    model.ActionType = "Edit";
                    model.ActionTime = DateTime.Now;
                    model.CategoryID = Int32.Parse(Request.Url.Segments[3]);
                    var modelDTO = toDTOMapper.Map<CategoryModel, CategoryDTO>(model);
                    service.Update(modelDTO.CategoryID, modelDTO);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
    }
}