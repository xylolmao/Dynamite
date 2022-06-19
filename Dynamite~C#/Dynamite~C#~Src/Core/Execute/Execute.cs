using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Dynamite {
    class Start {
        public void CreateHooks() {
            ConsoleUtils.Write("Names -> ");
            string? name = Console.ReadLine();
            foreach(JToken id in Channels.GetJSON()) {
                Thread t = new Thread(() => 
                    Create.CreateWebhooks(id, name) );
                t.Start();
            }
        }
        
        public void CreateThenSpam() {
            ConsoleUtils.Write("Names -> ");
            string? name = Console.ReadLine();
            ConsoleUtils.Write("Content -> ");
            string? content = Console.ReadLine();
            ConsoleUtils.Write("Amount -> ");
            int amount = Convert.ToInt32(Console.ReadLine());
            foreach(JToken id in Channels.GetJSON()) {
                Thread t = new Thread(() => 
                    Create.CreateWebhooks(id, name) );
                t.Start();
            }
            Thread.Sleep(3500);
            foreach(List<JToken> info in Webhooks.GetJSON()) {
                Thread t = new Thread(() => 
                    Spam.SpamMessage(info[0], info[1], amount, content) );
                t.Start();
            }
        }
        
        public void RenameHooks() {
            ConsoleUtils.Write("Rename To -> ");
            string? name = Console.ReadLine();
            foreach(List<JToken> info in Webhooks.GetJSON()) {
                Thread t = new Thread(() => 
                    Rename.RenameHooks(info[0], name) );
                t.Start();
            }
        }
        
        public void SendHooks() {
            ConsoleUtils.Write("Content -> ");
            string? content = Console.ReadLine();
            ConsoleUtils.Write("Amount -> ");
            int amount = Convert.ToInt32(Console.ReadLine());
            foreach(List<JToken> info in Webhooks.GetJSON()) {
                Thread t = new Thread(() => 
                    Spam.SpamMessage(info[0], info[1], amount, content) );
                t.Start();
            }
        }
        
        public void DeleteHooks() {
            foreach(List<JToken> info in Webhooks.GetJSON()) {
                Thread t = new Thread(() =>
                    Delete.DeleteHooks(info[0], info[1]));
                t.Start();
            }
        }
    }
}