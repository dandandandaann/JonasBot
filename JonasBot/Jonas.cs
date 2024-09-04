using System.Text;
using JonasBot.Dto;
using JonasBot.Telegram;

namespace JonasBot;

public class Jonas
{
    private Jonas() { }

    private static Jonas Instance { get; } = new();

    private static TelegramBot bot;

    public static Jonas Wakeup(string token)
    {
        bot = new TelegramBot(token);

        return Instance;
    }

    public async Task Work()
    {
        var keepGettingUpdates = true;

        do
        {
            var updates = await bot.GetUpdates();

            // Read and Reply to messages
            foreach (var message in updates.result)
            {
                // TODO: decide which messages to reply to

                await ReplyToMessage(message);


                Thread.Sleep(TimeSpan.FromSeconds(2));
            }

        } while (keepGettingUpdates);

        async Task ReplyToMessage(GetUpdatesResultDto message)
        {
            var messageReply = new StringBuilder();
            switch (message.message.text)
            {
                case "/start":
                    messageReply.AppendLine("Hi!");
                    messageReply.AppendLine($"You're talking to {bot.Info.Name}.");
                    messageReply.AppendLine($"I am {(bot.Info.IsBot ? "a bot" : "not a bot")}.");
                    break;
                case "/exit":
                    messageReply.AppendLine("Ok, app is closing now.");
                    keepGettingUpdates = false;
                    break;
                default:
                    break;
            }

            if (messageReply.Length > 0)
            {
                await bot.SendMessage(new SendMessageDto(message.message.chat.id, messageReply.ToString()));
                messageReply.Clear();
            }
        }
    }
    
}