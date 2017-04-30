using Microsoft.Practices.Unity;
using Musicfy.Bll.Contracts;
using Musicfy.Bll.Services;

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
            RegisterBllLayer(container);
            RegisterDalLayer(container);
        }

        private static void RegisterDalLayer(IUnityContainer container)
        {
            
        }

        private static void RegisterBllLayer(IUnityContainer container)
        {
            container.RegisterType<IArtistService, ArtistService>();
        }

        #endregion
    }
}