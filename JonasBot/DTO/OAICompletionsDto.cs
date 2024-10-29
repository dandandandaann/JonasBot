namespace JonasBot.Dto;

public class OAICompletionsDto
{
    public string id { get; set; }
    public string @object { get; set; }
    public int created { get; set; }
    public string model { get; set; }
    public OAIChoicesDto[] choices { get; set; }
    public OAIUsageDto usage { get; set; }
    public string system_fingerprint { get; set; }
}

public class OAIChoicesDto
{
    public int index { get; set; }
    public OAIMessageDto message { get; set; }
    public object logprobs { get; set; }
    public string finish_reason { get; set; }
}

public class OAIMessageDto
{
    public string role { get; set; }
    public string content { get; set; }
    public object refusal { get; set; }
}

public class OAIUsageDto
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}