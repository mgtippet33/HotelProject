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
    public class ClientController : Controller
    {
        private IService<ClientDTO> service;
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public ClientController(IService<ClientDTO> service)
        {
            this.service = service;
            fromDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
            toDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientModel, ClientDTO>()).CreateMapper();
        }

        // GET: Client
        [Authorize]
        public ActionResult Index()
        {
            var data = fromDTOMapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(service.GetAll());
            return View(data);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            var data = fromDTOMapper.Map<ClientDTO, ClientModel>(service.Get(id));
            return View(data);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(ClientModel model)
        {
            if (ModelState.IsValid)
            {
                model.ActionUserName = User.Identity.Name;
                model.ActionType = "Create";
                model.ActionTime = DateTime.Now;
                var modelDTO = toDTOMapper.Map<ClientModel, ClientDTO>(model);
                if (!service.Check(modelDTO))
                {
                    service.Create(modelDTO);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "This client already exists");
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
            var data = fromDTOMapper.Map<ClientDTO, ClientModel>(service.Get(id));
            return View(data);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(ClientModel model)
        {
            if (ModelState.IsValid)
            {
                model.ActionUserName = User.Identity.Name;
                model.ActionType = "Edit";
                model.ActionTime = DateTime.Now;
                model.ClientID = Int32.Parse(Request.Url.Segments[3]);
                var modelDTO = toDTOMapper.Map<ClientModel, ClientDTO>(model);
                if (!service.Check(modelDTO))
                {
                    service.Update(modelDTO.ClientID, modelDTO);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "This client already exists");
                return View();
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
            service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}