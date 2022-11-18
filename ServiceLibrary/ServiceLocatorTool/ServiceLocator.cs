using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.ServiceLocatorTool
{
    public interface IServiceLocatorCache : System.IDisposable
    {
        void PutServiceRecord<TService>(ServiceBase service) where TService : IServiceContract;
        ServiceBase TakeServiceRecord<TService>() where TService : IServiceContract;
    }

    public class ServiceLocatorCache : System.Object, ServiceLocatorTool.IServiceLocatorCache
    {
        protected Dictionary<string, ServiceBase> CacheList { get; set; } = default;
        public ServiceLocatorCache() : base() => this.CacheList = new Dictionary<string, ServiceBase>();

        public void PutServiceRecord<TService>(ServiceBase service) where TService : IServiceContract
        {
            if (this.CacheList.ContainsKey(typeof(TService).FullName)) throw new ServiceLocatorException("Кеш не найден");
            this.CacheList.Add(typeof(TService).FullName, service);
        }

        public ServiceBase TakeServiceRecord<TService>() where TService : IServiceContract
        {
            if (!this.CacheList.ContainsKey(typeof(TService).FullName)) return null;
            return this.CacheList[typeof(TService).FullName];
        }

        public void Dispose() => this.CacheList.GetEnumerator().Dispose();
    }

    public sealed class ServiceLocatorException : System.Exception
    {
        public System.String ServiceName { get; private set; } = default;

        public ServiceLocatorException(string message) : this(message, string.Empty) { }
        public ServiceLocatorException(string message, string name) : base(message)
            => this.ServiceName = name;
    }

    public interface IServiceContract { System.Type FactoryType { get; } }

    public abstract class ServiceBase : System.Object
    {
        public System.String ServiceName { get; private set; } = default;
        public System.Guid ServiceID { get; private set; } = default;

        protected ServiceBase(System.String service_name) : base() 
        { this.ServiceName = service_name; this.ServiceID = System.Guid.NewGuid(); }
    }

    public abstract class ServiceFactoryBase : System.Object 
    {
        public abstract ServiceLocatorTool.ServiceBase BuildService();
    }

    public static class ServiceLocator : System.Object
    {
        private static ServiceLocatorCache LocatorCache { get; set; } = default;
        private static Dictionary<string, ServiceFactoryBase> BuildingsList { get; set; } = default;

        static ServiceLocator()
        {
            ServiceLocator.BuildingsList = new Dictionary<string, ServiceFactoryBase>();
            ServiceLocator.LocatorCache = new ServiceLocatorCache();
        }

        public static TService GetService<TService>() where TService: IServiceContract
        {
            var service_cache = ServiceLocator.LocatorCache.TakeServiceRecord<TService>();
            if (service_cache != null && service_cache is TService service) return service;

            if (!ServiceLocator.BuildingsList.ContainsKey(typeof(TService).FullName))
            { 
                throw new ServiceLocatorTool.ServiceLocatorException("Сервис не найден"); 
            }
            var service_instance = ServiceLocator.BuildingsList[typeof(TService).FullName].BuildService();
            ServiceLocator.LocatorCache.PutServiceRecord<TService>(service_instance);

            if (service_instance is TService result) return result;
            throw new ServiceLocatorTool.ServiceLocatorException("Несовместимый интерфейс");
        }

        public static void RegistrationService<TService>(ServiceFactoryBase factory) 
            where TService : IServiceContract
        {
            if (ServiceLocator.BuildingsList.ContainsKey(typeof(TService).FullName))
            {  throw new ServiceLocatorTool.ServiceLocatorException("Сервис уже зарегистрирован"); }

            ServiceLocator.BuildingsList.Add(typeof(TService).FullName, factory);
        }
    }
}
