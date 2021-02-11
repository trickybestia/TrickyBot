// -----------------------------------------------------------------------
// <copyright file="ServiceLoader.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

using TrickyBot.API.Features;
using TrickyBot.API.Interfaces;

namespace TrickyBot
{
    /// <summary>
    /// A class that incapsulates service loading.
    /// </summary>
    internal static class ServiceLoader
    {
        /// <summary>
        /// Loads all service from specified assemblies.
        /// </summary>
        /// <param name="assemblies">The list of assemblies containing services.</param>
        /// <returns>A list of loaded services.</returns>
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
        /// Loads all services from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly containing services.</param>
        /// <returns>A list of loaded services.</returns>
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
                        Log.Error(typeof(ServiceLoader), $"{type.FullName} does not have public constructor without parameters!");
                    }
                    else
                    {
                        try
                        {
                            services.Add((IService<IConfig>)constructor.Invoke(null));
                        }
                        catch (Exception ex)
                        {
                            Log.Error(typeof(ServiceLoader), $"Exception thrown while creating instance of {type.FullName}: {ex}");
                        }
                    }
                }
            }

            return services;
        }
    }
}