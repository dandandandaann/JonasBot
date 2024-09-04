using JonasBot.Telegram;
using FluentAssertions;

namespace UnitTest;

public class TelegramBot_UnitTest
{
    private TelegramApi api;
    private readonly string token = "token";

    [SetUp]
    public void Setup()
    {
        // Arrange
        // var mockApi = new Mock<TelegramApi>();
        // mockApi.Setup(s => s.GetMe()).ReturnsAsync(new BotInfo("Name", true));
        // api = mockApi.Object;

        api = new TelegramApi(token);
    }

    [Test]
    public void GetUrl_Test()
    {
        api.GetUrl(TelegramApi.ApiMethod.getMe).Should().Be($"https://api.telegram.org/bot{token}/getMe");
        api.GetUrl(TelegramApi.ApiMethod.getUpdates).Should().Be($"https://api.telegram.org/bot{token}/getUpdates");
        api.GetUrl(TelegramApi.ApiMethod.sendMessage).Should().Be($"https://api.telegram.org/bot{token}/sendMessage");
    }
}