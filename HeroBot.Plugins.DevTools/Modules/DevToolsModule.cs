using Discord.Commands;
using HeroBot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using V8.Net;

namespace HeroBot.Plugins.DevTools.Modules
{
    [Cooldown(3)]
    [NeedPlugin()]
    public class DevToolsModule : ModuleBase<SocketCommandContext>
    {


        [Command("checkcode"), Alias("codecheck")]
        public async Task CheckCode([Remainder]string code) {
            using (HttpClient c = new HttpClient())
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://jshint:3000"));
                webRequest.Method = "GET";
                code = $"'use strict';\n/* jshint -W097 */\n/* jshint node: true */\n/*jshint esversion: 6 */\n{code}";
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] byte1 = encoding.GetBytes(code);

                // Set the content length of the string being posted.
                webRequest.ContentLength = byte1.Length;
                webRequest.Method = "POST";
                Stream newStream = webRequest.GetRequestStream();

                newStream.Write(byte1, 0, byte1.Length);
                WebResponse g = await webRequest.GetResponseAsync();
                String responseString = "";
                using (Stream stream = g.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                }
                var errors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(responseString);
                var result = "```diff\n+Code analysis : ```\n";
                if (errors.Count == 0) result += "`No errors/warnings found`";
                int i = 0;
                foreach (dynamic err in errors)
                {
                    var enp = "`" + err.code + " position:" + err.line + ":" + err.character + ", " + err.reason + "`\n";
                    if (result.Length + enp.Length >= 1990)
                    {
                        result += "And " + (errors.Count - i) + " others";
                        break;
                    }
                    result += enp;
                    i++;

                }
                await ReplyAsync(result);
            }
        }
        [Command("eval")]

        public async Task ExecuteJs([Remainder]string code) {
            var engine = new V8Engine(false);
            var context = engine.CreateContext();
            engine.DefaultMemberBindingSecurity = ScriptMemberSecurity.NoAcccess;
            var timer = new System.Threading.Timer((state) => { engine.TerminateExecution(); ReplyAsync(":x: Votre code a pris trop de temps a s'éxécuter !").Wait(); }, new { }, 1000, System.Threading.Timeout.Infinite);
            engine.SetContext(context);
            using (var result = engine.Execute(code.Replace("```js",String.Empty).Replace("```",String.Empty),"HeroBotV2-GV8-Executor"))
            {
                if (result.IsNull || result.IsEmpty)
                    await ReplyAsync("Le code n'as retourné aucun résultat");
                else
                    await ReplyAsync("```js\r\n" + result.AsString.Replace("```",String.Empty) + "```");
            }
            timer.Dispose();
            engine.ForceV8GarbageCollection();
            context.Dispose();
            engine.Dispose();

        }
    }
}
