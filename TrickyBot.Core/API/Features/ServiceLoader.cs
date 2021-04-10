// -----------------------------------------------------------------------
// <copyright file="ServiceLoader.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

using TrickyBot.API.Interfaces;

namespace TrickyBot.API.Features
{
    /// <summary>
    /// Класс, инкапсулирующий в себе загрузку сервисов.
    /// </summary>
    internal static class ServiceLoader
    {
        /// <summary>
        /// Загружает все сервисы из приведённых сборок.
        /// </summary>
        /// <param name="assemblies">Список сборок.</param>
        /// <returns>Список сервисов.</returns>
        public static List<IService<IConfig>> GetServices(IEnumerable<Assembly> assemblies)
        {
            var services = new List<IService<IConfig>>();
            foreach (var assembly in assemblies)
            {
                services.AddRange(GetServices(assembly));
            }

            return services;
        }

        /// <summary>
        /// Загружает все сервисы из приведённой сборки.
        /// </summary>
        /// <param name="assembly">Сборка.</param>
        /// <returns>Список сервисов.</returns>
        public static List<IService<IConfig>> GetServices(Assembly assembly)
        {
            var services = new List<IService<IConfig>>();
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(typeof(IService<IConfig>)))
                {
                    var constructor = type.GetConstructor(Array.Empty<Type>());
                    if (constructor is null)
                    {
                        Log.Error(typeof(ServiceLoader), $"Тип {type.FullName} не имеет публичных конструкторов без параметров!");
                    }
                    else
                    {
                        try
                        {
                            services.Add((IService<IConfig>)constructor.Invoke(null));
                        }
                        catch (Exception ex)
                        {
                            Log.Error(typeof(ServiceLoader), $"Ошибка во время создания экземпляра типа {type.FullName}: {ex}");
                        }
                    }
                }
            }

            return services;
        }
    }
}