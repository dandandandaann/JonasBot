using System.Text;
using JonasBot.Dto;
using JonasBot.Model;
using Newtonsoft.Json;

namespace JonasBot.Telegram;

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

    public async Task<BotInfo> GetMe()
    {
        var response = await client.GetAsync(GetUrl(ApiMethod.getMe));
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsAsync<GetMeDto>();
        return new BotInfo(content.result.first_name, content.result.is_bot);
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

    internal string GetUrl(ApiMethod method) => $"{baseUrl}{method.ToString()}";

    internal enum ApiMethod
    {
        getMe,
        getUpdates,
        sendMessage
    }
}