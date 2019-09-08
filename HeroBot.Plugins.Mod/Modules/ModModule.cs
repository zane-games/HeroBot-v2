using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Helpers;
using HeroBot.Plugins.Mod.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.Mod.Modules
{
    /// <summary>
    /// Represents the moderation commands
    /// </summary>
    [Cooldown(10)]
    [NeedPlugin()]
    public class ModModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// The <see cref="TempMuteService"/>
        /// </summary>
        private readonly TempMuteService _tempMuteService;
        /// <summary>
        /// Initializes the commands
        /// </summary>
        /// <param name="tempMuteService">From dependcy injection</param>
        public ModModule(TempMuteService tempMuteService)
        {
            _tempMuteService = tempMuteService;
        }
        /// <summary>
        /// Kicks a user from a server
        /// </summary>
        /// <param name="target">The victim</param>
        /// <param name="reason">Why</param>
        /// <returns></returns>
        [Command("kick"), Alias("k")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Kick a user from the server.")]
        public async Task KickMember(SocketGuildUser target, [Remainder]string reason = "*no reason*")
        {
            // Check if we can kick the user
            if (this.Context.Guild.CurrentUser.Hierarchy > target.Hierarchy)
            {
                // We try to sent a dm to the user
                bool canSentMp = true;
                try
                {
                    var mp = await target.GetOrCreateDMChannelAsync();
                    await mp.TriggerTypingAsync();
                    await mp.SendMessageAsync($"Hey {target.Mention} ! You have been **kicked** from `{Context.Guild.Name}` for `{reason.Replace("noinvite", String.Empty)}` {(reason.Contains("noinvite") ? $"you can re-join the server with this invite https://discord.gg/{(await Context.Guild.DefaultChannel.CreateInviteAsync(maxUses: 1, isUnique: true)).Code}" : String.Empty)}");
                }
                catch (Exception)
                {
                    canSentMp = false;
                }
                finally
                {
                    // Finally, we kick the user from th server
                    await target.KickAsync($"Requested by {Context.User.Username}#{Context.User.Discriminator} : {reason}");
                    await ReplyAsync($"<:check:606088713897902081> `{target.Username}#{target.Discriminator} {(target.Nickname == null ? String.Empty : $"({target.Nickname})")}` has been kicked out from the server ! {(canSentMp ? "i've sent him the reason in private message !" : "i can't send him a message in DM")}");
                }
            }
            else
            {
                // I we can't kick the user from the server
                await ReplyAsync($"<:fail:606088713705095208> {target.Mention} has too much power for me ! Please check if the user is behind me !");
            }
        }
        /// <summary>
        /// Definitively ban a user from a server
        /// </summary>
        /// <param name="target">The victim</param>
        /// <param name="reason">Why</param>
        /// <returns></returns>
        [Command("ban"), Alias("b")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [Summary("Definitively ban a user from the server.")]
        public async Task BanMember(SocketGuildUser target, [Remainder]string reason = "*no reason*")
        {
            // Check if we can kick the user
            if (this.Context.Guild.CurrentUser.Hierarchy > target.Hierarchy)
            {
                // We try to sent a dm to the user
                bool canSentMp = true;
                try
                {
                    var mp = await target.GetOrCreateDMChannelAsync();
                    await mp.TriggerTypingAsync();
                    await mp.SendMessageAsync($"Hey {target.Mention} ! You have been **banned** from `{Context.Guild.Name}` for `{reason}`");
                }
                catch (Exception)
                {
                    canSentMp = false;
                }
                finally
                {
                    // Finally, we ban the user
                    await target.Guild.AddBanAsync(target, 7, $"Requested by {Context.User.Username}#{Context.User.Discriminator} : {reason}");
                    await ReplyAsync($"<:check:606088713897902081> `{target.Username}#{target.Discriminator} {(target.Nickname == null ? String.Empty : $"({target.Nickname})")}` has been banned from the server ! {(canSentMp ? "i've sent him the reason in private message !" : "i can't send him a message in DM")}");
                }
            }
            else
            {
                // If we can't ban the user
                await ReplyAsync($"<:fail:606088713705095208> {target.Mention} has too much power for me ! Please check if the user is behind me !");
            }
        }
        /// <summary>
        /// SoftBan, ban a user then deban the user
        /// </summary>
        /// <param name="target">Victim</param>
        /// <param name="reason">Why</param>
        /// <returns></returns>
        [Command("softban"), Alias("sb")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Summary("Kick the user from the server and delete their messages.")]
        public async Task SoftBan(SocketGuildUser target, [Remainder]string reason = "*no reason*")
        {
            // Check if we can kick the user
            if (this.Context.Guild.CurrentUser.Hierarchy > target.Hierarchy)
            {
                // We try to sent a dm to the user
                bool canSentMp = true;
                try
                {
                    var mp = await target.GetOrCreateDMChannelAsync();
                    await mp.TriggerTypingAsync();
                    await mp.SendMessageAsync($"Hey {target.Mention} ! You have been **softbanned** from `{Context.Guild.Name}` for `{reason}`");
                }
                catch (Exception)
                {
                    canSentMp = false;
                }
                finally
                {
                    // Finally, we softban the user
                    await target.Guild.AddBanAsync(target, 7, $"Requested by {Context.User.Username}#{Context.User.Discriminator} : {reason}");
                    await target.Guild.RemoveBanAsync(target);
                    await ReplyAsync($"<:check:606088713897902081> `{target.Username}#{target.Discriminator} {(target.Nickname == null ? String.Empty : $"({target.Nickname})")}` has been softbanned from the server ! {(canSentMp ? "i've sent him the reason in private message !" : "i can't send him a message in DM")}");
                }
            }
            else
            {
                // If we can't ban the user
                await ReplyAsync($"<:fail:606088713705095208> {target.Mention} has too much power for me ! Please check if the user is behind me !");
            }
        }
        /// <summary>
        /// Clear a certain amount of messages
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [Command("clear"), Alias("c")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireContext(ContextType.Guild)]
        [Summary("Deletes a certain count of messages.")]
        public async Task Clear(int count)
        {
            // We can't delete more than 300 messages
            if (count > 300)
            {
                await ReplyAsync("<:fail:606088713705095208> You can't delete more than **300** messages :/");
                return;
            }
            var items = (await Context.Channel.GetMessagesAsync(count + 1).Flatten().ToArray()).Where(x => x.Timestamp > DateTime.Now.AddDays(-14));
            var textChannel = Context.Channel as SocketTextChannel;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await textChannel.DeleteMessagesAsync(items);
            stopWatch.Stop();
            var message = await ReplyAsync($"<:check:606088713897902081> I've deleted {items.Count() - 1} message in {stopWatch.ElapsedMilliseconds / 1000}s *( this message will disapear in 3s )*");
            await Task.Delay(3000);
            await message.DeleteAsync();
        }
        [Command("setupMute")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        [Summary("Creates & configure the mute role.")]
        public async Task SetupMute() {
            // First, we need to create the role
            try
            {
                var role = await Context.Guild.CreateRoleAsync("Muted", new GuildPermissions(sendMessages: false, addReactions: false, speak: false), Color.DarkerGrey);
                foreach (IGuildChannel channel in Context.Guild.Channels)
                {
                    await channel.AddPermissionOverwriteAsync(role, new OverwritePermissions(sendMessages: PermValue.Deny, addReactions: PermValue.Deny, speak: PermValue.Deny));
                }
                await ReplyAsync("Setup finished !");
            }
            catch (Exception) {
                await ReplyAsync("Something wrong happended... Please, check my permissions");
            }
        }
        /// <summary>
        /// Deny the permission "talk" to a user
        /// </summary>
        /// <param name="target"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [Command("mute"), Alias("m")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireContext(ContextType.Guild)]
        [Summary("Mutes a user from a server.")]
        public async Task Mute(SocketGuildUser target, [Remainder]string reason = "*no reason*")
        {
            // Definitively need some optimizations
            if (this.Context.Guild.CurrentUser.Hierarchy > target.Hierarchy)
            {
                if (Context.Guild.Roles.Any(x => x.Name.ToLower().Contains("mute")))
                {
                    var mp = await target.GetOrCreateDMChannelAsync();
                    bool canSentMp = true;
                    try
                    {
                        await mp.TriggerTypingAsync();
                        await mp.SendMessageAsync($"Hey {target.Mention} ! You have been **muted** in `{Context.Guild.Name}` for `{reason}`");
                    }
                    catch (Exception)
                    {
                        canSentMp = false;
                    }
                    finally
                    {
                        var role = Context.Guild.Roles.First(x => x.Name.ToLower().Contains("mute"));
                        await target.AddRoleAsync(role);
                        await ReplyAsync($"<:check:606088713897902081> `{target.Username}#{target.Discriminator} {(target.Nickname == null ? String.Empty : $"({target.Nickname})")}` has been muted in this server ! {(canSentMp ? "i've sent him the reason in private message !" : "i can't send him a message in DM")}");
                    }
                }
                else
                    await ReplyAsync("<:fail:606088713705095208> There is no muted role...");
            }
            else
                await ReplyAsync($"<:fail:606088713705095208> {target.Mention} has too much power for me ! Please check if the user is behind me !");
        }
        /// <summary>
        /// Add the permission "talk" to a user
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [Command("unmute"), Alias("um")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireContext(ContextType.Guild)]
        public async Task UnMute(SocketGuildUser target)
        {
            if (this.Context.Guild.CurrentUser.Hierarchy > target.Hierarchy)
            {
                if (Context.Guild.Roles.Any(x => x.Name.ToLower().Contains("mute")))
                {
                    var mp = await target.GetOrCreateDMChannelAsync();
                    bool canSentMp = true;
                    try
                    {
                        await mp.TriggerTypingAsync();
                        await mp.SendMessageAsync($"Hey {target.Mention} ! You have been **unmuted** in `{Context.Guild.Name}`");
                    }
                    catch (Exception)
                    {
                        canSentMp = false;
                    }
                    finally
                    {
                        var role = Context.Guild.Roles.First(x => x.Name.ToLower().Contains("mute"));
                        await target.RemoveRoleAsync(role);
                        await ReplyAsync($"<:check:606088713897902081> `{target.Username}#{target.Discriminator} {(target.Nickname == null ? String.Empty : $"({target.Nickname})")}` has been unmutes in this server ! {(canSentMp ? "i've sent him a message in private message !" : "i can't send him a message in DM")}");
                    }
                }
                else
                    await ReplyAsync("<:fail:606088713705095208> There is no muted role...");
                }
            else
            {
                await ReplyAsync($"<:fail:606088713705095208> {target.Mention} has too much power for me ! Please check if the user is behind me !");
            }
        }
        /// <summary>
        /// Add a user to a channel
        /// </summary>
        /// <param name="target"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        [Command("add"), Alias("a")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireContext(ContextType.Guild)]
        public async Task AddToChannel(SocketGuildUser target, SocketTextChannel channel = null)
        {
            if (this.Context.Guild.CurrentUser.Hierarchy > target.Hierarchy)
            {
                if (channel == null)
                    channel = Context.Channel as SocketTextChannel;
                if (channel.Users.Any(x => x.Id == target.Id))
                {

                    await ReplyAsync($"... This user is aleady here ...");
                }
                else
                {
                    await channel.AddPermissionOverwriteAsync(target, new OverwritePermissions(sendMessages: PermValue.Allow, viewChannel: PermValue.Allow, readMessageHistory: PermValue.Allow));
                    await ReplyAsync($"<:check:606088713897902081> The user was added to {channel.Mention}");
                }
            }
            else
            {
                await ReplyAsync($"<:fail:606088713705095208> {target.Mention} has too much power for me ! Please check if the user is behind me !");
            }
        }
        /// <summary>
        /// Remove a user to a channel
        /// </summary>
        /// <param name="target"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        [Command("remove"), Alias("rm")]
        [RequireBotPermission(GuildPermission.ManageChannels)]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        [RequireContext(ContextType.Guild)]
        public async Task RemoveTochannel(SocketGuildUser target, SocketTextChannel channel = null)
        {
            if (this.Context.Guild.CurrentUser.Hierarchy > target.Hierarchy)
            {
                if (channel == null)
                    channel = Context.Channel as SocketTextChannel;
                if (!channel.Users.Any(x => x.Id == target.Id))
                {
                    await ReplyAsync($"<:fail:606088713705095208> This user is already here !");
                }
                else
                {
                    if (channel.GetPermissionOverwrite(target) != null)
                        await channel.RemovePermissionOverwriteAsync(target);
                    await ReplyAsync("<:check:606088713897902081> This user was removed from this channel");
                }
            }
            else
            {
                await ReplyAsync($"<:fail:606088713705095208> {target.Mention} has too much power for me ! Please check if the user is behind me !");
            }
        }
        /// <summary>
        /// Lock a channel
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        [Command("lock"), Alias("l")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireContext(ContextType.Guild)]
        public async Task LockChannel(SocketTextChannel channel = null)
        {
            if (channel == null)
                channel = (SocketTextChannel)Context.Channel;
            foreach (var role in Context.Guild.Roles)
            {
                if (!role.Permissions.ManageMessages && !role.Permissions.ManageChannels)
                {
                    await channel.AddPermissionOverwriteAsync(role, new OverwritePermissions(sendMessages: PermValue.Deny));
                }
            }
            await ReplyAsync("<:check:606088713897902081> This channel is now locked :lock:");
        }
        /// <summary>
        /// Unlock a channel
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        [Command("unlock"), Alias("ul")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireContext(ContextType.Guild)]
        public async Task UnLockChannel(SocketTextChannel channel = null)
        {
            if (channel == null)
                channel = (SocketTextChannel)Context.Channel;
            foreach (var role in Context.Guild.Roles)
            {
                if (!role.Permissions.ManageMessages && !role.Permissions.ManageChannels && channel.GetPermissionOverwrite(role) != null)
                {
                        await channel.RemovePermissionOverwriteAsync(role);
                }
            }
            await ReplyAsync("<:check:606088713897902081> | This channel is now unlocked !");
        }
        /// <summary>
        /// Sent a message
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Command("say"), Alias("s")]
        [Summary("Made the bots say something")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        public async Task Say([Remainder]string text) {
            await Context.Message.DeleteAsync();
            await ReplyAsync(text.Replace("@everyone", "`@everyone`").Replace("@here", "`@here`"));
        }
        /// <summary>
        /// Temporaly deny the permission "talk" to a user
        /// </summary>
        /// <param name="target"></param>
        /// <param name="time"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [Command("tempmute"), Alias("tm")]
        [RequireBotPermission(GuildPermission.SendMessages)]
        [RequireUserPermission(ChannelPermission.ManageChannels)]
        public async Task TempMute(SocketGuildUser target, TimeSpan time, [Remainder]string reason = "*no reason*")
        {

            if (this.Context.Guild.CurrentUser.Hierarchy > target.Hierarchy)
            {
                if (Context.Guild.Roles.Any(x => x.Name.ToLower().Contains("mute")))
                {
                    if (await _tempMuteService.CreatetempMute(new TempMute() { GuildId = Context.Guild.Id, UserId = target.Id, Reason = reason, TimeSpan = time }))
                    {
                        var mp = await target.GetOrCreateDMChannelAsync();
                        bool canSentMp = true;
                        try
                        {
                            await mp.TriggerTypingAsync();
                            await mp.SendMessageAsync($"Hey {target.Mention} ! You have been **tempmuted** from `{Context.Guild.Name}` for `{reason.Replace("noinvite", String.Empty)}` during {time.ToHumanReadable()} {(reason.Contains("noinvite") ? $"you can re-join the server with this invite https://discord.gg/{(await Context.Guild.DefaultChannel.CreateInviteAsync(maxUses: 1, isUnique: true)).Code}" : String.Empty)}");
                        }
                        catch (Exception)
                        {
                            canSentMp = false;
                        }
                        finally
                        {
                            var role = Context.Guild.Roles.First(x => x.Name.ToLower().Contains("mute"));
                            await target.AddRoleAsync(role);
                            await ReplyAsync($"<:check:606088713897902081> `{target.Username}#{target.Discriminator} {(target.Nickname == null ? String.Empty : $"({target.Nickname})")}` has been muted for {time.ToHumanReadable()}s from the server ! {(canSentMp ? "i've sent him the reason in private message !" : "i can't send him a message in DM")}");
                        }
                    }
                    else
                    {
                        await ReplyAsync($"<:fail:606088713705095208> `{target.Username}#{target.Discriminator} {(target.Nickname == null ? String.Empty : $"({target.Nickname})")}` is already muted ...");
                    }
                }
                else
                    await ReplyAsync("<:fail:606088713705095208> There is no muted role... you can setup the muted role with the `hb!setupmute` command.");
            }
            else
            {
                await ReplyAsync($"<:fail:606088713705095208> {target.Mention} has too much power for me ! Please check if the user is behind me !");
            }
        }

        [Command("mention")]
        [RequireBotPermission(GuildPermission.Administrator)]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        public async Task Mention([Remainder]string role) {
            if (Context.Guild.Roles.Any(x => x.Name.ToLower().Contains(role.ToLower())))
            {
                var roleE = Context.Guild.Roles.First(x => x.Name.Contains(role));
                var needtoEdit = !roleE.IsMentionable;
                if (needtoEdit)
                    await roleE.ModifyAsync(x => x.Mentionable = true);
                await ReplyAsync(roleE.Mention);
                if (needtoEdit)
                    await roleE.ModifyAsync(x => x.Mentionable = false);
            }
            else await ReplyAsync($"I can't fint a role matching to `{role}`");
        }

        /// <summary>
        /// User to create & setup embeds
        /// </summary>
        [Cooldown(1)]
        [Group("embed")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public class Embed : ModuleBase<SocketCommandContext>
        {
            [Command("create")]
            [Summary("Creates an embed for you :3")]
            public async Task CreateEmbed(SocketTextChannel channel, string title, string link = null, uint color = 0xFFF)
            {
                var Embed = new EmbedBuilder();
                if (color == 0xFFF)
                    Embed.WithRandomColor();
                else
                    Embed.WithColor(new Color(color));
                if (link != null)
                    Embed.WithUrl(link);
                Embed.WithTitle(title);
                await channel.SendMessageAsync(embed: Embed.Build());
                await ReplyAsync("Embed sent !");
            }
            #region import/export
            [Command("import")]
            [Summary("Import an embed via .json")]
            public async Task ImportEmbed(SocketTextChannel channel, ulong id = 0)
            {
                if (Context.Message.Attachments.Count == 1 && Context.Message.Attachments.First().Filename.EndsWith(".json"))
                {
                        var json = "";
                        using (WebClient client = new WebClient())
                        {
                            json = await client.DownloadStringTaskAsync(Context.Message.Attachments.First().Url);
                        }
                        try {
                            EmbedExport embed = JsonConvert.DeserializeObject<EmbedExport>(json);
                            var embedbuilder = new EmbedBuilder();
                            embedbuilder.WithColor(embed.color)
                            .WithUrl(embed.url)
                            .WithThumbnailUrl(embed.thubmail)
                            .WithTitle(embed.title)
                            .WithDescription(embed.description);
                            if (embed.timeStamp.HasValue)
                                embedbuilder.WithTimestamp(embed.timeStamp.Value);
                            foreach (EmbedExportField embedExportField in embed.embedFields) {
                                embedbuilder.AddField(embedExportField.title, embedExportField.content, embedExportField.inline);
                            }
                            if (embed.embedAuthor != null) {
                                embedbuilder.WithAuthor(embed.embedAuthor.name, embed.embedAuthor.icon, embed.embedAuthor.url);
                            }
                            if (embed.embedFooter != null) {
                                embedbuilder.WithFooter(embed.embedFooter.text, embed.embedFooter.iconurl);
                            }
                            if (id != 0)
                            {
                                var message = (IUserMessage)await channel.GetMessageAsync(id);
                                if (message != null && message.Author.Id == Context.Client.CurrentUser.Id)
                                {
                                    await message.ModifyAsync(x => x.Embed = embedbuilder.Build());
                                    await ReplyAsync("<:fail:606088713705095208> I've edited the embed");
                                    return;
                                }
                                await ReplyAsync("<:fail:606088713705095208> I can't find the message !");
                                return;
                            }
                            else {
                                await channel.SendMessageAsync(embed: embedbuilder.Build());
                                await ReplyAsync("Embed sent !");
                            }
                        } catch (Exception) {
                            await ReplyAsync("<:fail:606088713705095208> Invalid file.");
                        }
                    
                }

            }
            [Command("export")]
            [Summary("Export an embed as .json")]
            public async Task ExportEmbed(SocketTextChannel channel, ulong id)
            {
                var message = (IUserMessage)await channel.GetMessageAsync(id);
                if (message != null && message.Author.Id == Context.Client.CurrentUser.Id)
                {
                    var embed = message.Embeds.First();
                    var export = new EmbedExport() {
                        description = embed.Description ?? string.Empty,
                        color = embed.Color.HasValue ? embed.Color.Value.RawValue : 0xFFF,
                        thubmail = embed.Thumbnail.HasValue ? embed.Thumbnail.Value.Url : string.Empty,
                        title = embed.Title ?? "",
                        timeStamp = embed.Timestamp,
                        url = embed.Url
                    };
                    List<EmbedExportField> fields = new List<EmbedExportField>();
                    foreach (EmbedField embedField in embed.Fields)
                    {
                        fields.Add(new EmbedExportField()
                        {
                            content = embedField.Value,
                            inline = embedField.Inline,
                            title = embedField.Name
                        });
                    }
                    export.embedFields = fields.ToArray();
                    if (embed.Author.HasValue) {
                        export.embedAuthor = new EmbedExportAuthor()
                        {
                            icon = embed.Author.Value.IconUrl,
                            name = embed.Author.Value.Name,
                            url = embed.Author.Value.Url
                        };
                    }
                    if (embed.Footer.HasValue) {
                        export.embedFooter = new EmbedExportFooter()
                        {
                            text = embed.Footer.Value.Text,
                            iconurl = embed.Footer.Value.IconUrl
                        };
                    }
                    var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(export)));
                    try
                    {
                        await Context.Channel.SendFileAsync(stream, $"embed-{message.Id}.json");
                    }
                    catch (Exception) { /* When we can't sent the file, then, we send a message */ await ReplyAsync("<:fail:606088713705095208> I can't send the file ..."); }
                    finally {
                        stream.Close();
                    }

                    return;
                }
                await ReplyAsync("I can't find the message !");
            }
            #endregion


            [Command("thumbmail")]
            [Summary("Set the thumbmail of your embed")]
            public async Task SetThumbmail(SocketTextChannel channel, ulong id, string imageUrl = null)
            {
                var message = (IUserMessage)await channel.GetMessageAsync(id);
                if (message != null && message.Author.Id == Context.Client.CurrentUser.Id)
                {
                    var embedBuilder = message.Embeds.First().ToEmbedBuilder();
                    if (imageUrl != null)
                        embedBuilder.WithThumbnailUrl(imageUrl);
                    else
                        embedBuilder.ThumbnailUrl = null;
                    await message.ModifyAsync(x => x.Embed = embedBuilder.Build());
                    await ReplyAsync("I've edited the embed");
                    return;
                }
                await ReplyAsync("I can't find the message !");
            }
            [Command("description")]
            [Summary("Set the description of your embed")]
            public async Task SetDescription(SocketTextChannel channel, ulong id, string description = null)
            {
                var message = (IUserMessage)await channel.GetMessageAsync(id);
                if (message != null && message.Author.Id == Context.Client.CurrentUser.Id)
                {
                    var embedBuilder = message.Embeds.First().ToEmbedBuilder();
                    if (description != null)
                        embedBuilder.WithDescription(description);
                    else
                        embedBuilder.Description = null;
                    await message.ModifyAsync(x => x.Embed = embedBuilder.Build());
                    await ReplyAsync("I've edited the embed");
                    return;
                }
                await ReplyAsync("I can't find the message !");
            }
            [Command("color")]
            [Summary("Set the color of your embed")]
            public async Task SetColor(SocketTextChannel channel, ulong id, uint color = 0xFFF)
            {
                var message = (IUserMessage)await channel.GetMessageAsync(id);
                if (message != null && message.Author.Id == Context.Client.CurrentUser.Id)
                {
                    var embedBuilder = message.Embeds.First().ToEmbedBuilder();
                    embedBuilder.WithColor(color);
                    await message.ModifyAsync(x => x.Embed = embedBuilder.Build());
                    await ReplyAsync("I've edited the embed");
                    return;
                }
                await ReplyAsync("I can't find the message !");
            }
            [Command("title")]
            [Summary("Set the title of your embed")]
            public async Task SetTitle(SocketTextChannel channel, ulong id, string title = null)
            {
                var message = (IUserMessage)await channel.GetMessageAsync(id);
                if (message != null && message.Author.Id == Context.Client.CurrentUser.Id)
                {
                    var embedBuilder = message.Embeds.First().ToEmbedBuilder();
                    if (title != null)
                        embedBuilder.WithTitle(title);
                    else
                        embedBuilder.Title = null;
                    await message.ModifyAsync(x => x.Embed = embedBuilder.Build());
                    await ReplyAsync("I've edited the embed");
                    return;
                }
                await ReplyAsync("I can't find the message !");
            }
            [Command("url")]
            [Summary("Set the url of your embed")]
            public async Task SetUrl(SocketTextChannel channel, ulong id, string url = null)
            {
                var message = (IUserMessage)await channel.GetMessageAsync(id);
                if (message != null && message.Author.Id == Context.Client.CurrentUser.Id)
                {
                    var embedBuilder = message.Embeds.First().ToEmbedBuilder();
                    if (url != null)
                        embedBuilder.WithUrl(url.ToString());
                    else
                        embedBuilder.Url = null;
                    await message.ModifyAsync(x => x.Embed = embedBuilder.Build());
                    await ReplyAsync("I've edited the embed");
                    return;
                }
                await ReplyAsync("I can't find the message !");
            }
            [Command("addfield")]
            [Summary("Add a field tu your embed")]
            public async Task AddField(SocketTextChannel channel, ulong id,string name,string value,bool inline)
            {
                var message = (IUserMessage)await channel.GetMessageAsync(id);
                if (message != null && message.Author.Id == Context.Client.CurrentUser.Id)
                {
                    var embedBuilder = message.Embeds.First().ToEmbedBuilder();
                    embedBuilder.AddField(name, value, inline);
                    await message.ModifyAsync(x => x.Embed = embedBuilder.Build());
                    await ReplyAsync("I've edited the embed");
                    return;
                }
                await ReplyAsync("I can't find the message !");
            }
            struct EmbedExport
            {
                public string title;
                public uint color;
                public string thubmail;
                public string description;
                public string url;
                public DateTimeOffset? timeStamp;
                public EmbedExportField[] embedFields;
                public EmbedExportAuthor embedAuthor;
                public EmbedExportFooter embedFooter;
            }
            struct EmbedExportField
            {
                public string title;
                public string content;
                public bool inline;
            }
            class EmbedExportFooter
            {
                public string text;
                public string iconurl;
            }
            class EmbedExportAuthor
            {
                public string name;
                public string icon;
                public string url;
            }
        }

    }
}
