using System;
namespace Infrastructure.Bot
{
    public interface IBotResponseStrategy
    {
        Task<string> GetResponse();
    }

}
