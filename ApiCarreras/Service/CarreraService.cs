namespace ApiCarreras
{
    public class CarreraService : ICarreraService
    {
        private readonly ICarreraRepository _repo;

        public CarreraService(ICarreraRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Carrera>> ListarAsync()
        {
            var lista = await _repo.ListarAsync();

            if (lista == null || !lista.Any())
                throw new InvalidOperationException("No existen carreras registradas.");

            return lista;
        }

            public async Task<bool> InsertarAsync(string Car_Nom, string Turno, string Car_PlanEstudio)
        {
            if (string.IsNullOrWhiteSpace(Car_Nom) ||
                string.IsNullOrWhiteSpace(Turno) ||
                string.IsNullOrWhiteSpace(Car_PlanEstudio))
            {
                throw new ArgumentException("Todos los campos son obligatorios.");
            }

            var existentes = await _repo.ListarAsync();
            if (existentes.Any(c =>
                c.Car_Nom.ToLower() == Car_Nom.ToLower() &&
                c.Turno.ToLower() == Turno.ToLower()))
            {
                throw new InvalidOperationException("Ya existe una carrera con ese nombre y turno.");
            }

            return await _repo.InsertarAsync(Car_Nom, Turno, Car_PlanEstudio);
        }

        public async Task<bool> ModificarAsync(int ID_Car, string Car_Nom, string Turno, string Car_PlanEstudio)
        {
            if (string.IsNullOrWhiteSpace(Car_Nom) ||
                string.IsNullOrWhiteSpace(Turno) ||
                string.IsNullOrWhiteSpace(Car_PlanEstudio))
            {
                throw new ArgumentException("Todos los campos son obligatorios.");
            }

            var existentes = (await _repo.ListarAsync()).ToList();

            if (!existentes.Any())
                throw new InvalidOperationException("No existen carreras registradas.");

            if (ID_Car <= 0)
                throw new ArgumentException("Debe seleccionar una carrera para modificar.");

            if (existentes.Any(c =>
                c.ID_Car != ID_Car &&
                c.Car_Nom.ToLower() == Car_Nom.ToLower() &&
                c.Turno.ToLower() == Turno.ToLower()))
            {
                throw new InvalidOperationException("Ya existe otra carrera con ese nombre y turno.");
            }

            return await _repo.ModificarAsync(ID_Car, Car_Nom, Turno, Car_PlanEstudio);
        }

        public async Task<bool> EliminarAsync(int ID_Car)
        {
            var lista = await _repo.ListarAsync();

            if (lista == null || !lista.Any())
                throw new InvalidOperationException("No existen carreras registradas.");

            if (ID_Car <= 0)
                throw new ArgumentException("Debe seleccionar una carrera para eliminar.");

            return await _repo.EliminarAsync(ID_Car);
        }
    }
}
