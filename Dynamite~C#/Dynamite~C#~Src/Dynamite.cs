namespace Dynamite {
    partial class Run {
        private static async Task Main(string[] args) {
            try {
                await App.Run();
            }  catch(InvalidToken e) {
                ConsoleUtils.Write(e.Message);
                Thread.Sleep(1500);
                return;
            } catch (InvalidGuild g) {
                ConsoleUtils.Write(g.Message);
                Thread.Sleep(1500);
                return;
            }
        }
    }
}