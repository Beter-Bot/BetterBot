using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBot.Commands
{
    public class hello : ModuleBase
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;

        public hello(DiscordSocketClient bot, CommandService command)
        {
            this.client = bot;
            this.commands = command;
        }

        [Command("hello"), Alias("hi")]
        [Summary("get a hello")]
        public async Task helloAsync()
        {

            

            await ReplyAsync($"Hello");
            return;
        }
    }
}
