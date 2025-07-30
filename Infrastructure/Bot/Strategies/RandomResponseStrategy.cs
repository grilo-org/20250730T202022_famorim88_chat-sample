using System;
using System.Text.Json;

namespace Infrastructure.Bot.Strategies
{
    public class RandomResponseStrategy : IBotResponseStrategy
    {
        private readonly HttpClient _http;
        public RandomResponseStrategy(HttpClient http)
        {
            _http = http;
        }

        private static readonly string[] Responses = {
            "Como posso te ajudar?",
            "request",
            "Tudo certo por aqui!",
            "request",
            "Pode repetir por favor?",
            "request",
            "Estou aqui para ajudar."
    };

        public async Task<string>GetResponse()
        {
            var rnd = new Random();
            var value = Responses[rnd.Next(Responses.Length)];
            if(value != "request")
            return value;
            var response = await _http.GetStringAsync("https://www.boredapi.com/api/activity");
            var obj = JsonSerializer.Deserialize<BoredResponse>(response);
            return $"Que tal: {obj?.Activity}?";
        }
    }
    public class BoredResponse
    {
        public string? Activity { get; set; }
    }

}
