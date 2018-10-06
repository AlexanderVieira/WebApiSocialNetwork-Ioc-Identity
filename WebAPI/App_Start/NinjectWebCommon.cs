[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace WebAPI.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using WebApi.StorageTableService.Repository;
    using WebApi.StoredProcedure.Repositories;
    using WebAPI.BLL.Interfaces.Repositories;
    using WebAPI.BLL.Interfaces.Services;
    using WebAPI.BLL.Services;
    using WebAPI.DAL.Repositories;
    using WebAPI.StorageBlobService.Repository;    
    using WebApiContrib.IoC.Ninject;

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

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
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
            //####################### DataBaseSqlRepository #######################
            kernel.Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>));
            //kernel.Bind<IFriendShipRepository>().To<FriendShipRepository>();            
            kernel.Bind<IProfileRepository>().To<ProfileRepository>();
            kernel.Bind<IGroupRepository>().To<GroupRepository>();
            kernel.Bind<IMarketplaceRepository>().To<MarketplaceRepository>();
            kernel.Bind<IGalleryRepository>().To<GalleryRepository>();
            kernel.Bind<IImageRepository>().To<ImageRepository>();
            kernel.Bind<IAnnouncementRepository>().To<AnnouncementRepository>();
            kernel.Bind<IPostRepository>().To<PostRepository>();
            kernel.Bind<INotificationRepository>().To<NotificationRepository>();

            //########################## CloudStorageService #######################
            kernel.Bind<IBlobStorageRepository>().To<BlobStorageRepository>();
            kernel.Bind<ITableStorageRepository>().To<TableStorageRepository>();

            //########################## DataBaseSqlService #######################
            kernel.Bind(typeof(IBaseService<>)).To(typeof(BaseService<>));
            //kernel.Bind<IFriendShipService>().To<FriendShipService>();
            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<IGroupService>().To<GroupService>();
            kernel.Bind<IMarketplaceService>().To<MarketplaceService>();
            kernel.Bind<IGalleryService>().To<GalleryService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<IAnnouncementService>().To<AnnouncementService>();
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<INotificationService>().To<NotificationService>();

            //########################## CloudStorageService #######################
            kernel.Bind<IBlobStorageService>().To<BlobStorageService>();
            kernel.Bind<ITableStorageService>().To<TableStorageService>();

            //########################## StorageProcedureRepository #################
            kernel.Bind<IFriendShipRepository>().To<FriendShipProcedureRepository>();            

            //########################## StorageProcedureService ####################
            kernel.Bind<IFriendShipService>().To<FriendShipProcedureService>();
            
        }
    }
}
