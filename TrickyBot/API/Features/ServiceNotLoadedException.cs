using System;

namespace TrickyBot.API.Features
{
    public class ServiceNotLoadedException : Exception
    {
        public Type ServiceType { get; }

        public ServiceNotLoadedException(Type serviceType) : base($"Service \"{serviceType.FullName}\" is not loaded!")
        {
            ServiceType = serviceType;
        }
    }
}
