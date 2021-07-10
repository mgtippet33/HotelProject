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
    public class ReservationController : ApiController
    {
        private IService<ReservationDTO> service;
        private IMapper mapper;

        public ReservationController(IService<ReservationDTO> service)
        {
            this.service = service;
            mapper = new MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<ReservationDTO, ReservationModel>().ReverseMap();
                   cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                   cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                   cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                   cfg.CreateMap<ClientDTO, ClientModel>().ReverseMap();
               }).CreateMapper();
        }

        // GET: api/Reservation
        [ResponseType(typeof(IEquatable<ReservationModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var data = service.GetAll();
                var reservations = mapper.Map<IEnumerable<ReservationDTO>, List<ReservationModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, reservations);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET: api/Reservation/5
        [ResponseType(typeof(ReservationModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                ReservationDTO reservation = service.Get(id);
                var data = new ReservationModel();
                if (reservation != null)
                {
                    data = mapper.Map<ReservationDTO, ReservationModel>(reservation);
                    return request.CreateResponse(HttpStatusCode.OK, data);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST: api/Reservation
        [Route("api/Reservation/Booking")]
        [HttpPost]
        public HttpResponseMessage BookingPost(HttpRequestMessage request, [FromBody] ReservationModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(
                   cfg =>
                   {
                       cfg.CreateMap<ReservationDTO, ReservationModel>().ReverseMap();
                       cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                       cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                       cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                       cfg.CreateMap<ClientDTO, ClientModel>().ReverseMap();
                   }).CreateMapper();
                var data = mapper.Map<ReservationModel, ReservationDTO>(value);
                if (!service.Check(data))
                {
                    service.Create(data);
                    return request.CreateResponse(HttpStatusCode.OK);
                }
                return request.CreateResponse(HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/Reservation/CheckIn/{reservationID}")]
        [HttpGet]
        public HttpResponseMessage CheckInPost(HttpRequestMessage request, int reservationID)
        {
            try
            {
                var reservation = service.Get(reservationID);
                if (reservation != null)
                {
                    reservation.SettledIn = true;
                    service.Update(reservationID, reservation);
                    return request.CreateResponse(HttpStatusCode.OK);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("api/Reservation/MothlyProfit/{year}")]
        [HttpGet]
        // PUT: api/Reservation/5
        public HttpResponseMessage MothlyProfitGet(HttpRequestMessage request, int year)
        {
            try
            {
                var reservations = service.GetAll();
                Dictionary<string, decimal> montlyProfit = new Dictionary<string, decimal>();
                foreach(var reservation in reservations)
                {
                    if(reservation.ArrivalDate.Year == year)
                    {
                        string key = reservation.ArrivalDate.ToString("MMMM");
                        decimal sum = Decimal.Parse((reservation.DepatureDate - reservation.ArrivalDate).TotalDays.ToString()) * reservation.RoomReservation.RoomCategory.Price;
                        if (montlyProfit.ContainsKey(key))
                        {
                            montlyProfit[key] += sum;
                        }
                        else
                        {
                            montlyProfit.Add(key, sum);
                        }
                    }
                }
                return request.CreateResponse(HttpStatusCode.OK, montlyProfit);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Reservation/5
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                var reservation = service.Get(id);
                if (reservation != null)
                {
                    service.Delete(id);
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
