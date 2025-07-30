using Application.Chat.DTOs;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Application.Hubs;
public class SignalRMessageNotifier : IMessageNotifier
{
    private readonly IHubContext<ChatHub> _hubContext;

    public SignalRMessageNotifier(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyAsync(IEnumerable<MessageResponse> messages)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", messages);
    }
}
