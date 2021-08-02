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
                user.Password = user.HashedPassword;
                var userDTO = toDTOMapper.Map<UserModel, UserDTO>(user);
                var data = service.Login(userDTO);
                if (data != null)
                {
                    UserModel userModel = fromDTOMapper.Map<UserDTO, UserModel>(data);
                    FormsAuthentication.SetAuthCookie(userModel.Login, true);
                    return RedirectToAction("Index", "Category");
                }
                ModelState.AddModelError("", "User not found");

            }
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = new UserModel()
                {
                    Login = model.Login,
                    Password = model.HashedPassword,
                    Surname = model.Surname,
                    Name = model.Name
                };
                var userDTO = toDTOMapper.Map<UserModel, UserDTO>(user);
                if (!service.Check(userDTO))
                {
                    service.Create(userDTO);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "User with such login already exist");
                }
            }
            return View();
        }
    }
}