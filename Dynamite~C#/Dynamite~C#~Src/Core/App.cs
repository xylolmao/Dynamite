namespace Dynamite {
    static class App {
        public static List<string>? headers;
        public static string? guild;
        static int count;
        public enum Tasks {
            Create = 1,
            Delete,
            Spam,
            Rename,
            CreateThenSpam,
            Invalid
        }
        
        private static async Task StartTasks(Tasks _Task) {
            Start instance = new Start();
            string Tag = User.Tag();
            switch(_Task) {
                case Tasks.Create:
                    ConsoleUtils.Title($"Dynamite | Creating Hooks ~ {Tag}");
                    Thread tCreate = new Thread(() => instance.CreateHooks());
                    tCreate.Start();
                    Thread.Sleep(7000);
                    await Run();
                    break;
                case Tasks.Delete:
                    ConsoleUtils.Title($"Dynamite | Deleting Hooks ~ {Tag}");
                    Thread tDelete = new Thread(() => instance.DeleteHooks());
                    tDelete.Start();
                    Thread.Sleep(3500);
                    await Run();
                    break;
                case Tasks.Spam:
                    ConsoleUtils.Title($"Dynamite | Spamming Hooks ~ {Tag}");
                    Thread tSpam = new Thread(() => instance.SendHooks());
                    tSpam.Start();
                    Thread.Sleep(10000);
                    await Run();
                    break;
                case Tasks.Rename:
                    ConsoleUtils.Title($"Dynamite | Renaming Hooks ~ {Tag}");
                    Thread tRename = new Thread(() => instance.RenameHooks());
                    tRename.Start();
                    Thread.Sleep(5000);
                    await Run();
                    break;
                case Tasks.CreateThenSpam:
                    ConsoleUtils.Title($"Dynamite | Creating & Spamming Hooks ~ {Tag}");
                    Thread Start_ = new Thread(() => instance.CreateThenSpam());
                    Start_.Start();
                    Thread.Sleep(30000);
                    await Run();
                    break;
            }
        }
        public static async Task Run() {
            if(count == 0) {
                ConsoleUtils.Title("Dynamite | Login ~ Token");
                ConsoleUtils.Write("Token -> ");
                DynamiteHeaders headerins = new DynamiteHeaders();
                headers = await headerins.Headers(Console.ReadLine());
                Console.Clear();
                ConsoleUtils.Title("Dynamite | Login ~ Guild");
                ConsoleUtils.Write("Guild -> ");
                guild = Console.ReadLine();
                await Guild.isInGuild(guild);
                count++;
            }
            Console.Clear();
            Tasks task = Choice.GetChoice();
            Thread t = new Thread(() =>
                StartTasks(task)
            );
            t.Start();
        }
    }
}