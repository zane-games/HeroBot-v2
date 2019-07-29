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
    [Name("Images")]
    public class ImagesModule : ModuleBase<SocketCommandContext>
    {
        public static Random Random;
        public static string[] CatGreet = new[] { "dream","royal","beautiful","great","funny","good looking" };
        public static string[] CatGreetings = new[] {"Let's find a __great__ cat for you :3","Here is a __great__ cat !", "I've found this cat __great__ for you..."};
        public static string[] DogGreet = new[] { "dream", "royal", "beautiful", "great", "funny", "good looking","loyal" };
        public static string[] DogGreetings = new[] { "Let's find a __great__ dog for you :3", "Here is a __great__ dog !", "I've found this __great__ dog for you..." };

        public ImagesModule(Random random) {
            Random = random;
        }

        [Command("cat"), Alias("meow")]
        public Task RandomCat() {
            using (WebClient webclient = new WebClient()) {
                
                return webclient.DownloadStringTaskAsync(new Uri("http://aws.random.cat/meow")).ContinueWith((x) => {
                    var url = JsonConvert.DeserializeObject<dynamic>(x.Result).file;
                    ReplyAsync($"{Context.User.Mention} {CatGreetings[Random.Next() % CatGreetings.Length].Replace("__great__", CatGreet[Random.Next() % CatGreet.Length])}", false, new EmbedBuilder()
                    {
                        ImageUrl = url
                    }.WithRandomColor().WithCopyrightFooter(Context.User.Username, "cat").Build());
                });

            }
        }

        [Command("dog"), Alias(new[] { "wouf", "woof" })]
        public Task RandomDog() {
            using (WebClient webclient = new WebClient())
            {
                return webclient.DownloadStringTaskAsync(new Uri("https://random.dog/woof")).ContinueWith((x) => {
                    ReplyAsync($"{Context.User.Mention} {DogGreetings[Random.Next() % DogGreetings.Length].Replace("__great__", DogGreet[Random.Next() % DogGreet.Length])}", false, new EmbedBuilder()
                    {
                        ImageUrl = $"https://random.dog/{x.Result}"
                    }.WithRandomColor().WithCopyrightFooter(Context.User.Username, "dog").Build());
                });
            }
        }
        [Command("welcome")]
        public async Task Welcome(string url = null) {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {
                var e = HttpUtility.UrlEncode(Context.User.Username + '#' + Context.User.Discriminator);
                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/welcome?imageurl={rurl}&name={e}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream,"welcome.png","welcome :tada:");
                stream.Close();

            }
        }
        [Command("pixelate")]
        public async Task Pixelate(string url = null)
        {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {
                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/pixelate?imageurl={rurl}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream, "pixelate.png", "welcome :tada:");
                stream.Close();

            }
        }
        [Command("sepia")]
        public async Task Sepia(string url = null)
        {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {
                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/sepia?imageurl={rurl}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream, "sepia.png", "welcome :tada:");
                stream.Close();

            }
        }

        [Command("mirror")]
        public async Task Mirror(string url = null)
        {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {
                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/mirror?imageurl={rurl}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream, "mirror.png", "welcome :tada:");
                stream.Close();

            }
        }

        [Command("invert")]
        public async Task Invert(string url = null)
        {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine(rurl);
                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/invert?imageurl={rurl}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream, "mirror.png", "welcome :tada:");
                //stream.Close();

            }
        }

        [Command("upsidesdown")]
        public async Task Upsidesown(string url = null)
        {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {
                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/upsidesown?imageurl={rurl}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream, "mirror.png", "welcome :tada:");
                stream.Close();

            }
        }

        [Command("blur")]
        public async Task Blur(string url = null)
        {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {
                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/blur?imageurl={rurl}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream, "mirror.png", "welcome :tada:");
                //stream.Close();

            }
        }
        

                    [Command("rgb")]
        public async Task RGB(string url = null)
        {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {
                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/rgb?imageurl={rurl}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream, "mirror.gif", "welcome :tada:");
                stream.Close();
                
            }
        }
        
                                [Command("levelcard")]
        public async Task LevelCard(string url = null)
        {
            var rurl = ResolveUrl(url);
            using (HttpClient client = new HttpClient())
            {

                var bytearray = await client.GetByteArrayAsync($"http://localhost:3000/levelcard?imageurl={rurl}");
                var stream = new MemoryStream(bytearray);
                await Context.Channel.SendFileAsync(stream, "mirror.png", "welcome :tada:");
                stream.Close();

            }
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
            else {
                return HttpUtility.UrlEncode(Context.User.AvatarId == null ? $"{Context.User.GetDefaultAvatarUrl()}" : $"https://cdn.discordapp.com/avatars/{Context.User.Id}/{Context.User.AvatarId}.png");
            }
        }
    }
}
