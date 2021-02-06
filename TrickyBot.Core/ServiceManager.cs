// -----------------------------------------------------------------------
// <copyright file="ServiceManager.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Newtonsoft.Json;

using TrickyBot.API.Features;
using TrickyBot.API.Interfaces;

namespace TrickyBot
{
    /// <summary>
    /// A class that helps with manipulating services.
    /// </summary>
    public class ServiceManager
    {
        /// <summary>
        /// The instance of <see cref="JsonSerializerSettings"/> associated with this service manager.
        /// </summary>
        private static readonly JsonSerializerSettings ConfigSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };

        /// <summary>
        /// The list of loaded services.
        /// </summary>
        private readonly List<IService<IConfig>> services;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceManager"/> class.
        /// </summary>
        internal ServiceManager()
        {
            this.services = new List<IService<IConfig>>();
        }

        /// <summary>
        /// Gets a collection of loaded services.
        /// </summary>
        public IReadOnlyCollection<IService<IConfig>> Services => this.services;

        /// <summary>
        /// Gets the instance of the loaded service.
        /// </summary>
        /// <typeparam name="T">A service type.</typeparam>
        /// <param name="allowDisabled">A value indicating whether disabled service can be found.</param>
        /// <returns>The instance of the loaded service.</returns>
        public T GetService<T>(bool allowDisabled = false)
        {
            foreach (var service in this.Services)
            {
                if (service.GetType() == typeof(T))
                {
                    if (!service.Config.IsEnabled && !allowDisabled)
                    {
                        throw new ServiceNotEnabledException(typeof(T));
                    }

                    return (T)service;
                }
            }

            throw new ServiceNotLoadedException(typeof(T));
        }

        /// <summary>
        /// Starts a service manager asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous start operation.</returns>
        internal async Task StartAsync()
        {
            Log.Info("Starting services...");
            this.Load();
            foreach (var service in this.Services)
            {
                if (service.Config.IsEnabled)
                {
                    await service.StartAsync();
                }
            }

            Log.Info("Services started.");
        }

        /// <summary>
        /// Stops a service manager asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous start operation.</returns>
        internal async Task StopAsync()
        {
            Log.Info("Stopping services...");
            foreach (var service in this.Services)
            {
                if (service.Config.IsEnabled)
                {
                    await service.StopAsync();
                }
            }

            this.Save();
            Log.Info("Services stopped.");
        }

        /// <summary>
        /// Loads services and their configs.
        /// </summary>
        private void Load()
        {
            var assemblies = new List<Assembly>
            {
                Assembly.GetExecutingAssembly(),
            };
            foreach (var file in Directory.EnumerateFiles(Paths.Services, "*.dll", SearchOption.TopDirectoryOnly))
            {
                assemblies.Add(Assembly.LoadFrom(file));
            }

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAssignableTo(typeof(IService<IConfig>)))
                    {
                        var constructor = type.GetConstructor(Array.Empty<Type>());
                        this.services.Add((IService<IConfig>)constructor.Invoke(null));
                    }
                }
            }

            foreach (var service in this.services)
            {
                var configPath = Path.Combine(Paths.Configs, $"{service.Info.Name}.json");
                var configType = service.GetType().BaseType.GetGenericArguments()[0];
                try
                {
                    var config = JsonConvert.DeserializeObject(File.ReadAllText(configPath), configType, ConfigSerializerSettings);

                    foreach (var sourceProperty in configType.GetProperties())
                    {
                        configType.GetProperty(sourceProperty.Name)?.SetValue(service.Config, sourceProperty.GetValue(config, null), null);
                    }
                }
                catch
                {
                    Log.Warn($"Service \"{service.Info.Name}\" v{service.Info.Version} by \"{service.Info.Author}\" does not have config, generating...");
                    File.WriteAllText(configPath, JsonConvert.SerializeObject(service.Config, ConfigSerializerSettings));
                }
            }
        }

        /// <summary>
        /// Saves configs of services.
        /// </summary>
        private void Save()
        {
            foreach (var service in this.services)
            {
                var dataPath = Path.Combine(Paths.Configs, $"{service.Info.Name}.json");
                File.WriteAllText(dataPath, JsonConvert.SerializeObject(service.Config, ConfigSerializerSettings));
            }
        }
    }
}