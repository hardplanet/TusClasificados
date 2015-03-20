[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TusClasificados.Site.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TusClasificados.Site.App_Start.NinjectWebCommon), "Stop")]

namespace TusClasificados.Site.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using TusClasificados.Site.Models;
    using TusClasificados.Site.Infrastructure;
    using TusClasificados.Site.Infrastructure.Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ApplicationDbContext>().ToMethod(c => HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>()).InRequestScope();
            kernel.Bind<IRepositorioGenerico<Anuncio>>().To<RepositorioGenerico<Anuncio>>().InRequestScope();
            kernel.Bind<IRepositorioGenerico<Ticket>>().To<RepositorioGenerico<Ticket>>().InRequestScope();
            kernel.Bind<IAnunciosService>().To<AnunciosService>().InRequestScope();
            kernel.Bind<ICuentasService>().To<CuentasService>().InRequestScope();

            kernel.Bind<IReloj>().To<Reloj>();
        }        
    }
}
