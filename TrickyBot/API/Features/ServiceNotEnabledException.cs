using System;

namespace TrickyBot.API.Features
{
    public class ServiceNotEnabledException : Exception
    {
        public Type ServiceType { get; }

        public ServiceNotEnabledException(Type serviceType) : base($"Service \"{serviceType.FullName}\" is not enabled!")
        {
            ServiceType = serviceType;
        }
    }
}
