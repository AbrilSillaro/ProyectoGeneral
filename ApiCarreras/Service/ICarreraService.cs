namespace ApiCarreras
{
    public interface ICarreraService
    {
        Task<IEnumerable<Carrera>> ListarAsync();
        Task<bool> InsertarAsync(string Car_Nom, string Turno, string Car_PlanEstudio);
        Task<bool> ModificarAsync(int ID_Car, string Car_Nom, string Turno, string Car_PlanEstudio);
        Task<bool> EliminarAsync(int ID_Car);
    }
}
