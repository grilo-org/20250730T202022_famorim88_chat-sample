using Application.Chat.DTOs;
using Application.Hubs;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.SignalR;
public record CreateMessageCommand(string Text) : IRequest<List<MessageResponse>>;
public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, List<MessageResponse>>
{
    private readonly IChatRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IBotResponseFactory _botFactory;
    private readonly IHubContext<ChatHub> _hubContext;

    public CreateMessageHandler(
        IChatRepository repo,
        IUnitOfWork uow,
        IBotResponseFactory botFactory,
        IHubContext<ChatHub> hubContext)
    {
        _repo = repo;
        _uow = uow;
        _botFactory = botFactory;
        _hubContext = hubContext;
    }

    public async Task<List<MessageResponse>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var userMsg = Message.CreateUserMessage(request.Text);
        var botMsg = Message.CreateBotMessage(await _botFactory.Resolve(request.Text).GetResponse());

        _repo.Add(userMsg);
        _repo.Add(botMsg);
        await _uow.CommitAsync();

        // 🔔 Notificar clientes conectados via Observer (SignalR)
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", new[] {
            new { sender = userMsg.Sender, text = userMsg.Text },
            new { sender = botMsg.Sender, text = botMsg.Text }
        });

        return new List<MessageResponse> {
            MessageResponse.From(userMsg),
            MessageResponse.From(botMsg)
        };
    }
}
