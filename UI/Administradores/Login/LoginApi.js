const API_URL = "http://localhost:5131/login";

async function login(usuario, contraseña) {
    const response = await axios.post(API_URL, {
        Admin_Nom_Usuario: usuario,
        Admin_Contra: contraseña
    });

    return response.data;
}
