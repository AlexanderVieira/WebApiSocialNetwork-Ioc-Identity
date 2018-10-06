[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApi.Region.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebApi.Region.App_Start.NinjectWebCommon), "Stop")]

namespace WebApi.Region.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using BlobStorageService.Repository;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using WebApi.Domain.Services;
    using WebApi.ProcedureRegion.Repositories;
    using WebAPI.DataAccess.Repositories;
    using WebAPI.Domain.Interfaces.Repositories;
    using WebAPI.Domain.Interfaces.Services;
    using WebAPI.Domain.Services;

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

                // Install our Ninject-based IDependencyResolver into the Web API config
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

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
            //####################### DataBaseSqlRepository #######################
            kernel.Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>));
            //kernel.Bind<ICountryRepository>().To<CountryRepository>();            
            //kernel.Bind<IStateRepository>().To<StateRepository>();
            kernel.Bind<IAddressRepository>().To<AddressRepository>();            

            //########################## DataBaseSqlService #######################
            kernel.Bind(typeof(IBaseService<>)).To(typeof(BaseService<>));
            //kernel.Bind<ICountryService>().To<CountryService>();
            //kernel.Bind<IStateService>().To<StateService>();
            kernel.Bind<IAddressService>().To<AddressService>();

            //########################## CloudStorageRepository #######################
            kernel.Bind<IBlobStorageRepository>().To<BlobStorageRepository>();           

            //########################## CloudStorageService #######################
            kernel.Bind<IBlobStorageService>().To<BlobStorageService>();

            //########################## StorageProcedureRepository ##########################
            kernel.Bind<ICountryRepository>().To<CountryProcedureRepository>();
            kernel.Bind<IStateRepository>().To<StateProcedureRepository>();

            //########################## StorageProcedureService ##########################
            kernel.Bind<ICountryService>().To<CountryProcedureService>();
            kernel.Bind<IStateService>().To<StateProcedureService>();
        }        
    }
}
