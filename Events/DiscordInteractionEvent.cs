using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBot.Events
{
    public class DiscordInteractionEvent
    {
        private readonly DiscordShardedClient client;
        private readonly InteractionService commandService;
        private readonly IServiceProvider serviceProvider;

        public DiscordInteractionEvent(DiscordShardedClient bot, InteractionService commands, IServiceProvider services)
        {
            this.client = bot;
            this.commandService = commands;
            this.serviceProvider = services;

            bot.InteractionCreated += Bot_InteractionCreated;
            commands.InteractionExecuted += Commands_InteractionExecuted;


        }

        private async Task Bot_InteractionCreated(SocketInteraction arg)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();

                var ctx = new InteractionContext(client, arg);
                var resualt = await commandService.ExecuteCommandAsync(ctx, scope.ServiceProvider);
            }
            catch (Exception ex)
            {
                if (arg.Type == Discord.InteractionType.ApplicationCommand) await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());

                Console.WriteLine(ex);
            }
        }

        public Task Commands_InteractionExecuted(ICommandInfo arg1, Discord.IInteractionContext arg2, Discord.Interactions.IResult args3)
        {
            if (args3.IsSuccess) return Task.CompletedTask;

            

            switch (args3.Error)
            {
                case InteractionCommandError.Exception:
                   

                 

                    Console.WriteLine("Slash Command Exception:: " + args3.ErrorReason);
                    break;
                case InteractionCommandError.Unsuccessful:
                    

                    
                    Console.WriteLine("Error! {0}: " + args3.ErrorReason);
                    break;
                case InteractionCommandError.UnmetPrecondition:
                    
                    Console.WriteLine("Precondition Error: {0}: " + args3.ErrorReason);
                    break;
                default:
                    

                    Console.WriteLine("Command Error: " + args3.ErrorReason);
                    break;
            }

            return Task.CompletedTask;
           

        }

       
    }
}
