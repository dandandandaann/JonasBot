using System.Text;
using JonasBot.JsonClasses;
using JonasBot.Model;
using Newtonsoft.Json;

namespace JonasBot;

public class TelegramApi
{
    private HttpClient client;
    private readonly string token, baseUrl;

    public TelegramApi(string botToken)
    {
        client = new HttpClient();
        token = botToken;
        baseUrl = $"https://api.telegram.org/bot{token}/";
    }

    // TODO: create enum with methods and parse enum name to string when sending it

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