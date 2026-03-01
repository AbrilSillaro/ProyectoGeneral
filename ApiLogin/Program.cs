using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiLogin;

namespace ApiLogin;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddCors();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddSingleton<ILoginRepository>(_ => new LoginRepository(connectionString));
        builder.Services.AddSingleton<ILoginService, LoginService>();

        var app = builder.Build();

        // Swagger 
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseCors(policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );

        var loginService = app.Services.GetRequiredService<ILoginService>();

        // Endpoint /login
        app.MapPost("/login", async (Admin admin) =>
        {
            var resultado = await loginService.LoginAsync(admin.Admin_Nom_Usuario, admin.Admin_Contra);

            if (resultado.Exito)
            {
                return Results.Ok(new
                {
                    Exito = true,
                    Mensaje = resultado.Mensaje
                });
            }

            return Results.Json(new
            {
                Exito = false,
                Mensaje = resultado.Mensaje
            }, statusCode: resultado.Codigo);
        });

        // Puerto dinámico de Render
        var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
        app.Run($"http://0.0.0.0:{port}");
    }
}






