using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Hotel.Tests.ServiceTests
{
    [TestClass]
    public class ClientServiceTest
    {
        IMapper mapper, fromDTOMapper; 
        Mock<IWorkUnit> EFWorkUnitMock;

        public ClientServiceTest()
        {
            EFWorkUnitMock = new Mock<IWorkUnit>();
            mapper = new MapperConfiguration(cfg =>
            cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            fromDTOMapper = new MapperConfiguration(cfg =>
            cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
        }

        [TestMethod]
        public void ClientGetAllTest()
        {
            var clients = DataForTests.Clients;
            EFWorkUnitMock.Setup(x => x.Clients.GetAll()).Returns(clients);

            var clientService = new ClientService(EFWorkUnitMock.Object);
            var result = clientService.GetAll();
            var expected = mapper.Map<List<Client>, List<ClientDTO>>(clients);

            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void ClientGetTest()
        {
            int id = 1;

            var clients = DataForTests.Clients;
            EFWorkUnitMock.Setup(x => x.Clients.Get(id)).Returns(clients[id - 1]);

            var clientService = new ClientService(EFWorkUnitMock.Object);
            var result = clientService.Get(id);
            var expected = mapper.Map<Client, ClientDTO>(clients[id - 1]);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClientCreateTest()
        {
            var clientDTO = new ClientDTO()
            {
                Surname = "Test",
                Name = "Test",
                Passport = "TT 3324214"
            };

            
            EFWorkUnitMock.Setup(x => x.Clients.Create(fromDTOMapper.Map<ClientDTO, Client>(clientDTO)));
            EFWorkUnitMock.Setup(x => x.Clients.GetAll()).Returns(DataForTests.Clients);

            var clientService = new ClientService(EFWorkUnitMock.Object);
            clientService.Create(clientDTO);

            EFWorkUnitMock.Verify(x => x.Clients.Create(fromDTOMapper.Map<ClientDTO, Client>(clientDTO)));
        }

        [TestMethod]
        public void ClientUpdateTest()
        {
            var id = 1;
            var client = DataForTests.Clients[id - 1];
            var clientDTO = mapper.Map<Client, ClientDTO>(client);

            EFWorkUnitMock.Setup(x => x.Clients.Update(id, client));

            var clientService = new ClientService(EFWorkUnitMock.Object);
            clientService.Update(id, clientDTO);

            EFWorkUnitMock.Verify(x => x.Clients.Update(id, client));
        }

        [TestMethod]
        public void ClientDeleteTest()
        {
            var id = 1;

            EFWorkUnitMock.Setup(x => x.Clients.Delete(id));

            var clientService = new ClientService(EFWorkUnitMock.Object);
            clientService.Delete(id);

            EFWorkUnitMock.Verify(x => x.Clients.Delete(id));
        }
    }
}
