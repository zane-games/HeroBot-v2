using Discord;
using Discord.Commands;
using HeroBot.Common.Attributes;
using HeroBot.Common.Helpers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace HeroBot.Plugins.Images.Modules
{
    [Cooldown(10)]
    [NeedPlugin()]
    [Name("Images/Picture Generation")]
    public class ImagesModule : ModuleBase<SocketCommandContext>
    {
        private readonly Random _random;
        private readonly static string[] CatGreet = new[] { "dream", "royal", "beautiful", "great", "funny", "good looking" };
        private readonly static string[] CatGreetings = new[] { "Let's find a __great__ cat for you :3", "Here is a __great__ cat !", "I've found this cat __great__ for you..." };
        private readonly static string[] DogGreet = new[] { "dream", "royal", "beautiful", "great", "funny", "good looking", "loyal" };
        private readonly static string[] DogGreetings = new[] { "Let's find a __great__ dog for you :3", "Here is a __great__ dog !", "I've found this __great__ dog for you..." };
        private readonly static string CatUrl = "http://aws.random.cat/meow";
        private readonly static string DogUrl = "https://random.dog/woof";
        private readonly static string EclyssiaUrl = "https://eclyssia-api.tk/api/v1";
        public ImagesModule(Random random)
        {
            _random = random;
        }

        [Command("cat"), Alias("meow")]
        public Task RandomCat()
        {
            using WebClient webclient = new WebClient();
            return webclient.DownloadStringTaskAsync(new Uri(CatUrl)).ContinueWith(async (x) =>
            {
                var url = JsonConvert.DeserializeObject<dynamic>(x.Result).file;
                await ReplyAsync($":cat:  {Context.User.Mention} {CatGreetings[_random.Next() % CatGreetings.Length].Replace("__great__", CatGreet[_random.Next() % CatGreet.Length])}", false, new EmbedBuilder()
                {
                    ImageUrl = url
                }.WithRandomColor().WithCopyrightFooter(Context.User.Username, "cat").Build());
                webclient.Dispose();
            });
        }

        [Command("dog"), Alias(new[] { "wouf", "woof" })]
        public Task RandomDog()
        {
            using WebClient webclient = new WebClient();
            return webclient.DownloadStringTaskAsync(new Uri(DogUrl)).ContinueWith(async (x) =>
{
    await ReplyAsync($":dog: {Context.User.Mention} {DogGreetings[_random.Next() % DogGreetings.Length].Replace("__great__", DogGreet[_random.Next() % DogGreet.Length])}", false, new EmbedBuilder()
    {
        ImageUrl = $"https://random.dog/{x.Result}"
    }.WithRandomColor().WithCopyrightFooter(Context.User.Username, "dog").Build());
    webclient.Dispose();
});
        }

        [Command("pixelate")]
        public async Task Pixelate(string url = null)
        {
            var rurl = ResolveUrl(url);
            using HttpClient client = new HttpClient();
            var bytearray = await client.GetByteArrayAsync($"{EclyssiaUrl}/pixelate?url={rurl}");
            var stream = new MemoryStream(bytearray);
            try
            {
                await Context.Channel.SendFileAsync(stream, "pixelate.png", embed: new EmbedBuilder()
                .WithRandomColor()
                .WithAuthor(Context.User)
                .WithImageUrl("attachment://pixelate.png").Build());
            }
            catch (Exception) { /* Ignore message send errors */}
            finally {
                // Close the stream
                stream.Close();
            }
        }
        [Command("sepia")]
        public async Task Sepia(string url = null)
        {
            var rurl = ResolveUrl(url);
            using HttpClient client = new HttpClient();
            var bytearray = await client.GetByteArrayAsync($"{EclyssiaUrl}/sepia?url={rurl}");
            var stream = new MemoryStream(bytearray);
            try
            {
                await Context.Channel.SendFileAsync(stream, "sepia.png", embed: new EmbedBuilder()
                .WithRandomColor()
                .WithAuthor(Context.User)
                .WithImageUrl("attachment://sepia.png").Build());
            }
            catch (Exception) { /* Ignore message send errors */}
            finally
            {
                // Close the stream
                stream.Close();
            }
            client.Dispose();
        }

        [Command("mirror")]
        public async Task Mirror(string url = null)
        {
            var rurl = ResolveUrl(url);
            using HttpClient client = new HttpClient();
            var bytearray = await client.GetByteArrayAsync($"{EclyssiaUrl}/sepia?url={rurl}");
            var stream = new MemoryStream(bytearray);
            try
            {
                await Context.Channel.SendFileAsync(stream, "sepia.png", embed: new EmbedBuilder()
                .WithRandomColor()
                .WithAuthor(Context.User)
                .WithImageUrl("attachment://sepia.png").Build());
            }
            catch (Exception) { /* Ignore message send errors */}
            finally
            {
                // Close the stream
                stream.Close();
            }
            client.Dispose();
        }

        [Command("invert")]
        public async Task Invert(string url = null)
        {
            var rurl = ResolveUrl(url);
            using HttpClient client = new HttpClient();
            Console.WriteLine(rurl);
            var bytearray = await client.GetByteArrayAsync($"http://imageapi:3000/invert?imageurl={rurl}");
            var stream = new MemoryStream(bytearray);
            await Context.Channel.SendFileAsync(stream, "invert.png", embed: new EmbedBuilder().WithImageUrl("attachment://invert.png").WithCopyrightFooter()
                .WithRandomColor()
                .WithAuthor(Context.User).Build());
        }

        [Command("upsidesdown")]
        public async Task Upsidesown(string url = null)
        {
            var rurl = ResolveUrl(url);
            using HttpClient client = new HttpClient();
            var bytearray = await client.GetByteArrayAsync($"http://imageapi:3000/upsidesown?imageurl={rurl}");
            var stream = new MemoryStream(bytearray);
            await Context.Channel.SendFileAsync(stream, "upsidesdown.png", embed: new EmbedBuilder().WithCopyrightFooter()
                .WithRandomColor()
                .WithAuthor(Context.User).WithImageUrl("attachment://upsidesdown.png").Build());
            stream.Close();
        }

        [Command("blur")]
        public async Task Blur(string url = null)
        {
            var rurl = ResolveUrl(url);
            using HttpClient client = new HttpClient();
            var bytearray = await client.GetByteArrayAsync($"http://imageapi:3000/blur?imageurl={rurl}");
            var stream = new MemoryStream(bytearray);
            await Context.Channel.SendFileAsync(stream, "blur.png", embed: new EmbedBuilder().WithCopyrightFooter()
                .WithRandomColor()
                .WithAuthor(Context.User).WithImageUrl("attachment://blur.png").Build());
        }


        [Command("rgb")]
        public async Task RGB(string url = null)
        {
            var rurl = ResolveUrl(url);
            using HttpClient client = new HttpClient();
            var bytearray = await client.GetByteArrayAsync($"http://imageapi:3000/rgb?imageurl={rurl}");
            var stream = new MemoryStream(bytearray);
            await Context.Channel.SendFileAsync(stream, "rgb.gif", embed: new EmbedBuilder().WithCopyrightFooter()
                .WithRandomColor()
                .WithAuthor(Context.User).WithImageUrl("attachment://rgb.gif").Build());
            stream.Close();
        }

        [Command("levelcard")]
        public async Task LevelCard(string url = null)
        {
            var rurl = ResolveUrl(url);
            using HttpClient client = new HttpClient();
            var bytearray = await client.GetByteArrayAsync($"http://imageapi:3000/levelcard?imageurl={rurl}");
            var stream = new MemoryStream(bytearray);
            await Context.Channel.SendFileAsync(stream, "levelcard.png", embed: new EmbedBuilder().WithCopyrightFooter()
                .WithRandomColor()
                .WithAuthor(Context.User).WithImageUrl("attachment://levelcard.png").Build());
            stream.Close();
        }
        private string ResolveUrl(string url)
        {
            if (url != null)
            {
                return HttpUtility.UrlEncode(url);
            }
            else if (Context.Message.Attachments.Count == 1)
            {
                return HttpUtility.UrlEncode($"{Context.Message.Attachments.First().Url}?size=480");
            }
            else
            {
                return HttpUtility.UrlEncode(Context.User.AvatarId == null ? $"{Context.User.GetDefaultAvatarUrl()}" : $"https://cdn.discordapp.com/avatars/{Context.User.Id}/{Context.User.AvatarId}.png");
            }
        }
    }
}
