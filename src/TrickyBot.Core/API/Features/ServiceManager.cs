// -----------------------------------------------------------------------
// <copyright file="ServiceManager.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
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
using TrickyBot.API.Exceptions;
using TrickyBot.API.Extensions;
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
        /// Возвращает список экземпляров сервисов, которые могут могуть быть преобразованы к заданному типу.
        /// </summary>
        /// <typeparam name="T">Тип сервисов.</typeparam>
        /// <param name="allowDisabled">Значение, указывающее на возможность возврата не включённого сервиса.</param>
        /// <returns>Список экземпляров сервисов, которые могут могуть быть преобразованы к заданному типу.</returns>
        public static List<T> GetServicesOfType<T>(bool allowDisabled = false)
        {
            var result = new List<T>();

            foreach (var service in Services)
            {
                if ((service.Config.IsEnabled || allowDisabled) && service is T typedService)
                {
                    result.Add(typedService);
                }
            }

            return result;
        }

        /// <summary>
        /// Сохраняет на диск конфиги всех сервисов.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public static async Task SaveConfigs()
        {
            foreach (var service in services)
            {
                await SaveServiceConfig(service);
            }
        }

        /// <summary>
        /// Асинхронно запускает менеджер сервисов.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        internal static async Task StartAsync()
        {
            StopEvent.Reset();
            Log.Info(typeof(ServiceManager), "Запуск сервисов...");
            await Load();
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

            await SaveConfigs();
            Log.Info(typeof(ServiceManager), "Сервисы остановлены.");
            StopEvent.Set();
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
        private static async Task LoadServiceConfig(IService<IConfig> service)
        {
            var configPath = Path.Combine(Paths.Configs, $"{service.Info.Name}.json");
            if (File.Exists(configPath))
            {
                var content = await File.ReadAllTextAsync(configPath);
                var configType = service.Config.GetType();
                try
                {
                    var config = JsonConvert.DeserializeObject(content, configType, ConfigSerializerSettings);
                    config.CopyPropertiesTo(service.Config);
                }
                catch (JsonException)
                {
                    service.Config.IsEnabled = false;
                    Log.Error(typeof(ServiceManager), $"Ошибка парсинга конфига сервиса \"{service.Info.Name}\".");
                    throw;
                }
            }
            else
            {
                Log.Warn(typeof(ServiceLoader), $"Сервис \"{service.Info.Name}\" v{service.Info.Version} от \"{service.Info.Author}\" не имеет конфига, создание...");
                File.WriteAllText(configPath, JsonConvert.SerializeObject(service.Config, ConfigSerializerSettings));
            }
        }

        /// <summary>
        /// Сохраняет конфиг сервиса.
        /// </summary>
        /// <param name="service">Сервис, конфиг которого будет сохранён.</param>
        private static async Task SaveServiceConfig(IService<IConfig> service)
        {
            var configPath = Path.Combine(Paths.Configs, $"{service.Info.Name}.json");
            var content = JsonConvert.SerializeObject(service.Config, ConfigSerializerSettings);
            await File.WriteAllTextAsync(configPath, content);
        }

        /// <summary>
        /// Загружает сервисы и их конфиги.
        /// </summary>
        private static async Task Load()
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
                await LoadServiceConfig(service);
            }
        }
    }
}