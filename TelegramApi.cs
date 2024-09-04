using System.Text;
using JonasBot.JsonClasses;
using JonasBot.Model;
using Newtonsoft.Json;

namespace JonasBot;

// TODO: use TelegramBot instead of this and leave only Api methods here
// TODO: execute GetMe in constructor and save data in the class for lookup

public class TelegramApi
{
    private readonly HttpClient client;
    private readonly string token, baseUrl;

    public TelegramApi(string botToken)
    {
        client = new HttpClient();
        token = botToken;
        baseUrl = $"https://api.telegram.org/bot{token}/";
    }

    public async Task<TelegramBot> GetMe()
    {
        var response = await client.GetAsync(GetUrl(ApiMethod.getMe));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsAsync<GetMeDto>();
        return new TelegramBot(content.result.first_name, content.result.is_bot);
    }

    public async Task<GetUpdatesDto> GetUpdates()
    {
        var response = await client.GetAsync(GetUrl(ApiMethod.getUpdates));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsAsync<GetUpdatesDto>();
        return content;
    }

    public async Task SendMessage(SendMessageDto sendMessageDto)
    {
        string jsonString = JsonConvert.SerializeObject(sendMessageDto);
        StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(GetUrl(ApiMethod.sendMessage), content);
        response.EnsureSuccessStatusCode();
    }

    private string GetUrl(ApiMethod method) => $"{baseUrl}{method.ToString()}";

    enum ApiMethod
    {
        getMe,
        getUpdates,
        sendMessage
    }
}