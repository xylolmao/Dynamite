namespace Dynamite {
    partial class Choice {
        private static int Menu() {
            Console.Clear();
            System.Console.WriteLine(@"
            1 ~ Create Hooks
            2 ~ Create & Spam
            3 ~ Spam Existing
            4 ~ Delete Hooks
            5 ~ Rename Hooks
            ");
            Console.Write("> ");
            return Convert.ToInt32(Console.ReadLine());
        }
        
        public static App.Tasks GetChoice() {
            App.Tasks task;
            int choice = Menu();
            switch(choice) {
                case 1:
                    task = App.Tasks.Create;
                    break;
                case 2:
                    task = App.Tasks.CreateThenSpam;
                    break;
                case 3:
                    task = App.Tasks.Spam;
                    break;
                case 4:
                    task = App.Tasks.Delete;
                    break;
                case 5:
                    task = App.Tasks.Rename;
                    break;
                default:
                    task = App.Tasks.Invalid;
                    ConsoleUtils.Write("Invalid Choice Parsed");
                    Thread.Sleep(1500);
                    Menu();
                    break;
            }
            return task;
        }
    }
}