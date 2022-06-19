namespace Dynamite {
    class InvalidGuild : Exception {
        public InvalidGuild(string msg) : base(msg) {}
    }
    static class Guild {
        public static async Task isInGuild(string guild) {
            List<string>? headers = App.headers;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add(headers[0], headers[1]);
            HttpResponseMessage req = await client.GetAsync(
                $"https://discord.com/api/v9/guilds/{guild}"
            );
            if(req.StatusCode != System.Net.HttpStatusCode.OK) {
                throw new InvalidGuild("Invalid Guild Parsed");
            }
        }
    }
}