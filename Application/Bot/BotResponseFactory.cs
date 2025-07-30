using Infrastructure.Bot;
using Infrastructure.Bot.Strategies;

public class BotResponseFactory : IBotResponseFactory
{
    private readonly IEnumerable<IBotResponseStrategy> _strategies;

    public BotResponseFactory(IEnumerable<IBotResponseStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IBotResponseStrategy Resolve(string userMessage)
    {
        if (userMessage.Trim().ToLower() == "sair")
            return (IBotResponseStrategy)(_strategies.OfType<GoodbyeStrategy>().FirstOrDefault()
                ?? throw new Exception("GoodbyeStrategy not registered"));

        return (IBotResponseStrategy)(_strategies.OfType<RandomResponseStrategy>().FirstOrDefault()
            ?? throw new Exception("RandomResponseStrategy not registered"));
    }

}
