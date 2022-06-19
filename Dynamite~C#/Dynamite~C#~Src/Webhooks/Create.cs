using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Dynamite {
    internal partial class Create {
        static readonly List<string>? headers = App.headers;
        static readonly HttpClient CreateClient = new HttpClient();
        public static void CreateWebhooks(JToken id, string name) { 
            StringContent json = new StringContent(
                JsonConvert.SerializeObject(
                        new Dictionary<string, string>() {
                            {"name", name}
                        }
                    ), Encoding.UTF8, "application/json"
                );
            HttpRequestMessage req = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://discord.com/api/v9/channels/{Convert.ToString(id)}/webhooks"
            ) {
                Content = json
            };
            req.Headers.Add(headers[0], headers[1]);
            HttpResponseMessage res = CreateClient.Send(req);
            switch(res.StatusCode) {
                case System.Net.HttpStatusCode.OK:
                    System.Console.WriteLine($"[+]Created {name}");
                    break;
                case System.Net.HttpStatusCode.TooManyRequests:
                    Dictionary<object, object>? dict = 
                        JsonConvert.DeserializeObject<Dictionary<object, object>>
                            (res.Content.ReadAsStringAsync().Result);
                    object secs = dict["retry_after"];
                    System.Console.WriteLine($"[+]Rate Limited ~ Retrying In {secs}s");
                    break;
                default:
                    System.Console.WriteLine($"[+]Couldn't Create {name}");
                    break;
            }
        }
    }
}