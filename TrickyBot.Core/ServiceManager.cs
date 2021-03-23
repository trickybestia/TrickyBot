// -----------------------------------------------------------------------
// <copyright file="ServiceManager.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

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
    /// Обёртка над списком сервисов, созданная для удобного управления ими.
    /// </summary>
    public class ServiceManager
    {
        /// <summary>
        /// Настройки сериализатора конфигов.
        /// </summary>
        private static readonly JsonSerializerSettings ConfigSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };

        /// <summary>
        /// Список загруженных сервисов.
        /// </summary>
        private readonly List<IService<IConfig>> services;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServiceManager"/>.
        /// </summary>
        internal ServiceManager()
        {
            this.services = new List<IService<IConfig>>();
        }

        /// <summary>
        /// Получает коллекцию загруженных сервисов.
        /// </summary>
        public IReadOnlyCollection<IService<IConfig>> Services => this.services;

        /// <summary>
        /// Возвращает экземпляр загруженного сервиса.
        /// </summary>
        /// <typeparam name="T">Тип сервиса.</typeparam>
        /// <param name="allowDisabled">Значение, указывающее на возможность возврата не включённого сервиса.</param>
        /// <exception cref="ServiceNotEnabledException">Запрашиваемый сервис не включен.</exception>
        /// <exception cref="ServiceNotLoadedException">Запрашиваемый сервис не загружен.</exception>
        /// <returns>Экземпляр загруженного сервиса.</returns>
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
        /// Асинхронно запускает менеджер сервисов.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        internal async Task StartAsync()
        {
            Log.Info(this, "Starting services...");
            this.Load();
            foreach (var service in this.Services)
            {
                if (service.Config.IsEnabled)
                {
                    await service.StartAsync();
                }
            }

            Log.Info(this, "Services started.");
        }

        /// <summary>
        /// Асинхронно останавливает менеджер сервисов.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        internal async Task StopAsync()
        {
            Log.Info(this, "Stopping services...");
            foreach (var service in this.Services)
            {
                if (service.Config.IsEnabled)
                {
                    await service.StopAsync();
                }
            }

            this.Save();
            Log.Info(this, "Services stopped.");
        }

        /// <summary>
        /// Загружает конфиг сервиса.
        /// </summary>
        /// <param name="service">Сервис, конфиг которого будет загружен.</param>
        private static void LoadService(IService<IConfig> service)
        {
            var configPath = Path.Combine(Paths.Configs, $"{service.Info.Name}.json");
            var configType = service.Config.GetType();
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
                Log.Warn(typeof(ServiceLoader), $"Service \"{service.Info.Name}\" v{service.Info.Version} by \"{service.Info.Author}\" does not have config, generating...");
                File.WriteAllText(configPath, JsonConvert.SerializeObject(service.Config, ConfigSerializerSettings));
            }
        }

        /// <summary>
        /// Сохраняет конфиг сервиса.
        /// </summary>
        /// <param name="service">Сервис, конфиг которого будет сохранён.</param>
        private static void SaveService(IService<IConfig> service)
        {
            var configPath = Path.Combine(Paths.Configs, $"{service.Info.Name}.json");
            File.WriteAllText(configPath, JsonConvert.SerializeObject(service.Config, ConfigSerializerSettings));
        }

        /// <summary>
        /// Загружает сервисы и их конфиги.
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

            this.services.AddRange(ServiceLoader.GetServices(assemblies));

            foreach (var service in this.services)
            {
                LoadService(service);
            }
        }

        /// <summary>
        /// Сохраняет конфиги всех сервисов.
        /// </summary>
        private void Save()
        {
            foreach (var service in this.services)
            {
                SaveService(service);
            }
        }
    }
}