using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using ModBot.Business.Services;
using ModBot.Domain.Interfaces;

namespace ModBot.Bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
      private readonly ICommandLogic _commandLogic;

         Commands(ICommandLogic commandLogic)
        {
           _commandLogic = commandLogic;
        }


        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("pong");

        }

    }
}
