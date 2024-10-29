namespace JonasBot.Dto;

public record GetUpdatesDto(
    bool ok,
    GetUpdatesResultDto[] result
);

public record GetUpdatesResultDto(
    long update_id,
    TelegramMessageDto message
);

public record TelegramMessageDto(
    long message_id,
    MessageFromDto from,
    MessageChatDto chat,
    int date,
    string text
);

public record MessageFromDto(
    long id,
    bool is_bot,
    string first_name,
    string last_name,
    string username,
    string language_code
);

public record MessageChatDto(
    string id,
    string first_name,
    string last_name,
    string username,
    string type
);

