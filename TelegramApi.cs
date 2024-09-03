using System.Net.Http.Formatting;
using System.Text;
using JonasBot.JsonClasses;
using JonasBot.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JonasBot;

public class TelegramApi
{
    private static HttpClient client;

    private TelegramApi() => client = new HttpClient();

    // TODO: Change Telegram Api to allow multiple instances to talk to multiple bots
    public static TelegramApi Instance { get; } = new();

    // TODO: create enum with methods and parse enum name to string when sending it
    private static readonly string
        token = "",
        baseUrl = $"https://api.telegram.org/bot{token}/";

    public async Task<TelegramBot> GetMe()
    {
        var response = await client.GetAsync($"{baseUrl}getMe");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsAsync<GetMeDto>();
        return new TelegramBot(content.result.first_name, content.result.is_bot);
    }

    public async Task<GetUpdatesDto> GetUpdates()
    {
        var response = await client.GetAsync($"{baseUrl}getUpdates");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsAsync<GetUpdatesDto>();
        return content;
    }

    public async Task SendMessage(SendMessageDto sendMessageDto)
    {

        // Serialize the data to JSON
        string jsonString = JsonConvert.SerializeObject(sendMessageDto);
        StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        // var response = await client.PostAsJsonAsync($"{baseUrl}sendMessage", sendMessageDto);
        var response = await client.PostAsync($"{baseUrl}sendMessage", content);
        response.EnsureSuccessStatusCode();
    }
}