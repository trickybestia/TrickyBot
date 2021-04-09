// -----------------------------------------------------------------------
// <copyright file="ServiceManager.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Обёртка над списком сервисов, созданная для удобного управления ими.
    /// </summary>
    public static class ServiceManager
    {
        /// <summary>
        /// Настройки сериализатора конфигов.
        /// </summary>
        private static readonly JsonSerializerSettings ConfigSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };

        private static readonly ManualResetEventSlim StopEvent = new ManualResetEventSlim(true);

        /// <summary>
        /// Список загруженных сервисов.
        /// </summary>
#pragma warning disable SA1311 // Static readonly fields should begin with upper-case letter
        private static readonly List<IService<IConfig>> services = new List<IService<IConfig>>();
#pragma warning restore SA1311 // Static readonly fields should begin with upper-case letter

        /// <summary>
        /// Получает коллекцию загруженных сервисов.
        /// </summary>
        public static IReadOnlyCollection<IService<IConfig>> Services => services;

        /// <summary>
        /// Возвращает экземпляр загруженного сервиса.
        /// </summary>
        /// <typeparam name="T">Тип сервиса.</typeparam>
        /// <param name="allowDisabled">Значение, указывающее на возможность возврата не включённого сервиса.</param>
        /// <exception cref="ServiceNotEnabledException">Запрашиваемый сервис не включен.</exception>
        /// <exception cref="ServiceNotLoadedException">Запрашиваемый сервис не загружен.</exception>
        /// <returns>Экземпляр загруженного сервиса.</returns>
        public static T GetService<T>(bool allowDisabled = false)
        {
            foreach (var service in Services)
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
        internal static async Task StartAsync()
        {
            Log.Info(typeof(ServiceManager), "Запуск сервисов...");
            Load();
            foreach (var service in Services.OrderByDescending(service => service.Priority))
            {
                if (service.Config.IsEnabled)
                {
                    await service.StartAsync();
                }
            }

            Log.Info(typeof(ServiceManager), "Сервисы запущены.");
        }

        /// <summary>
        /// Асинхронно останавливает менеджер сервисов.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        internal static async Task StopAsync()
        {
            Log.Info(typeof(ServiceManager), "Остановка сервисов...");
            foreach (var service in Services.OrderBy(service => service.Priority))
            {
                if (service.Config.IsEnabled)
                {
                    await service.StopAsync();
                }
            }

            Save();
            Log.Info(typeof(ServiceManager), "Сервисы остановлены.");
        }

        /// <summary>
        /// Асинхронно ожидает остановки бота.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        internal static Task WaitToStopAsync()
        {
            return Task.Run(StopEvent.Wait);
        }

        /// <summary>
        /// Загружает конфиг сервиса.
        /// </summary>
        /// <param name="service">Сервис, конфиг которого будет загружен.</param>
        private static void LoadServiceConfig(IService<IConfig> service)
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
                Log.Warn(typeof(ServiceLoader), $"Сервис \"{service.Info.Name}\" v{service.Info.Version} by \"{service.Info.Author}\" не имеет конфига, создание...");
                File.WriteAllText(configPath, JsonConvert.SerializeObject(service.Config, ConfigSerializerSettings));
            }
        }

        /// <summary>
        /// Сохраняет конфиг сервиса.
        /// </summary>
        /// <param name="service">Сервис, конфиг которого будет сохранён.</param>
        private static void SaveServiceConfig(IService<IConfig> service)
        {
            var configPath = Path.Combine(Paths.Configs, $"{service.Info.Name}.json");
            File.WriteAllText(configPath, JsonConvert.SerializeObject(service.Config, ConfigSerializerSettings));
        }

        /// <summary>
        /// Загружает сервисы и их конфиги.
        /// </summary>
        private static void Load()
        {
            var assemblies = new List<Assembly>
            {
                Assembly.GetExecutingAssembly(),
            };
            foreach (var file in Directory.EnumerateFiles(Paths.Services, "*.dll", SearchOption.TopDirectoryOnly))
            {
                assemblies.Add(Assembly.LoadFrom(file));
            }

            services.AddRange(ServiceLoader.GetServices(assemblies));

            foreach (var service in services)
            {
                LoadServiceConfig(service);
            }
        }

        /// <summary>
        /// Сохраняет конфиги всех сервисов.
        /// </summary>
        private static void Save()
        {
            foreach (var service in services)
            {
                SaveServiceConfig(service);
            }
        }
    }
}