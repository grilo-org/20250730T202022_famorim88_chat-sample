namespace Domain.Entities
{

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public int Sender { get; set; } = 0;

    public static Message CreateUserMessage(string text)
    {
        return new Message
        {
            Id = Guid.NewGuid(),
            Text = text,
            Timestamp = DateTime.UtcNow,
            Sender = 0
        };
    }

    public static Message CreateBotMessage(string text)
    {
        return new Message
        {
            Id = Guid.NewGuid(),
            Text = text,
            Timestamp = DateTime.UtcNow,
            Sender = 1
        };
    }
}


}