using Infrastructure.Bot.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bot
{
    public interface IBotResponseFactory
    {
        IBotResponseStrategy GetStrategy(string userInput);
    }

    public class BotResponseFactory : IBotResponseFactory
    {
        private readonly HttpClient _http;
        public BotResponseFactory(HttpClient http) 
        {
        _http = http;
        }

        public IBotResponseStrategy GetStrategy(string userInput)
        {
            if (userInput.Trim().ToLower() == "sair")
                return new ExitResponseStrategy();
            return new RandomResponseStrategy(_http);
        }
    }

}
