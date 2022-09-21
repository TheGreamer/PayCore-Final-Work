using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog konfig�rasyonu yap�l�r.
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger());

// T�m konfig�rasyonlar ve servis konfig�rasyonlar� Startup ile beraber konfig�re edilir ve uygulama �al��t�r�l�r.
var startup = new PayCoreFinalWork.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app, builder.Environment);
app.Run();