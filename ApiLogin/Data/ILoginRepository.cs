namespace ApiLogin
{
    public interface ILoginRepository
    {
        Task<bool> LoginAsync(string Admin_Nom_Usuario, string Admin_Contra);
    }
}