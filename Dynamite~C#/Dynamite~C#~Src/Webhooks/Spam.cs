using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Dynamite {
    partial class Spam {
        static readonly HttpClient SpamClient = new HttpClient();
        
        public static void SpamMessage(JToken id, JToken token, int amount, string content) {
            StringContent json = new StringContent(JsonConvert.SerializeObject(
                        new Dictionary<string, object>() {
                            {"content", content}, {"tts", false}
                        }
                    ), Encoding.UTF8, "application/json"
                );
            for(int i = 0; i < amount; ++i) {
                HttpRequestMessage req = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"https://discord.com/api/webhooks/{Convert.ToString(id)}/{Convert.ToString(token)}"
                ) {
                    Content = json
                };
                HttpResponseMessage res = SpamClient.Send(req);
                switch(res.StatusCode) {
                    case System.Net.HttpStatusCode.OK:
                        System.Console.WriteLine($"[+]Sent {content}");
                        break;
                    case System.Net.HttpStatusCode.TooManyRequests:
                        Dictionary<object, object>? dict = 
                            JsonConvert.DeserializeObject<Dictionary<object, object>>
                                (res.Content.ReadAsStringAsync().Result);
                        object secs = dict["retry_after"];
                        System.Console.WriteLine($"[+]Rate Limited ~ Retrying In {secs}s");
                        break;
                    case System.Net.HttpStatusCode.NoContent:
                        System.Console.WriteLine($"[+]Sent {content}");
                        break;
                    default:
                        System.Console.WriteLine($"[+]Couldn't Send {content}");
                        break;
                }
            }
        }
    }
}