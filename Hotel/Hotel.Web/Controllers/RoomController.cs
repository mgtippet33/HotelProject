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
    public class RoomController : Controller
    {
        private IService<RoomDTO> roomService;
        private IService<PriceCategoryDTO> priceCategoryService;
        private IService<ReservationDTO> reservationService;
        private IMapper fromDTOMapper;
        private IMapper toDTOMapper;
        private IMapper mapperPriceCategories;

        public RoomController(IService<RoomDTO> roomService, 
            IService<PriceCategoryDTO> priceCategoryService,
            IService<ReservationDTO> reservationService)
        {
            this.roomService = roomService;
            this.priceCategoryService = priceCategoryService;
            this.reservationService = reservationService;
            fromDTOMapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
               }).CreateMapper();
            toDTOMapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<RoomModel, RoomDTO>().ReverseMap();
                   cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>().ReverseMap();
                   cfg.CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
               }).CreateMapper();
            mapperPriceCategories = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
               }).CreateMapper();
        }

        // GET: Room
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                var data = fromDTOMapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(roomService.GetAll());
                return View(data);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            try
            {
                var data = fromDTOMapper.Map<RoomDTO, RoomModel>(roomService.Get(id));
                return View(data);
            }
            catch (Exception ex)
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
                var priceCategories = mapperPriceCategories.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(priceCategoryService.GetAll());
                SelectList priceCategoriesList = new SelectList(priceCategories, "PriceCategoryID", "CategoryName.CategoryName");
                ViewBag.PriceCategories = priceCategoriesList;

                return View();
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(RoomModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ActionUserName = User.Identity.Name;
                    model.ActionType = "Create";
                    model.ActionTime = DateTime.Now;
                    var modelDTO = toDTOMapper.Map<RoomModel, RoomDTO>(model);
                    if (!roomService.Check(modelDTO))
                    {
                        roomService.Create(modelDTO);
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "This room already exists");
                    return View();
                }

                ModelState.AddModelError("", "Something went wrong");
                return View();
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
                var priceCategories = mapperPriceCategories.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(priceCategoryService.GetAll());
                SelectList priceCategoriesList = new SelectList(priceCategories, "PriceCategoryID", "CategoryName.CategoryName");
                ViewBag.PriceCategories = priceCategoriesList;

                var data = fromDTOMapper.Map<RoomDTO, RoomModel>(roomService.Get(id));

                return View(data);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(RoomModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ActionUserName = User.Identity.Name;
                    model.ActionType = "Edit";
                    model.ActionTime = DateTime.Now;
                    model.RoomID = Int32.Parse(Request.Url.Segments[3]);
                    var modelDTO = toDTOMapper.Map<RoomModel, RoomDTO>(model);
                    roomService.Update(modelDTO.RoomID, modelDTO);
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
                roomService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult SearchFreeRooms()
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
        public ActionResult SearchFreeRooms(DateTime arrivalDate, DateTime departureDate)
        {
            try
            {
                var reservationData = reservationService.GetAll();
                var reservationMapper = new MapperConfiguration(
                   cfg =>
                   {
                       cfg.CreateMap<ReservationDTO, ReservationModel>().ReverseMap();
                       cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                       cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                       cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                       cfg.CreateMap<ClientDTO, ClientModel>().ReverseMap();
                   }).CreateMapper();
                var reservations = reservationMapper.Map<IEnumerable<ReservationDTO>, List<ReservationModel>>(reservationData);
                var busyRooms = reservations.Where(r => ((r.ArrivalDate >= arrivalDate && (departureDate >= r.DepartureDate || departureDate <= r.DepartureDate)) ||
                                                         (r.ArrivalDate <= arrivalDate && (departureDate >= r.DepartureDate || departureDate <= r.DepartureDate))) &&
                                                         r.DepartureDate >= arrivalDate && r.ArrivalDate <= departureDate).
                                                         GroupBy(r => r.RoomReservation.RoomID).Select(r => r.First()).ToList();

                var roomData = roomService.GetAll();
                var rooms = fromDTOMapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(roomData);
                foreach (var room in busyRooms)
                {
                    rooms.RemoveAll(r => r.RoomID == room.RoomReservation.RoomID);
                }
                ViewBag.ArrivalDate = arrivalDate;
                ViewBag.DepartureDate = departureDate;
                return View("GetFreeRooms", rooms);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
    }
}