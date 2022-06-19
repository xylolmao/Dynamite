using Newtonsoft.Json.Linq;

namespace Dynamite {
    partial class Channels {
        static readonly List<string>? headers = App.headers;
        static readonly string? guild = App.guild;
        static readonly HttpClient ChannelClient = new HttpClient();
        public static IEnumerable<JToken> GetJSON() {
            HttpRequestMessage req = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://discord.com/api/v9/guilds/{guild}/channels"
            );
            req.Headers.Add(headers[0], headers[1]);
            HttpResponseMessage res = ChannelClient.Send(req);
            string resString = res.Content.ReadAsStringAsync().Result;
            JArray JSONArray = JArray.Parse(resString);
            for(int i = 0; i < JSONArray.Count; ++i) {
                yield return JSONArray[i]["id"];
            }
        }
    }
    
    partial class Webhooks {
        static readonly List<string>? headers = App.headers;
        static readonly string? guild = App.guild;
        static readonly HttpClient WebhookClient = new HttpClient();
        public static IEnumerable<List<JToken>> GetJSON() {
            HttpRequestMessage req = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://discord.com/api/v9/guilds/{guild}/webhooks"
            );
            req.Headers.Add(headers[0], headers[1]);
            HttpResponseMessage res = WebhookClient.Send(req);
            string resString = res.Content.ReadAsStringAsync().Result;
            JArray JSONArray = JArray.Parse(resString);
            for(int i = 0; i < JSONArray.Count(); ++i) {
                yield return new List<JToken>() {
                    JSONArray[i]["id"], JSONArray[i]["token"]
                };
            }
            if(JSONArray.Count() == 0) {
                System.Console.WriteLine("No Webhooks In Guild");
                Choice.GetChoice();
            }
        }
    }
}