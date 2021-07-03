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
    public class CategoryController : ApiController
    {
        private IService<CategoryDTO> service;
        private IMapper mapper;

        public CategoryController(IService<CategoryDTO> service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>()).CreateMapper();
        }

        // GET: api/Category
        [ResponseType(typeof(IEnumerable<CategoryModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var data = service.GetAll();
                var categories = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, categories);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET: api/Category/5
        [ResponseType(typeof(CategoryModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                CategoryDTO category = service.Get(id);
                var data = new CategoryModel();
                if (category != null)
                {
                    data = mapper.Map<CategoryDTO, CategoryModel>(category);
                    return request.CreateResponse(HttpStatusCode.OK, data);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST: api/Category
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] CategoryModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
                var data = mapper.Map<CategoryModel, CategoryDTO>(value);
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

        // PUT: api/Category/5
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] CategoryModel value)
        {
            try
            {
                var category = service.Get(id);
                if (category != null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryModel, CategoryDTO>()).CreateMapper();
                    var data = mapper.Map<CategoryModel, CategoryDTO>(value);
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

        // DELETE: api/Category/5
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                var category = service.Get(id);
                if (category != null)
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
