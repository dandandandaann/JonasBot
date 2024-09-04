namespace JonasBot.Dto;

public record SendMessageDto(
    string chat_id,
    string text,
    string parse_mode = "Markdown"
);