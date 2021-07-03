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
    public class PriceCategoryController : ApiController
    {
        private IService<PriceCategoryDTO> service;
        private IMapper mapper;

        public PriceCategoryController(IService<PriceCategoryDTO> service)
        {
            this.service = service;
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PriceCategoryDTO, PriceCategoryModel>().ReverseMap();
                    cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                }).CreateMapper();
        }

        // GET: api/PriceCategory
        [ResponseType(typeof(IEquatable<PriceCategoryModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var data = service.GetAll();
                var priceCategories = mapper.Map<IEnumerable<PriceCategoryDTO>, List<PriceCategoryModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, priceCategories);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET: api/PriceCategory/5
        [ResponseType(typeof(PriceCategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                PriceCategoryDTO priceCategory = service.Get(id);
                var data = new PriceCategoryModel();
                if (priceCategory != null)
                {
                    data = mapper.Map<PriceCategoryDTO, PriceCategoryModel>(priceCategory);
                    return request.CreateResponse(HttpStatusCode.OK, data);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST: api/PriceCategory
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] PriceCategoryModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>().ReverseMap();
                        cfg.CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
                    }).CreateMapper();
                var data = mapper.Map<PriceCategoryModel, PriceCategoryDTO>(value);
                if (!service.Check(data))
                {
                    service.Create(data);
                    return request.CreateResponse(HttpStatusCode.OK);
                }
                return request.CreateResponse(HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/PriceCategory/5
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] PriceCategoryModel value)
        {
            try
            {
                var priceCategory = service.Get(id);
                if (priceCategory != null)
                {
                    var mapper = new MapperConfiguration(
                        cfg =>
                        {
                            cfg.CreateMap<PriceCategoryModel, PriceCategoryDTO>().ReverseMap();
                            cfg.CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
                        }).CreateMapper();
                    var data = mapper.Map<PriceCategoryModel, PriceCategoryDTO>(value);
                    service.Update(id, data);
                    return request.CreateResponse(HttpStatusCode.OK);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/PriceCategory/5
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                var priceCategory = service.Get(id);
                if (priceCategory != null)
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
