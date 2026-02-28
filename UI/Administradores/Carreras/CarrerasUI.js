function ocultarTodos(panels) {
    panels.forEach(p => p.style.display = "none");
}

function mostrarError(err) {
    console.error(err);
    let msg = "Ocurrió un error";
    if (err.response?.data) {
        msg = err.response.data.mensaje || err.response.data;
    }
    alert("❌ " + msg);
}

function mostrarExito(msg) {
    alert("✅ " + msg);
}

function obtenerSeleccionado(container) {
    const radio = container.querySelector('input[type="radio"]:checked');
    return radio ? parseInt(radio.value, 10) : null;
}

function renderLista(container, lista) {
    container.innerHTML = "";
    lista.forEach(c => {
        container.insertAdjacentHTML("beforeend", `
            <div class="list-item">
                <input type="radio" name="${container.id}" value="${c.iD_Car}" id="${container.id}-${c.iD_Car}">
                <label for="${container.id}-${c.iD_Car}">
                    ${c.car_Nom} — ${c.turno} — ${c.car_PlanEstudio}
                </label>
            </div>
        `);
    });
}

function renderTabla(container, lista) {
    container.innerHTML = `
        <table class="table-carreras">
            <thead>
                <tr><th>ID</th><th>Nombre</th><th>Turno</th><th>Plan</th></tr>
            </thead>
            <tbody>
                ${lista.map(c => `
                    <tr>
                        <td>${c.iD_Car}</td>
                        <td>${c.car_Nom}</td>
                        <td>${c.turno}</td>
                        <td>${c.car_PlanEstudio}</td>
                    </tr>
                `).join("")}
            </tbody>
        </table>
    `;
}
