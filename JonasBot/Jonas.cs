using System.Text;
using JonasBot.Dto;
using JonasBot.Telegram;

namespace JonasBot;

public class Jonas
{
    private Jonas() { }

    private static Jonas Instance { get; } = new();

    private static TelegramBot bot;

    private static string openAiToken;

    public static Jonas Wakeup(string botToken, string openAiToken)
    {
        bot = new TelegramBot(botToken);

        Jonas.openAiToken = openAiToken;

        return Instance;
    }

    public async Task Work()
    {
        var keepGettingUpdates = true;
        long latestUpdate = 0;
        var lastMinutesToReadMessage = 2;

        do
        {
            var updates = await bot.GetUpdates(latestUpdate);

            // Read and Reply to messages
            foreach (var updateResult in updates.result)
            {
                var message = updateResult.message;

                //TODO: decide which messages to reply to?
                //TODO: Wait for a few messages to then reply?

                if (message.date > DateTimeOffset.Now.AddMinutes(-lastMinutesToReadMessage).ToUnixTimeSeconds())
                {
                    await ReplyToMessage(message);

                    latestUpdate = Math.Max(latestUpdate, updateResult.update_id);
                }

                Thread.Sleep(TimeSpan.FromSeconds(2));
            }

        } while (keepGettingUpdates);


        async Task ReplyToMessage(TelegramMessageDto message)
        {
            var messageReply = new StringBuilder();
            switch (message.text)
            {
                case "/start":
                    // TODO: only start replying to someone after they said /start
                    messageReply.AppendLine("Hi!");
                    messageReply.AppendLine($"You're talking to {bot.Info.Name}.");
                    messageReply.AppendLine($"I am {(bot.Info.IsBot ? "a bot" : "not a bot")}.");
                    messageReply.AppendLine("What is your name?");
                    break;
                case "/exit":
                    messageReply.AppendLine("Ok, I'm shutting down.");
                    keepGettingUpdates = false;
                    break;
                default:
                    messageReply.AppendLine(await ChatgptApi(message.text));
                    // messageReply.AppendLine("Sorry, I'm not sure what to respond to that.");
                    // TODO: send default messages to ChatGPT
                    break;
            }

            if (messageReply.Length > 0)
            {
                await bot.SendMessage(new SendMessageDto(message.chat.id, messageReply.ToString()));
                messageReply.Clear();
            }
        }
    }

    // TODO: move this method to chatGPT class
    public async Task<string> ChatgptApi(string messageText)
    {
        var apiKey = openAiToken;
        var apiUrl = "https://api.openai.com/v1/chat/completions";

        using var client = new HttpClient();
        // Set up headers
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        // Set up request body
        var requestBody = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                // TODO: move initial instructions to secrets?
                new { role = "system", content =
                    @"You are a helpful assistant bot called Jonas. People are talking to you through the messaging app Telegram.
You respond with short and concise answers. You don't know what chatgpt is." },
                new { role = "user", content = messageText }
            },
            temperature = 0.7m
        };

        var content = new StringContent(
            Newtonsoft.Json.JsonConvert.SerializeObject(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        // Send POST request
        var response = await client.PostAsync(apiUrl, content);

        // Read the response
        var responseContent = await response.Content.ReadAsAsync<OAICompletionsDto>();

        Console.WriteLine("User: " + messageText);
        Console.WriteLine("Bot: " + responseContent.choices.First().message.content);

        return responseContent.choices.First().message.content;
    }
}