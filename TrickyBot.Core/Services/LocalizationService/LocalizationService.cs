// -----------------------------------------------------------------------
// <copyright file="LocalizationService.cs" company="TrickyBot Team">
// Copyright (c) TrickyBot Team. All rights reserved.
// Licensed under the CC BY-ND 4.0 license.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using TrickyBot.API.Abstract;
using TrickyBot.API.Features;
using TrickyBot.Services.BotService.API.Features;
using TrickyBot.Services.ConsoleCommandService.API.Interfaces;
using TrickyBot.Services.DiscordCommandService.API.Interfaces;
using TrickyBot.Services.LocalizationService.API.Features;

namespace TrickyBot.Services.LocalizationService
{
    /// <summary>
    /// Сервис для поддержки локализации и кастомизации интерфейса бота.
    /// </summary>
    public class LocalizationService : ServiceBase<LocalizationServiceConfig>
    {
        private List<LocalizationTable> localizations;

        /// <inheritdoc/>
        public override Priority Priority { get; } = new Priority(Priorities.CoreService.Value + 1);

        /// <inheritdoc/>
        public override IReadOnlyList<IDiscordCommand> DiscordCommands { get; } = Array.Empty<IDiscordCommand>();

        /// <inheritdoc/>
        public override IReadOnlyList<IConsoleCommand> ConsoleCommands { get; } = Array.Empty<IConsoleCommand>();

        /// <inheritdoc/>
        public override ServiceInfo Info { get; } = new ServiceInfo
        {
            Name = nameof(LocalizationService),
            Author = "TrickyBot Team",
            Version = Bot.Version,
            GithubRepositoryUrl = "https://github.com/TrickyBestia/TrickyBot",
        };

        /// <summary>
        /// Получает список загруженных таблиц локализаций.
        /// </summary>
        public IReadOnlyList<LocalizationTable> LocalizationTables => this.localizations;

        /// <inheritdoc/>
        protected override async Task OnStart()
        {
            this.localizations = new List<LocalizationTable>();
            foreach (var file in Directory.EnumerateFiles(Paths.Localizations))
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                this.localizations.Add(await LocalizationTable.FromStreamAsync(stream));
            }

            foreach (var service in ServiceManager.Services)
            {
                var assembly = service.GetType().Assembly;
                foreach (var localizationResourceName in assembly.GetManifestResourceNames().Where(name => name.Contains("Localizations")))
                {
                    using var stream = assembly.GetManifestResourceStream(localizationResourceName);
                    this.localizations.Add(await LocalizationTable.FromStreamAsync(stream));
                }
            }
        }

        /// <inheritdoc/>
        protected override Task OnStop() => Task.CompletedTask;
    }
}