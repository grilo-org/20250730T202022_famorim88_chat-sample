using FluentAssertions;
using Infrastructure.Bot.Strategies;

namespace ChatBotApp.Tests;

public class RandomResponseStrategyTests
{
    [Fact]
    public async Task GetResponse_ShouldReturnLocalResponse_WhenNotRequest()
    {
        var httpClient = new HttpClient();
        var strategy = new RandomResponseStrategy(httpClient);

        var result = await strategy.GetResponse();

        result.Should().NotBeNullOrWhiteSpace();
    }
}