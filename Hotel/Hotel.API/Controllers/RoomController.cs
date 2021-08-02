using AutoMapper;
using Hotel.API.Models;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hotel.API.Controllers
{
    public class RoomController : ApiController
    {
        private IService<RoomDTO> roomService;
        private IService<ReservationDTO> reservationService;
        private IMapper mapper;

        public RoomController(IService<RoomDTO> roomService, IService<ReservationDTO> reservationService)
        {
            this.roomService = roomService;
            this.reservationService = reservationService;
            mapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
               }).CreateMapper();
        }

        // GET: api/Room
        [ResponseType(typeof(IEquatable<RoomModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var data = roomService.GetAll();
                var rooms = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, rooms);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET: api/Room/5
        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                RoomDTO room = roomService.Get(id);
                var data = new RoomModel();
                if (room != null)
                {
                    data = mapper.Map<RoomDTO, RoomModel>(room);
                    return request.CreateResponse(HttpStatusCode.OK, data);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("api/Room/FreeRoom/{arrivalDate}/{depatureDate}/")] 
        [HttpGet]
        public HttpResponseMessage GetFreeRoom(HttpRequestMessage request, DateTime arrivalDate, DateTime depatureDate)
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
                var busyRooms = reservations.Where(r => ((r.ArrivalDate >= arrivalDate && (depatureDate >= r.DepartureDate || depatureDate <= r.DepartureDate)) ||
                                                         (r.ArrivalDate <= arrivalDate && (depatureDate >= r.DepartureDate || depatureDate <= r.DepartureDate))) &&
                                                         r.DepartureDate >= arrivalDate && r.ArrivalDate <= depatureDate).
                                                         GroupBy(r => r.RoomReservation.RoomID).Select(r => r.First()).ToList();

                var roomData = roomService.GetAll();
                var rooms = mapper.Map<IEnumerable<RoomDTO>, List<RoomModel>>(roomData);
                foreach(var room in busyRooms)
                {
                    rooms.RemoveAll(r => r.RoomID == room.RoomReservation.RoomID);
                }
                return request.CreateResponse(HttpStatusCode.OK, rooms);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST: api/Room
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] RoomModel value)
        {
            try
            {
                //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();
                var mapper = new MapperConfiguration(
                    cfg =>
                    {
                       cfg.CreateMap<RoomModel, RoomDTO>().ReverseMap();
                       cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>().ReverseMap();
                       cfg.CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
                    }).CreateMapper();
                var data = mapper.Map<RoomModel, RoomDTO>(value);
                if (!roomService.Check(data))
                {
                    roomService.Create(data);
                    return request.CreateResponse(HttpStatusCode.OK);
                }
                return request.CreateResponse(HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Room/5
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] RoomModel value)
        {
            try
            {
                var room = roomService.Get(id);
                if (room != null)
                {
                    var mapper = new MapperConfiguration(
                        cfg =>
                        {
                            cfg.CreateMap<RoomModel, RoomDTO>().ReverseMap();
                            cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>().ReverseMap();
                            cfg.CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
                        }).CreateMapper();
                    var data = mapper.Map<RoomModel, RoomDTO>(value);
                    roomService.Update(id, data);
                    return request.CreateResponse(HttpStatusCode.OK);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Room/5
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                var room = roomService.Get(id);
                if (room != null)
                {
                    roomService.Delete(id);
                    return request.CreateResponse(HttpStatusCode.OK);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
