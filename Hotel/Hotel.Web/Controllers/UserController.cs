using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hotel.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserService service;
        private IMapper toDTOMapper;
        private IMapper fromDTOMapper;

        public UserController(IUserService service)
        {
            this.service = service;
            toDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, UserDTO>()).CreateMapper();
            fromDTOMapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
        }

        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var userDTO = toDTOMapper.Map<UserModel, UserDTO>(user);
                var data = service.Login(userDTO);
                if (data != null)
                {
                    UserModel userModel = fromDTOMapper.Map<UserDTO, UserModel>(data);
                    FormsAuthentication.SetAuthCookie(user.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "User not found");

            }
            return View(user);
        }
    }
}