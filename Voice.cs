using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace TcBotDiscord.Modules
{
    public class Voice : ModuleBase<SocketCommandContext>
    {
        [Command("Voice", RunMode = RunMode.Async)]
        [RequireBotPermission(GuildPermission.ManageChannels)]
        public async Task CreateVoiceChannel(int time = 0)
        {
            time = time * 60000;
            int timeback = time;
            if (time == 0)
            {
                var use = await Context.Channel.SendMessageAsync("Use: ``!Voice {Time In Minutes}``");
                await Task.Delay(5000);
                await use.DeleteAsync();
            }
            if (time >= 60 * 60000)
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("!Voice");
                embed.AddInlineField("use", "!Voice {Ammount in minutes}");
                embed.AddInlineField("", "");
                embed.AddInlineField("maximum time", "60 minutes");
                embed.WithCurrentTimestamp();
                embed.WithColor(0, 0, 255);
                var m = await Context.Channel.SendMessageAsync("", embed: embed);
            }
            if (time == 1 * 60000)
            {
                await Context.Message.DeleteAsync();
                var v = await Context.Guild.CreateVoiceChannelAsync(name: $"{Context.User.Username}'s Voice Channel ({time / 60000} minute)");
                var me = await Context.Channel.SendMessageAsync($"A voice channel has been created! {Context.User.Mention}!");
                await me.ModifyAsync(m => { m.Content = $"A voice channel has been created! {Context.User.Mention}! {time / 60000} minute left"; });
                await Task.Delay(time);
                await v.DeleteAsync();
                await me.DeleteAsync();
            }
            if (time >= 2 * 60000 && time <= 60 * 60000)
            {
                await Context.Message.DeleteAsync();
                var v = await Context.Guild.CreateVoiceChannelAsync(name: $"{Context.User.Username}'s Voice Channel ({time / 60000} minutes)");
                var me = await Context.Channel.SendMessageAsync($"A voice channel has been created! {Context.User.Mention}!");
                Stopwatch s = new Stopwatch();
                s.Start();
                while (s.Elapsed < TimeSpan.FromSeconds(time))
                {
                    time = time - 1;
                    await me.ModifyAsync(m => { m.Content = $"A voice channel has been created! {Context.User.Mention}! {time / 60000 + 1} minute(s) left"; });
                    await v.ModifyAsync(x => { x.Name = $"{Context.User.Username}'s Voice Channel ({time / 60000 + 1} minutes)"; });
                    await Task.Delay(60000);
                    s.Restart();
                }
                s.Stop();
                await v.DeleteAsync();
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}, {timeback / 60000} minutes have passed, the voice channel has been deleted");
            }
        }

    }
}
