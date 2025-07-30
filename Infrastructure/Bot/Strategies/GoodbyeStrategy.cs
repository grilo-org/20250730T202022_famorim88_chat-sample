using Infrastructure.Bot;
public class GoodbyeStrategy : IBotResponseStrategy
{
    public Task<string> GetResponse()
    {
        return Task.FromResult("Até logo! A conversa foi encerrada.");
    }
}