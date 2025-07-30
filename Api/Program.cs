using Application.Chat.CreateMessage;
using Infrastructure.Bot;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Shared.Behaviors;
using Infrastructure.Bot.Strategies;
using Api.Hubs;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", p =>
{
    p.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(_ => true);
}));



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IBotResponseStrategy, RandomResponseStrategy>();
builder.Services.AddScoped<IBotResponseStrategy, GoodbyeStrategy>();


// MediatR + Behaviors
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreateMessageCommand>());

builder.Services.AddValidatorsFromAssemblyContaining<CreateMessageValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// DbContext
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBotResponseFactory, BotResponseFactory>();
builder.Services.AddHttpClient<RandomResponseStrategy>();

var app = builder.Build();
app.UseCors("AllowAll");
app.MapHub<ChatHub>("/hubs/chat");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
