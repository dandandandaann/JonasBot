using System.Text;
using JonasBot.Dto;
using JonasBot.Telegram;
using Microsoft.Extensions.Configuration;

namespace JonasBot;

class Program
{
    static readonly HttpClient client = new();

    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        var secretProvider = config.Providers.First();
        secretProvider.TryGet("BotToken", out var botToken);

        var bot = new TelegramBot(botToken);

        var continueGettingUpdates = true;
        do
        {
            var updates = await bot.GetUpdates();
            var messageReply = new StringBuilder();

            // Read and Reply to messages
            foreach (var message in updates.result)
            {
                switch (message.message.text)
                {
                    case "/start":
                        messageReply.AppendLine("Hi!");
                        messageReply.AppendLine($"You're talking to {bot.Info.Name}.");
                        messageReply.AppendLine($"I am {(bot.Info.IsBot ? "a bot" : "not a bot")}.");
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
                    await bot.SendMessage(new SendMessageDto(message.message.chat.id, messageReply.ToString()));
                    messageReply.Clear();
                }

                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        } while (continueGettingUpdates);

        Console.WriteLine("App closing.");
        Console.ReadKey();
    }
}