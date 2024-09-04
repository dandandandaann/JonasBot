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

        new Jonas(botToken).Start();

        Console.WriteLine("App closing.");
        Console.ReadKey();
    }
}