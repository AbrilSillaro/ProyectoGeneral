using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ApiCarreras
{
    public class CarreraRepository : ICarreraRepository
    {
        private readonly string _connectionString;

        public CarreraRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Carrera>> ListarAsync()
        {
            using var db = Connection;
            return await db.QueryAsync<Carrera>(
                "sp_ListarCarreras",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Carrera?> BuscarPorIdAsync(int id)
        {
            using var db = Connection;

            return await db.QueryFirstOrDefaultAsync<Carrera>(
                "sp_BuscarCarreraPorId",
                new { ID_Car = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> InsertarAsync(string Car_Nom, string Turno, string Car_PlanEstudio)
        {
            using var db = Connection;

            var p = new DynamicParameters();
            p.Add("@Nombre", Car_Nom);
            p.Add("@Turno", Turno);
            p.Add("@PlanEstudio", Car_PlanEstudio);

            await db.ExecuteAsync("sp_InsertarCarrera", p, commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<bool> ModificarAsync(int ID_Car, string Car_Nom, string Turno, string Car_PlanEstudio)
        {
          using var db = Connection;

          var p = new DynamicParameters();
          p.Add("@ID_Car", ID_Car);
          p.Add("@Nombre", Car_Nom);
          p.Add("@Turno", Turno);
          p.Add("@PlanEstudio", Car_PlanEstudio);

          await db.ExecuteAsync("sp_ModificarCarrera", p, commandType: CommandType.StoredProcedure);
          return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var db = Connection;

            await db.ExecuteAsync(
                "sp_EliminarCarrera",
                new { ID_Car = id },
                commandType: CommandType.StoredProcedure
            );

            return true;
        }
    }
}
