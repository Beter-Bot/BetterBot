using BetterBot.Utils;
using Discord;
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
        private readonly DiscordSocketClient ram;

        public DiscordReadyEvent(DiscordSocketClient client)
        {
            client.Ready += Client_Ready;

            ram = client;
        }

        public async Task Client_Ready()
        {
            await ram.SetGameAsync($"/help | {BotConfig.VERSION}");
            await ram.SetStatusAsync(UserStatus.Idle);

            Console.WriteLine("Ready");


            


            
        }
    }
}
