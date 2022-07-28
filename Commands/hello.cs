using BetterBot.Utils;
using Discord;

using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBot.Commands
{
    public class hello : InteractionModuleBase
    {
        private readonly DiscordShardedClient client;
       

        public hello(DiscordShardedClient bot)
        {
            this.client = bot;
        }

        [SlashCommand("hello", "Get a hello")]
        public async Task helloAsync()
        {
            var data = await RamApi.Run("hello", "english");
            Console.WriteLine(data);

            if (data == null) return;

            if(data.Too_many_requests != null)
            {
                await RespondAsync("Ram api ratelimit reached try again in a few seconds");
                return;
            }

            await RespondAsync($"{data.text}");

            
           
            return;
        }
    }
}
