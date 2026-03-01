const API_URL = "https://proyectogeneral.onrender.com/login";

async function login(usuario, contraseña) {
    const response = await axios.post(API_URL, {
        Admin_Nom_Usuario: usuario,
        Admin_Contra: contraseña
    });

    return response.data;
}
