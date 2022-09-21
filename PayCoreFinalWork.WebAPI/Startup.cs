using Hangfire;
using Hangfire.PostgreSql;
using PayCoreFinalWork.Core.WebAPI.Concrete.Extensions;
using PayCoreFinalWork.Core.WebAPI.Concrete.Jwt;

namespace PayCoreFinalWork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static JwtConfig JwtConfig { get; private set; }

        // Yazılan tüm extension metodlar diğer gerekli hazır servislerin yanında konfigüre edilir.
        public void ConfigureServices(IServiceCollection services)
        {
            string jwtSection = "JwtConfig", postgreSqlSection = "PostgreSqlConnection";
            JwtConfig = Configuration.GetSection(jwtSection).Get<JwtConfig>();

            services.AddEndpointsApiExplorer()
                    .AddSwaggerGen()
                    .AddNHibernatePosgreSql(Configuration.GetConnectionString(postgreSqlSection))
                    .AddRegisteries()
                    .ConfigureAutoMapper()
                    .Configure<JwtConfig>(Configuration.GetSection(jwtSection))
                    .AddJwtBearerAuthentication()
                    .AddCustomizeSwagger()
                    .AddHangfire(configuration => configuration
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UsePostgreSqlStorage(Configuration.GetConnectionString(postgreSqlSection), new PostgreSqlStorageOptions
                        {
                            TransactionSynchronisationTimeout = TimeSpan.FromMinutes(5),
                            InvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.FromMinutes(5),
                        }))
                    .AddHangfireServer();

            services.AddControllers()
                    .ConfigureCustomDateTime()
                    .ConfigureFluentValidation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger()
                   .UseSwaggerUI();
            }

            app.UseHttpsRedirection()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseHangfireDashboard()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}