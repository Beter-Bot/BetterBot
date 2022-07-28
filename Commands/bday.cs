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
    public class bday : InteractionModuleBase
    {
        private readonly DiscordShardedClient client;


        public bday(DiscordShardedClient bot)
        {
            this.client = bot;
        }

        [SlashCommand("bday", "Get a bday wish")]
        public async Task bdayAsync()
        {

            var data = await RamApi.Run("bday", "english");
            Console.WriteLine(data);

            if (data == null) return;

            if (data.Too_many_requests != null)
            {
                await RespondAsync("Ram api ratelimit reached try again in a few seconds");
                return;
            }

            EmbedBuilder embed = new EmbedBuilder()
            {
                Color = Color.Gold,
                Description = data.text,
                ImageUrl = data.url,
            };

            await RespondAsync(embed: embed.Build());




            return;
        }
    }
}
