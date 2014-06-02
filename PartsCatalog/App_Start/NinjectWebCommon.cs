[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PartsCatalog.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PartsCatalog.App_Start.NinjectWebCommon), "Stop")]

namespace PartsCatalog.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using PartsCatalog.Util;
    using PartsCatalog.DAL;
    using System.Data.Entity;
    using PartsCatalog.Models;
    using PartsCatalog.DAL.Context;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
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

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<HttpServerUtilityBase>().ToMethod(c => new HttpServerUtilityWrapper(HttpContext.Current.Server));

            RegisterUtils(kernel);
            RegisterDAL(kernel);
        }

        private static void RegisterUtils(IKernel kernel)
        {
            kernel.Bind<IImageManager>().To<ImageManager>()
                .WhenInjectedInto<IMakesRepository>()
                .WithConstructorArgument(Make.ImagesPath);
            kernel.Bind<IImageManager>().To<ImageManager>()
                .WhenInjectedInto<IModelsRepository>()
                .WithConstructorArgument(Model.ImagesPath);
        }

        private static void RegisterDAL(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<PartsCatalogDbContext>().InRequestScope();
            kernel.Bind(typeof(IDbContextAdapter<>)).To(typeof(DbContextAdapter<>)).InRequestScope();
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>)).InRequestScope();
            kernel.Bind<IMakesRepository>().To<MakesRepository>().InRequestScope();
            kernel.Bind<IModelsRepository>().To<ModelsRepository>().InRequestScope();
        }


    }
}
