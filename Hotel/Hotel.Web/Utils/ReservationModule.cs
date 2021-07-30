﻿using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Web.Utils
{
    public class ReservationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IService<ReservationDTO>>().To<ReservationService>();
        }
    }
}