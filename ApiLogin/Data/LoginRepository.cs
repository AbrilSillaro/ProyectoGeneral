using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ApiLogin
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString; //Guarda el connection string para conectarse a la base 

        public LoginRepository(string connectionString)
        {
            _connectionString = connectionString; // constructor con inyeccion de dependencia 
        }

        public async Task<bool> LoginAsync(string Admin_Nom_Usuario, string Admin_Contra) // devuelve si el login fue exitoso 
        {
            using var connection = new SqlConnection(_connectionString); // creacion de la conexion a la base 

            var parametros = new DynamicParameters();
            parametros.Add("@Admin_Nom_Usuario", Admin_Nom_Usuario);
            parametros.Add("@Admin_Contra", Admin_Contra);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 100, direction: ParameterDirection.Output); //parametro de salida

            await connection.ExecuteAsync("Logueo_Admin", parametros, commandType: CommandType.StoredProcedure); // uso de dapper

            string mensaje = parametros.Get<string>("@Mensaje"); // obtiene el parametro de salida 

            return mensaje == "Logueo exitoso.";
        }
    }
}

