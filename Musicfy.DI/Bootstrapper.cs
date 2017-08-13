using System;
using Microsoft.Practices.Unity;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Services;
using Musicfy.Bll.Utils;
using Musicfy.Dal.Contracts;
using Musicfy.Dal.Repositories;
using Musicfy.Infrastructure.Configs;
using Neo4jClient;

namespace Musicfy.DI
{
    public static class Bootstrapper
    {
        #region Static constructor

        static Bootstrapper()
        {
            Initialize();
        }

        #endregion

        #region Public properties

        public static IUnityContainer Container
        {
            get { return _container; }
        }

        #endregion

        #region Private fields

        private static IUnityContainer _container;

        #endregion

        #region Private methods

        private static void Initialize()
        {
            var container = BuildUnityContainer();

            _container = container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            RegisterAuthorizationTokensCache();
            RegisterBllLayer(container);
            RegisterDalLayer(container);
        }

        private static void RegisterDalLayer(IUnityContainer container)
        {
            container.RegisterType<IGraphClient, GraphClient>(new PerRequestLifetimeManager(), new InjectionFactory(
                a =>
                {
                    var client = new GraphClient(new Uri(Config.GraphDbUrl), Config.GraphDbUser, Config.GraphDbPassword);
                    client.Connect();

                    return client;
                }));

            container.RegisterType<IArtistRepository, ArtistRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ISongCategoryRepository, SongCategoryRepository>();
        }

        private static void RegisterBllLayer(IUnityContainer container)
        {
            container.RegisterType<IArtistService, ArtistService>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<ISongCategoryService, SongCategoryService>();
        }

        private static void RegisterAuthorizationTokensCache()
        {
            if (!AuthorizationCache.Instance.Initialized)
            {
                var initializer = new AuthorizationCache.AuthorizationCacheInitializer();

                initializer.Initialize(AuthorizationCache.Instance);
            }
        }

        #endregion
    }
}