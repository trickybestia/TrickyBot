// -----------------------------------------------------------------------
// <copyright file="CustomizationService.cs" company="The TrickyBot Team">
// Copyright (c) The TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.CustomizationService.API.Features;

namespace TrickyBot.Services.CustomizationService
{
    /// <summary>
    /// Сервис для поддержки кастомизации интерфейса бота.
    /// </summary>
    public class CustomizationService : ServiceBase<CustomizationServiceConfig>
    {
        private List<CustomizationTable> customizationTables;

        /// <inheritdoc/>
        public override Priority Priority { get; } = new Priority(Priorities.CoreService.Value + 1);

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(CustomizationService),
            Author = "The TrickyBot Team",
            Version = Bot.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        /// <summary>
        /// Получает список загруженных таблиц кастомных строк.
        /// </summary>
        public IReadOnlyList<CustomizationTable> CustomizationTables => this.customizationTables;

        /// <inheritdoc/>
        protected override async Task OnStart()
        {
            this.customizationTables = new List<CustomizationTable>();
            foreach (var file in Directory.EnumerateFiles(Paths.CustomStrings))
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                this.customizationTables.Add(await CustomizationTable.FromStreamAsync(stream));
            }

            foreach (var service in ServiceManager.Services)
            {
                var assembly = service.GetType().Assembly;
                foreach (var localizationResourceName in assembly.GetManifestResourceNames().Where(name => name.EndsWith("CustomStrings.txt")))
                {
                    using var stream = assembly.GetManifestResourceStream(localizationResourceName);
                    this.customizationTables.Add(await CustomizationTable.FromStreamAsync(stream));
                }
            }
        }

        /// <inheritdoc/>
        protected override Task OnStop() => Task.CompletedTask;
    }
}