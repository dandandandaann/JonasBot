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
        secretProvider.TryGet("OpenAIProjectToken", out var openAIToken);

        await Jonas.Wakeup(botToken, openAIToken).Work();

        Console.WriteLine("App closing.");
        Console.ReadKey();
    }
}