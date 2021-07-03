using Hotel.API.Models;
using Hotel.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Hotel.BLL.DTO;
using System.Web.Http.Description;

namespace Hotel.API.Controllers
{
    public class ClientController : ApiController
    {
        private IService<ClientDTO> service;
        private IMapper mapper;
        
        public ClientController(IService<ClientDTO> service)
        {
            this.service = service;
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
        }

        // GET: api/Client
        [ResponseType(typeof(IEnumerable<ClientModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var data = service.GetAll();
                var clients = mapper.Map<IEnumerable<ClientDTO>, List<ClientModel>>(data);
                return request.CreateResponse(HttpStatusCode.OK, clients);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // GET: api/Client/5
        [ResponseType(typeof(ClientModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                ClientDTO client = service.Get(id);
                var data = new ClientModel();
                if (client != null)
                {
                    data = mapper.Map<ClientDTO, ClientModel>(client);
                    return request.CreateResponse(HttpStatusCode.OK, data);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch(Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            
        }

        // POST: api/Client
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] ClientModel value)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientModel, ClientDTO>()).CreateMapper();
                var data = mapper.Map<ClientModel, ClientDTO>(value);
                if (!service.Check(data))
                {
                    service.Create(data);
                    return request.CreateResponse(HttpStatusCode.OK);
                }
                return request.CreateResponse(HttpStatusCode.Conflict);
            }
            catch(Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Client/5
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] ClientModel value)
        {
            try
            {
                var client = service.Get(id);
                if (client != null)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientModel, ClientDTO>()).CreateMapper();
                    var data = mapper.Map<ClientModel, ClientDTO>(value);
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

        // DELETE: api/Client/5
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            try
            {
                var client = service.Get(id);
                if (client != null)
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
