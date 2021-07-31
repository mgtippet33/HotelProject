using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelWEB.Utils
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            //AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        //private void AddBindings()
        //{
        //    kernel.Bind<IUserService>().To<UserService>();
        //    kernel.Bind<IService<ClientDTO>>().To<ClientService>();
        //    kernel.Bind<IService<CategoryDTO>>().To<CategoryService>();
        //    kernel.Bind<IService<PriceCategoryDTO>>().To<PriceCategoryService>();
        //    kernel.Bind<IService<RoomDTO>>().To<RoomService>();
        //    kernel.Bind<IService<ReservationDTO>>().To<ReservationService>();
        //}
    }
}