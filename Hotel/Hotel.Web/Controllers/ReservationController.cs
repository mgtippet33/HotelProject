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
    public class ReservationController : Controller
    {
        private IService<ReservationDTO> reservationService;
        private IService<RoomDTO> roomService;
        private IService<ClientDTO> clientService;
        private IMapper fromDTOMapper, toDTOMapper;
        private IMapper clientMapper, roomMapper;

        public ReservationController(IService<ReservationDTO> reservationService,
            IService<RoomDTO> roomService, IService<ClientDTO> clientService)
        {
            this.reservationService = reservationService;
            this.roomService = roomService;
            this.clientService = clientService;
            fromDTOMapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<ReservationDTO, ReservationModel>().ReverseMap();
                   cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                   cfg.CreateMap<ClientDTO, ClientModel>().ReverseMap();
               }).CreateMapper();
            toDTOMapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<ReservationDTO, ReservationModel>().ReverseMap();
                   cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                   cfg.CreateMap<ClientDTO, ClientModel>().ReverseMap();
               }).CreateMapper();
            clientMapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
            roomMapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
               }).CreateMapper();
        }

        // GET: Reservation
        public ActionResult Index()
        {
            var data = fromDTOMapper.Map<IEnumerable<ReservationDTO>, List<ReservationModel>>(reservationService.GetAll());
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = fromDTOMapper.Map<ReservationDTO, ReservationModel>(reservationService.Get(id));
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var clients = clientMapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientService.GetAll());
            var rooms = roomMapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(roomService.GetAll());
            SelectList clientList = new SelectList(clients, "ClientID", "Surname", "Name");
            SelectList roomList = new SelectList(rooms, "RoomID", "RoomName");
            ViewBag.Clients = clientList;
            ViewBag.Rooms = roomList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(ReservationModel model)
        {
            if (ModelState.IsValid)
            {
                //model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                var modelDTO = toDTOMapper.Map<ReservationModel, ReservationDTO>(model);
                if (!reservationService.Check(modelDTO))
                {
                    reservationService.Create(modelDTO);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "This room already reserved");
                return View();
            }

            ModelState.AddModelError("", "Something went wrong");
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var clients = clientMapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientService.GetAll());
            var rooms = roomMapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(roomService.GetAll());
            SelectList clientList = new SelectList(clients, "ClientID", "Surname", "Name");
            SelectList roomList = new SelectList(rooms, "RoomID", "RoomName");
            ViewBag.Clients = clientList;
            ViewBag.Rooms = roomList;

            var data = fromDTOMapper.Map<ReservationDTO, ReservationModel>(reservationService.Get(id));
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(ReservationModel model)
        {
            if (ModelState.IsValid)
            {
                //model.ActionUserId = Convert.ToInt32(User.Identity.Name);
                model.ReservationID = Int32.Parse(Request.Url.Segments[3]);
                var modelDTO = toDTOMapper.Map<ReservationModel, ReservationDTO>(model);
                if (!reservationService.Check(modelDTO))
                {
                    reservationService.Update(modelDTO.ReservationID, modelDTO);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "This room already reserved");
                return View();
            }

            ModelState.AddModelError("", "Something went wrong");
            return View();
        }

        public ActionResult Delete(int id)
        {
            reservationService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult CheckIn(int reservationID)
        {
            var reservation = reservationService.Get(reservationID);
            if (reservation != null)
            {
                reservation.SettledIn = true;
                reservationService.Update(reservationID, reservation);
            }
            return RedirectToAction("Index");
        }

        public ActionResult MonthlyProfit(string _year)
        {
            int year = int.Parse(_year);
            var reservations = reservationService.GetAll();
            Dictionary<string, decimal> monthlyProfit = new Dictionary<string, decimal>();
            foreach (var reservation in reservations)
            {
                if (reservation.ArrivalDate.Year == year)
                {
                    string key = reservation.ArrivalDate.ToString("MMMM");
                    decimal sum = Decimal.Parse((reservation.DepartureDate - reservation.ArrivalDate).TotalDays.ToString()) * reservation.RoomReservation.RoomCategory.Price;
                    if (monthlyProfit.ContainsKey(key))
                    {
                        monthlyProfit[key] += sum;
                    }
                    else
                    {
                        monthlyProfit.Add(key, sum);
                    }
                }
            }
            ViewBag.Year = year;
            return View(monthlyProfit);
        }
    }
}