namespace Dynamite {
    static class ConsoleUtils {
        public static void Write(string msg) {
            foreach(char c in msg) {
                System.Console.Write(c);
                Thread.Sleep(5);
            }
        }
        
        public static void Title(string title) {
            Console.Title = title;
        }
    }
}