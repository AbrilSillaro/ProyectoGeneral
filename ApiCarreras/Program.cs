using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiCarreras;
using Microsoft.Data.SqlClient;


namespace ApiCarreras
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

        
            builder.Services.AddAuthorization();
            builder.Services.AddCors();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                      ?? throw new InvalidOperationException("Connection string no encontrada");

        
            builder.Services.AddSingleton<ICarreraRepository>(_ =>
                new CarreraRepository(connectionString));

            builder.Services.AddSingleton<ICarreraService, CarreraService>();


            var app = builder.Build();

            
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


            var service = app.Services.GetRequiredService<ICarreraService>();

            app.MapGet("/carreras", async () =>
            {
                try
                {
                    var lista = await service.ListarAsync();
                    return Results.Ok(lista);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { Exito = false, Mensaje = ex.Message });
                }
                catch
                {
                    return Results.StatusCode(503);
                }
            });


            app.MapPost("/carreras", async (Carrera C) =>
            {
             try
             {
               await service.InsertarAsync(C.Car_Nom, C.Turno, C.Car_PlanEstudio);
               return Results.Ok(new { Exito = true, Mensaje = "Carrera ingresada correctamente." });
             } 
             catch (InvalidOperationException ex)
            {
              // errores de negocio 
             return Results.BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
              // UNIQUE constraint
             return Results.BadRequest(new
             {
               Exito = false,
               Mensaje = "Ya existe una carrera con ese nombre y turno."
             });
           }
           });

            app.MapPut("/carreras/{id}", async (int id, Carrera C) =>
            {
                try
                {
                    await service.ModificarAsync(id, C.Car_Nom, C.Turno, C.Car_PlanEstudio);
                    return Results.Ok(new { Exito = true, Mensaje = "Carrera modificada correctamente." });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Exito = false, Mensaje = ex.Message });
                }
            });

            app.MapDelete("/carreras/{id}", async (int id) =>
            {
                try
                {
                    await service.EliminarAsync(id);
                    return Results.Ok(new { Exito = true, Mensaje = "Carrera eliminada correctamente." });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { Exito = false, Mensaje = ex.Message });
                }
            });


            app.Run();
        }
    }
}

