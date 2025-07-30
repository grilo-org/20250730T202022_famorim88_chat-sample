using Application.Chat.DTOs;
using Domain.Entites;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chat.CreateMessage
{
    public record CreateMessageCommand(string Text) : IRequest<List<MessageResponse>>;

}
