using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TcBotDiscord.Modules
{
    public class Live : ModuleBase<SocketCommandContext>
    {
        [Command("Live")]
        public async Task ConnorAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Hail to the king baby!")
                .WithDescription("Mike is live over @ https://twitch.tv/nukem")
                .WithColor(Color.Orange);

            await ReplyAsync("", false, builder.Build());
        }
    }
}
