using System;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Hotel.API.Controllers;
using Hotel.API.Models;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http;

namespace Hotel.Tests
{
    [TestClass]
    public class ClientControllerTest
    {
        HttpConfiguration httpConfiguration;
        HttpRequestMessage httpRequest;
        IMapper mapper;
        Mock<IWorkUnit> EFWorkUnitMock;
        Mock<IService<ClientDTO>> ClientServiceMock;

        public ClientControllerTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            ClientServiceMock = new Mock<IService<ClientDTO>>();
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
            httpConfiguration = new HttpConfiguration();
            httpRequest = new System.Net.Http.HttpRequestMessage();
            httpRequest.Properties[System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey] = httpConfiguration;
        }

        [TestMethod]
        public void ClientGetTest()
        {
            int id = 2;

            EFWorkUnitMock.Setup(x => x.Clients.Get(id)).Returns(new Client());
            ClientServiceMock.Setup(a => a.Get(id)).Returns(new ClientDTO());

            var clientService = new ClientService(EFWorkUnitMock.Object);
            ClientController controller = new ClientController(ClientServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.Content.ReadAsAsync<ClientModel>();
            ClientModel expected = mapper.Map<ClientDTO, ClientModel>(clientService.Get(id));

            Assert.AreEqual(expected, result.Result);
        }

        [TestMethod]
        public void ClientGetNegativeTest()
        {
            int id = 10;

            Client client = null;
            ClientDTO clientDTO = null;

            EFWorkUnitMock.Setup(x => x.Clients.Get(id)).Returns(client);
            ClientServiceMock.Setup(a => a.Get(id)).Returns(clientDTO);

            var clientService = new ClientService(EFWorkUnitMock.Object);
            ClientController controller = new ClientController(ClientServiceMock.Object);

            var httpResponse = controller.Get(httpRequest, id);
            var result = httpResponse.StatusCode;
            var expected = HttpStatusCode.NotFound;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClientPostTest()
        {
            ClientModel clientModel = new ClientModel()
            {
                ClientID = 1,
                Surname = "Test",
                Name = "Ivan",
                Passport = "DD 3324214"
            };

            EFWorkUnitMock.Setup(x => x.Clients.Search(DataForTests.Clients[0])).Returns(true);

            var toDTO = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var clientDTO = toDTO.Map<Client, ClientDTO>(DataForTests.Clients[0]);
            ClientServiceMock.Setup(a => a.Check(clientDTO)).Returns(true);

            ClientController controller = new ClientController(ClientServiceMock.Object);

            var httpResponse = controller.Post(httpRequest, clientModel);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void ClientPostNegativeTest()
        {
            var client = DataForTests.Clients[0];

            EFWorkUnitMock.Setup(x => x.Clients.Search(client)).Returns(true);

            var toDTO = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var clientDTO = toDTO.Map<Client, ClientDTO>(client);
            ClientServiceMock.Setup(a => a.Check(clientDTO)).Returns(true);

            ClientController controller = new ClientController(ClientServiceMock.Object);

            var httpResponse = controller.Post(httpRequest, mapper.Map<ClientDTO, ClientModel>(clientDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.Conflict, result);
        }

        [TestMethod]
        public void ClientPutTest()
        {
            var id = 1;
            var client = DataForTests.Clients[id - 1];

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var clientDTO = toDTOMapper.Map<Client, ClientDTO>(client);


            ClientServiceMock.Setup(a => a.Get(id)).Returns(clientDTO);

            ClientController controller = new ClientController(ClientServiceMock.Object);
            var httpResponse = controller.Put(httpRequest, id, mapper.Map<ClientDTO, ClientModel>(clientDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void ClientPutNegativeTest()
        {
            var id = 1;
            Client client = null;

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var clientDTO = toDTOMapper.Map<Client, ClientDTO>(client);


            ClientServiceMock.Setup(a => a.Get(id)).Returns(clientDTO);

            ClientController controller = new ClientController(ClientServiceMock.Object);
            var httpResponse = controller.Put(httpRequest, id, mapper.Map<ClientDTO, ClientModel>(clientDTO));
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }

        [TestMethod]
        public void ClientDeleteTest()
        {
            var id = 1;
            var client = DataForTests.Clients[id - 1];

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var clientDTO = toDTOMapper.Map<Client, ClientDTO>(client);


            ClientServiceMock.Setup(a => a.Get(id)).Returns(clientDTO);

            ClientController controller = new ClientController(ClientServiceMock.Object);
            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod]
        public void ClientDeleteNegativeTest()
        {
            var id = 1;
            Client client = null;

            var toDTOMapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            var clientDTO = toDTOMapper.Map<Client, ClientDTO>(client);


            ClientServiceMock.Setup(a => a.Get(id)).Returns(clientDTO);

            ClientController controller = new ClientController(ClientServiceMock.Object);
            var httpResponse = controller.Delete(httpRequest, id);
            var result = httpResponse.StatusCode;

            Assert.AreEqual(HttpStatusCode.NotFound, result);
        }
    }
}
