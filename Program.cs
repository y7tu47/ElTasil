using ElTasilAcademy.Models;
using ElTasilAcademy.servicies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Claims;
using System.Text;


namespace ElTasilAcademy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .MinimumLevel.Debug()
            .CreateLogger();

            builder.Host.UseSerilog();

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
            });

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue; 
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"))
            );

            builder.Services.AddSingleton<MediaManagerService>();
            builder.Services.AddScoped<AudioProcessorsService>();
            builder.Services.AddScoped<Jwt>();
            builder.Services.AddSingleton<OneSignalNotificationService>();
            //builder.Services.AddHostedService<PeriodicNotificationService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy =>
                    {
                        policy.WithOrigins("https://tasil.runasp.net",
                            "http://localhost:4200") // your Angular app URL
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["Auth0:Domain"];
                    
                    options.Audience = builder.Configuration["Auth0:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });

            

            var app = builder.Build();
            app.UseCors("AllowAngularApp");

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //    app.UseSwaggerUI(options =>
            //    {
            //        options.SwaggerEndpoint("/openapi/v1.json", "api");
            //    });
            //}

            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "api");
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();    
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
