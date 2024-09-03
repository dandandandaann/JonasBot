using System.Text;
using JonasBot.JsonClasses;

namespace JonasBot;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        var telegramMe = await TelegramApi.Instance.GetMe();

        var continueGettingUpdates = true;
        do
        {
            var updates = await TelegramApi.Instance.GetUpdates();
            var messageReply = new StringBuilder();

            // Read and Reply to messages
            foreach (var message in updates.result)
            {
                switch (message.message.text)
                {
                    case "/start":
                        messageReply.AppendLine("Hi!");
                        messageReply.AppendLine($"You're talking to {telegramMe.Name}.");
                        messageReply.AppendLine($"I am {(telegramMe.IsBot ? "a bot" : "not a bot")}.");
                        break;
                    case "/exit":
                        messageReply.AppendLine("Ok, app is closing now.");
                        continueGettingUpdates = false;
                        break;
                    default:
                        break;
                }

                if (messageReply.Length > 0)
                {
                    await TelegramApi.Instance.SendMessage(new SendMessageDto(message.message.chat.id, messageReply.ToString()));
                    messageReply.Clear();
                }

                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        } while (continueGettingUpdates);

        Console.WriteLine("App closing.");
        Console.ReadKey();
    }
}