namespace ApiLogin
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<(bool Exito, string Mensaje, int Codigo)> LoginAsync(string Admin_Nom_Usuario, string Admin_Contra)
        {
            
            bool loginCorrecto = await _loginRepository.LoginAsync(Admin_Nom_Usuario, Admin_Contra);

            if (!loginCorrecto)
            {
                return (false, "Usuario o contraseña incorrectos", 401);
            }

            return (true, "Logueo exitoso", 200);
        }
    }
}
