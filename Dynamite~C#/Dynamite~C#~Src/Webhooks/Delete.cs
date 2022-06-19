using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Dynamite {
    partial class Delete {
        static readonly List<string>? headers = App.headers;
        static readonly HttpClient DeleteClient = new HttpClient();
        public static void DeleteHooks(JToken id, JToken token) {
            HttpRequestMessage req = new HttpRequestMessage(
                HttpMethod.Delete,
                $"https://discord.com/api/webhooks/{Convert.ToString(id)}/{Convert.ToString(token)}"
            );
            req.Headers.Add(headers[0], headers[1]);
            HttpResponseMessage res = DeleteClient.Send(req);
            switch(res.StatusCode) {
                case System.Net.HttpStatusCode.OK:
                    System.Console.WriteLine($"[+]Deleted {id}");
                    break;
                case System.Net.HttpStatusCode.TooManyRequests:
                    Dictionary<object, object>? dict = 
                        JsonConvert.DeserializeObject<Dictionary<object, object>>
                            (res.Content.ReadAsStringAsync().Result);
                    object secs = dict["retry_after"];
                    System.Console.WriteLine($"[+]Rate Limited ~ Retrying In {secs}s");
                    break;
                case System.Net.HttpStatusCode.NoContent:
                    System.Console.WriteLine($"[+]Deleted {id}");
                    break;
                default:
                    System.Console.WriteLine($"[+]Couldn't Delete {id}");
                    break;
            }
        }
    }
}