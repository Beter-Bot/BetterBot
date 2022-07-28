using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord.Interactions;
using BetterBot.Utils;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using BetterBot.Events;

namespace BetterBot
{
    public class BetterBot
    {
        private DiscordSocketClient client = null;
       private IServiceProvider _services = null;
        private CommandService _commandService = null;

        public static void Main(string[] args)
            => new BetterBot().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Console.Title = "Better Bot";
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.AllUnprivileged,
                DefaultRetryMode = RetryMode.AlwaysRetry,
                LogLevel = LogSeverity.Info,
                AlwaysDownloadUsers = true,
            });

            _commandService = new CommandService(new CommandServiceConfig()
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = Discord.Commands.RunMode.Sync,
            });
            var collection = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(_commandService)
                .AddSingleton<DiscordMessageEvent>()
                .AddSingleton<DiscordReadyEvent>();

            _services = collection.BuildServiceProvider();

            foreach (ServiceDescriptor service in collection)
            {
                if (service.ServiceType.GetCustomAttributes(typeof(PreInitialize), false) == null)
                    continue;

                if (service.ImplementationType == null)
                    continue;

                _services.GetService(service.ImplementationType);
            }
            //end

            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);


            await client.LoginAsync(TokenType.Bot, BotConfig.TOKEN);
            await client.StartAsync();
            await Task.Delay(Timeout.Infinite);

        }
    }
}
