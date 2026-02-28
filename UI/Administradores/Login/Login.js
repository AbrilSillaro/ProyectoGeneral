// Botones de navegación
document.getElementById("btnLogin").onclick = mostrarLogin;
document.getElementById("btnRegister").onclick = mostrarRegistro;
document.getElementById("cancelLogin").onclick = mostrarHome;
document.getElementById("cancelRegister").onclick = mostrarHome;

// Submit login
loginForm.addEventListener("submit", async e => {
    e.preventDefault();

    const usuario = document.getElementById("loginUser").value.trim();
    const contraseña = document.getElementById("loginPassword").value.trim();

    if (!usuario || !contraseña) {
        return mostrarError("Complete todos los campos.");
    }

    try {
        const data = await login(usuario, contraseña);

        if (data.exito || data.Exito) {
            mostrarExito("Inicio de sesión exitoso.");

            guardarSesion(usuario);
            window.location.href = "menu.html";
        } else {
            mostrarError(data.mensaje || data.Mensaje);
        }

    } catch (error) {
        console.error(error);

        if (error.response?.data?.mensaje) {
            mostrarError(error.response.data.mensaje);
        } else {
            mostrarError("Error de conexión con la API.");
        }
    }
});

// Submit registro (no implementado)
registerForm.addEventListener("submit", e => {
    e.preventDefault();
    mostrarError("La API no tiene endpoint de registro.");
});
