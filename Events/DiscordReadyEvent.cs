using BetterBot.Utils;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBot.Events
{
    [PreInitialize]
    public class DiscordReadyEvent
    {
        private readonly DiscordShardedClient client;
        private readonly InteractionService commands;

        public DiscordReadyEvent(DiscordShardedClient bot, InteractionService _commands)
        {

            client = bot;
            commands = _commands;
            bot.ShardReady += Client_Ready;

            
           
        }

        public async Task Client_Ready(DiscordSocketClient client)
        {
            await client.SetGameAsync($"/help | {BotConfig.VERSION}");
            await client.SetStatusAsync(UserStatus.Idle);

            var data = await RamApiv.Run();

            Console.WriteLine(data);




#if DEBUG
            await commands.RegisterCommandsToGuildAsync(936050113602793483);
#else
            await commands.RegisterCommandsGloballyAsync();
#endif
            Console.WriteLine("Ready");











        }
    }
}
