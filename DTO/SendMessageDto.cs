namespace JonasBot.JsonClasses;

public record SendMessageDto(
string chat_id,
string text,
string parse_mode = "Markdown"
    );
