using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bot.Strategies
{
    public class ExitResponseStrategy : IBotResponseStrategy
    {
        public async Task<string> GetResponse() => await Task.FromResult("Encerrando a conversa. Até logo!");
    }

}
