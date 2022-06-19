using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Dynamite {
    partial class Rename {
        static readonly List<string>? headers = App.headers;
        static readonly HttpClient RenameClient = new HttpClient();
        
        public static void RenameHooks(JToken id, string name) {
            StringContent json = new StringContent(
                JsonConvert.SerializeObject(
                    new Dictionary<string, string>() {
                        {"name", name}
                    }
                ), Encoding.UTF8, "application/json"
            );
            HttpRequestMessage req = new HttpRequestMessage(
                HttpMethod.Patch,
                $"https://discord.com/api/v9/webhooks/{Convert.ToString(id)}"
            ) {
                Content = json
            };
            req.Headers.Add(headers[0], headers[1]);
            HttpResponseMessage res = RenameClient.Send(req);
            switch(res.StatusCode) {
                case System.Net.HttpStatusCode.OK:
                    System.Console.WriteLine($"[+]Renamed {id} To {name}");
                    break;
                case System.Net.HttpStatusCode.TooManyRequests:
                    Dictionary<object, object>? dict = 
                        JsonConvert.DeserializeObject<Dictionary<object, object>>
                            (res.Content.ReadAsStringAsync().Result);
                    object secs = dict["retry_after"];
                    System.Console.WriteLine($"[+]Rate Limited ~ Retrying In {secs}s");
                    break;
                case System.Net.HttpStatusCode.NoContent:
                    System.Console.WriteLine($"[+]Renamed {id} To {name}");
                    break;
                default:
                    System.Console.WriteLine($"[+]Couldn't Rename {id} To {name}");
                    break;
            }
        }
    }
}