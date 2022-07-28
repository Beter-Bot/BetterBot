using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBot.Events
{
    public class DiscordMessageEvent
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commandService;
        private readonly IServiceProvider serviceProvider;

        public DiscordMessageEvent(DiscordSocketClient bot, CommandService commands, IServiceProvider services)
        {
            bot.MessageReceived += Message;
            this.client = bot;
            this.commandService = commands;
            this.serviceProvider = services;


        }

        public async Task Message(SocketMessage message)
        {
            if (message.Author.IsBot || message.Author.IsWebhook)
                return;

            var msg = message as SocketUserMessage;

            var argpos = 0;

            if (!(msg.HasStringPrefix(BotConfig.PREFIX, ref argpos) || msg.HasMentionPrefix(client.CurrentUser, ref argpos)))
                return;



            var context = new SocketCommandContext(client, msg);
            var result = await commandService.ExecuteAsync(context, argpos, serviceProvider);

            if (result.IsSuccess)
            {
                Console.WriteLine(result);


                return;
            }



            switch (result.Error)
            {
                case CommandError.BadArgCount:
                case CommandError.UnmetPrecondition:
                case CommandError.Unsuccessful:



                    if (result.Error is CommandError.UnmetPrecondition)
                    {
                        Console.WriteLine(result.ErrorReason);



                        await message.Channel.SendMessageAsync($"Error: {result.ErrorReason}");
                        return;
                    }
                    Console.WriteLine(result.ErrorReason);

                    await message.Channel.SendMessageAsync("Error: contact Gamearoo#0001 to repot the error!");
                    break;
                default:
                    Console.WriteLine(result.ErrorReason);


                    break;
            }
        }
    }
}
