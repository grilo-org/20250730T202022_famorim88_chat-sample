using Application.Chat.DTOs;
namespace Application.Common.Interfaces
{

public interface IMessageNotifier
{
    Task NotifyAsync(IEnumerable<MessageResponse> messages);
}

}