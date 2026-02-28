// ---------- SESIÓN ----------
if (localStorage.getItem("logueado") !== "true") {
    alert("⛔ Debe iniciar sesión.");
    window.location.href = "admin.html";
}

// ---------- DOM ----------
const $ = id => document.getElementById(id);

const panels = [
    $("panelInsertar"),
    $("panelModificar"),
    $("panelEliminar"),
    $("panelListar"),
    $("modFormContainer"),
    $("delConfirmContainer")
];

const modListContainer = $("modListContainer");
const delListContainer = $("delListContainer");
const tablaContainer   = $("tablaContainer");

// ---------- MENÚ ----------
$("btnInsertar").onclick = () => {
    ocultarTodos(panels);
    $("panelInsertar").style.display = "block";
};

$("btnModificar").onclick = async () => {
    ocultarTodos(panels);
    $("panelModificar").style.display = "block";
    cargarLista(modListContainer);
};

$("btnEliminar").onclick = async () => {
    ocultarTodos(panels);
    $("panelEliminar").style.display = "block";
    cargarLista(delListContainer);
};

$("btnListar").onclick = async () => {
    ocultarTodos(panels);
    $("panelListar").style.display = "block";
    cargarTabla();
};

// ---------- INSERTAR ----------
$("formInsertar").onsubmit = async e => {
    e.preventDefault();

    try {
        await crearCarrera({
            car_Nom: $("insNombre").value,
            turno: $("insTurno").value,
            car_PlanEstudio: $("insPlan").value
        });

        mostrarExito("Carrera creada");
        e.target.reset();
        ocultarTodos(panels);
    } catch (err) {
        mostrarError(err);
    }
};

$("btnCancelarInsert").onclick = () => ocultarTodos(panels);

// ---------- MODIFICAR ----------
$("btnAceptarModificar").onclick = async () => {
    const id = obtenerSeleccionado(modListContainer);
    if (!id) return alert("Seleccione una carrera");

    const lista = await getCarreras();
    const carrera = lista.find(c => c.iD_Car === Number(id));
    $("modId").value     = carrera.iD_Car;
    $("modNombre").value = carrera.car_Nom;
    $("modTurno").value  = carrera.turno;
    $("modPlan").value   = carrera.car_PlanEstudio;

    $("modFormContainer").style.display = "block";
};

$("formModificar").onsubmit = async e => {
    e.preventDefault();

    try {
        await actualizarCarrera($("modId").value, {
            car_Nom: $("modNombre").value,
            turno: $("modTurno").value,
            car_PlanEstudio: $("modPlan").value
        });

        mostrarExito("Carrera modificada");
        ocultarTodos(panels);
    } catch (err) {
        mostrarError(err);
    }
};

$("btnCancelarModificar").onclick =
$("btnCancelarEdicion").onclick = () => ocultarTodos(panels);

// ---------- ELIMINAR ----------
$("btnAceptarEliminar").onclick = async () => {
    const id = obtenerSeleccionado(delListContainer);
    if (!id) return alert("Seleccione una carrera");

    try {
        const lista = await getCarreras();
        const carrera = lista.find(c => c.iD_Car === Number(id));

        $("delConfirmContainer").style.display = "block";
        $("delConfirmText").innerText =
            `¿Eliminar la carrera "${carrera.car_Nom}" (${carrera.turno})?`;

        $("btnConfirmDelete").dataset.id = id;
    } catch (err) {
        mostrarError(err);
    }
};

$("btnConfirmDelete").onclick = async () => {
    const id = $("btnConfirmDelete").dataset.id;
    if (!id) return;

    try {
        await borrarCarrera(id);
        mostrarExito("Carrera eliminada correctamente");

        $("delConfirmContainer").style.display = "none";
        cargarLista(delListContainer); // refresca lista
    } catch (err) {
        mostrarError(err);
    }
};


$("btnCancelDelete").onclick =
$("btnCancelarEliminar").onclick = () => ocultarTodos(panels);

// ---------- LISTAR ----------
$("btnRefrescar").onclick = cargarTabla;
$("btnCerrarList").onclick = () => ocultarTodos(panels);

// ---------- FUNCIONES ----------
async function cargarLista(container) {
    try {
        const lista = await getCarreras();
        renderLista(container, lista);
    } catch (e) {
        mostrarError(e);
    }
}

async function cargarTabla() {
    try {
        tablaContainer.innerHTML = "";
        await new Promise(resolve => setTimeout(resolve, 1000));
        const lista = await getCarreras();
        renderTabla(tablaContainer, lista);
    } catch (e) {
        mostrarError(e);
    }
}
