using CrmBackend.Utils;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Development.json");
builder.Services.RegisterAllServices(builder.Configuration);


var app = builder.Build();
app.RegisterMiddlewares();
await app.RunAsync();
