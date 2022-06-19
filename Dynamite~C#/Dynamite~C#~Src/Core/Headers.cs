using Newtonsoft.Json.Linq;

namespace Dynamite {

    partial class InvalidToken : Exception {
        public InvalidToken(string msg) : base(msg) {}
    }
    partial class DynamiteHeaders {
        private protected static HttpClient HeaderClient = new HttpClient();
        public async Task<List<string>> Headers(string token) {
            HeaderClient.DefaultRequestHeaders.Add("Authorization", token);
            string bot = "Invalid"; {
                HttpResponseMessage res = await HeaderClient.GetAsync(
                    "https://discord.com/api/v9/users/@me"
                );
                if (res.StatusCode == System.Net.HttpStatusCode.OK) {
                    bot = "User";
                }
                HeaderClient.DefaultRequestHeaders.Clear();
                HeaderClient.DefaultRequestHeaders.Add("Authorization", $"Bot {token}");
                res = await HeaderClient.GetAsync(
                    "https://discord.com/api/v9/users/@me"
                );
                if(res.StatusCode == System.Net.HttpStatusCode.OK) {
                    bot = "Bot";
                }
            }
            
            if(bot == "User") {
                return new List<string>() {
                    "Authorization", token
                };
            }
            else if(bot == "Bot") {
                return new List<string>() {
                    "Authorization", $"Bot {token}"
                };
            }
            throw new InvalidToken("Invalid Token Parsed");
        }
    }
}