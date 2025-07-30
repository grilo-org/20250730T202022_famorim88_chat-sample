using Infrastructure.Bot;

public interface IBotResponseFactory
{
    IBotResponseStrategy Resolve(string userMessage);
}
