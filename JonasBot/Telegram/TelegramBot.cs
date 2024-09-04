using JonasBot.Dto;
using JonasBot.Model;

namespace JonasBot.Telegram;

public class TelegramBot
{
    private readonly TelegramApi api;
    public readonly BotInfo Info;

    public TelegramBot(string token)
    {
        api = new TelegramApi(token);
        // TODO: check if there's better way to call async in constructor
        Info = api.GetMe().GetAwaiter().GetResult();
    }

    public async Task<GetUpdatesDto> GetUpdates() => await api.GetUpdates();

    public async Task SendMessage(SendMessageDto sendMessageDto) => await api.SendMessage(sendMessageDto);
}