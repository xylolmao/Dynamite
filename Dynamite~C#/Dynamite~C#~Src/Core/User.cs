using System.Net.Http.Json;

namespace Dynamite {
    class BluePrintUser {
        public string? username{ get; set; }
        public string? discriminator{ get; set; }
    }
    
    static class User {
        static HttpClient UserClient = new HttpClient();
        static List<string>? headers = App.headers;
        public static string Tag() {
            HttpRequestMessage req = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://discord.com/api/v9/users/@me"
            );
            req.Headers.Add(headers[0], headers[1]);
            HttpResponseMessage res = UserClient.Send(req);
            BluePrintUser bp = res.Content.ReadFromJsonAsync<BluePrintUser>().Result;
            return $"{bp.username}#{bp.discriminator}";
        }
    }
}