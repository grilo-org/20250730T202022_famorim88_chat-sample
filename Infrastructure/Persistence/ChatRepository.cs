using Domain.Entities;
using Infrastructure.Data;

public class ChatRepository : IChatRepository
{
    private readonly ChatDbContext _context;

    public ChatRepository(ChatDbContext context)
    {
        _context = context;
    }

    public void Add(Message message)
    {
        _context.Messages.Add(message);
    }
}
