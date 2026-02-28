const home = document.getElementById("home");
const loginForm = document.getElementById("loginForm");
const registerForm = document.getElementById("registerForm");

function mostrarHome() {
    home.style.display = "block";
    loginForm.style.display = "none";
    registerForm.style.display = "none";
}

function mostrarLogin() {
    home.style.display = "none";
    loginForm.style.display = "block";
}

function mostrarRegistro() {
    home.style.display = "none";
    registerForm.style.display = "block";
}

function mostrarError(msg) {
    alert("⛔ " + msg);
}

function mostrarExito(msg) {
    alert("✅ " + msg);
}

function guardarSesion(usuario) {
    localStorage.setItem("logueado", "true");
    localStorage.setItem("usuario", usuario);
}
