namespace ApiLogin
{
    public interface ILoginService
    {
        Task<(bool Exito, string Mensaje, int Codigo)> LoginAsync(
            string Admin_Nom_Usuario,
            string Admin_Contra
        );
    }
}
