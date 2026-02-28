
namespace ApiCarreras
{
    public interface ICarreraRepository
    {
        Task<IEnumerable<Carrera>> ListarAsync();
        Task<Carrera?> BuscarPorIdAsync(int id);
        Task<bool> InsertarAsync(string Car_Nom, string Turno, string Car_PlanEstudio);
        Task<bool> ModificarAsync(int ID_Car, string Car_Nom, string Turno, string Car_PlanEstudio);
        Task<bool> EliminarAsync(int id);
    }
}
