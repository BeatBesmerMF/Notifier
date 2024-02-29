using Notifyer.Events;
using Notifyer.State;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<EventStore>();
builder.Services.AddSingleton<StatisticsState>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var statisticsState = app.Services.GetRequiredService<StatisticsState>();

app.Services.GetRequiredService<EventStore>().eventHandler += (sender, e) =>
{
    statisticsState.Project(e.Event);
};

app.Run();
