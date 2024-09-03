namespace JonasBot.JsonClasses;

public record GetMeDto(
    bool ok,
    GetMeResultDto result
);

public record GetMeResultDto(
    bool can_connect_to_business,
    bool can_join_groups,
    bool can_read_all_group_messages,
    string first_name,
    bool has_main_web_app,
    long id,
    bool is_bot,
    bool supports_inline_queries,
    string username
);


