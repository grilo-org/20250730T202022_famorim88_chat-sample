using Domain.Entities;

namespace Application.Chat.DTOs;

public class MessageResponse
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public int Sender { get; set; }
    public DateTime Timestamp { get; set; }

    public static MessageResponse From(Message m) => new()
    {
        Id = m.Id,
        Text = m.Text,
        Sender = m.Sender,
        Timestamp = m.Timestamp
    };
}
