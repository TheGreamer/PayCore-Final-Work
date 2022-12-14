using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog konfigürasyonu yapılır.
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger());

// Tüm konfigürasyonlar ve servis konfigürasyonları Startup ile beraber konfigüre edilir ve uygulama çalıştırılır.
var startup = new PayCoreFinalWork.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.Configure(app, builder.Environment);
app.Run();