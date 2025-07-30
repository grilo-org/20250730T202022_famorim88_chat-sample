using Application.Chat.DTOs;
using Domain.Entites;
using Domain.Entities;
using Infrastructure.Data;
using MediatR;

namespace Application.Chat.CreateMessage
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, List<MessageResponse>>
    {
        private readonly ChatDbContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IBotResponseFactory _botFactory;

        public CreateMessageHandler(ChatDbContext context, IUnitOfWork uow, IBotResponseFactory botFactory)
        {
            _context = context;
            _uow = uow;
            _botFactory = botFactory;
        }

        public async Task<List<MessageResponse>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var userMsg = new Message { Text = request.Text, Sender = (int)SenderType.User };
            _context.Messages.Add(userMsg);

            var botStrategy = _botFactory.Resolve(request.Text);
            var botResponseText = await botStrategy.GetResponse();
            var botMsg = new Message { Text = botResponseText, Sender = (int)SenderType.Bot };
            _context.Messages.Add(botMsg);

            await _uow.CommitAsync();

            return new List<MessageResponse>
            {
                MessageResponse.From(userMsg),
                MessageResponse.From(botMsg)
            };

        }
    }

}
