const API_BASE = "https://proyectogeneral.onrender.com/Carreras";

async function getCarreras() {
    const resp = await axios.get(API_BASE);
    return resp.data;
}

function crearCarrera(data) {
    return axios.post(API_BASE, data);
}

function actualizarCarrera(id, data) {
    return axios.put(`${API_BASE}/${id}`, data);
}

function borrarCarrera(id) {
    return axios.delete(`${API_BASE}/${id}`);
}
