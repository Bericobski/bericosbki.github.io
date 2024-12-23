


// Código de Inicio de Sesión
let usuarioActual = localStorage.getItem('usuarioActual') || null;

function iniciarSesion(usuario, contrasena) {
    const usuariosRegistrados = JSON.parse(localStorage.getItem('usuarios')) || [];
    const usuarioEncontrado = usuariosRegistrados.find(
        (u) => u.usuario === usuario && u.contrasena === contrasena
    );

    if (usuarioEncontrado) {
        usuarioActual = usuario;
        localStorage.setItem('usuarioActual', usuario);
        alert(`¡Bienvenido, ${usuario}!`);
        actualizarEstadoSesion();
    } else {
        alert('Usuario o contraseña incorrectos.');
    }
}

function registrarUsuario(usuario, contrasena) {
    let usuariosRegistrados = JSON.parse(localStorage.getItem('usuarios')) || [];

    if (usuariosRegistrados.some((u) => u.usuario === usuario)) {
        alert('El usuario ya existe.');
        return;
    }

    usuariosRegistrados.push({ usuario, contrasena });
    localStorage.setItem('usuarios', JSON.stringify(usuariosRegistrados));
    alert('Usuario registrado con éxito.');
}

function cerrarSesion() {
    usuarioActual = null;
    localStorage.removeItem('usuarioActual');
    alert('Has cerrado sesión.');
    actualizarEstadoSesion();
}

function actualizarEstadoSesion() {
    const estadoSesion = document.getElementById('estado-sesion');

    if (usuarioActual) {
        estadoSesion.innerHTML = `
            <p>Sesión iniciada como: ${usuarioActual}</p>
            <button onclick="cerrarSesion()">Cerrar sesión</button>
        `;
    } else {
        estadoSesion.innerHTML = `
            <p>No has iniciado sesión.</p>
        `;
    }
}

function mostrarFormularioInicioSesion() {
    const formulario = document.getElementById('formulario');
    formulario.innerHTML = `
        <h2>Iniciar Sesión</h2>
        <input type="text" id="usuario" placeholder="Usuario">
        <input type="password" id="contrasena" placeholder="Contraseña">
        <button onclick="procesarInicioSesion()">Iniciar</button>
    `;
}

function mostrarFormularioRegistro() {
    const formulario = document.getElementById('formulario');
    formulario.innerHTML = `
        <h2>Registrarse</h2>
        <input type="text" id="usuario" placeholder="Usuario">
        <input type="password" id="contrasena" placeholder="Contraseña">
        <button onclick="procesarRegistro()">Registrar</button>
    `;
}

function procesarInicioSesion() {
    const usuario = document.getElementById('usuario').value;
    const contrasena = document.getElementById('contrasena').value;
    iniciarSesion(usuario, contrasena);
}

function procesarRegistro() {
    const usuario = document.getElementById('usuario').value;
    const contrasena = document.getElementById('contrasena').value;
    registrarUsuario(usuario, contrasena);
}

// Eventos para los botones de Login y Register
document.getElementById('login').addEventListener('click', mostrarFormularioInicioSesion);
document.getElementById('register').addEventListener('click', mostrarFormularioRegistro);

// Inicializar
actualizarEstadoSesion();







/********************************* */

//Paginas de games funciones 

function updateFeaturedImage(element) {
    const featuredDisplay = document.getElementById('featured-display');
    const featuredContent = document.getElementById('featured-content');
    
    // Reemplaza cualquier iframe existente con una nueva imagen
    if (featuredContent.tagName !== "IMG") {
        const img = document.createElement("img");
        img.id = "featured-content";
        img.src = element.src;
        img.alt = element.alt;
        img.style.maxWidth = "100%";
        img.style.maxHeight = "100%";
        featuredDisplay.replaceChild(img, featuredContent);
    } else {
        featuredContent.src = element.src;
        featuredContent.alt = element.alt;
    }
}

function updateFeaturedVideo(element) {
    const featuredDisplay = document.getElementById('featured-display');
    const featuredContent = document.getElementById('featured-content');
    
    // Reemplaza cualquier imagen existente con un iframe
    if (featuredContent.tagName !== "IFRAME") {
        const iframe = document.createElement("iframe");
        iframe.id = "featured-content";
        iframe.src = element.src;
        iframe.style.width = "100%";
        iframe.style.height = "100%";
        iframe.frameBorder = "0";
        iframe.allow = "accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture";
        iframe.allowFullscreen = true;
        featuredDisplay.replaceChild(iframe, featuredContent);
    } else {
        featuredContent.src = element.src;
    }
}



/*********************************************************************************************************************************************** */









//Agregar producto al carrito 




function agregarProducto(event){
    var producto = {
        id: event.target.getAttribute("data-id"),
        nombre: event.target.getAttribute("data-nombre"),
        precio: event.target.getAttribute("data-precio")
    }; 

    

    var carrito = JSON.parse(localStorage.getItem("carrito")) || []; 

    // Verificar si el producto ya está en el carrito
    var existe = carrito.some(item => item.id === producto.id);

    if (existe) {
        return; // No agregar el producto si ya existe
    }

    carrito.push(producto); 
    localStorage.setItem("carrito", JSON.stringify(carrito));
    actualizarCarrito(); //Actualiza el carrito en pantalla
}



function eliminarProducto(idProducto){
    var carrito = JSON.parse(localStorage.getItem("carrito")) || []; 
    carrito = carrito.filter(function (producto){
        return producto.id !== idProducto;
    }); 

    localStorage.setItem("carrito", JSON.stringify(carrito));
    actualizarCarrito();
}

function vaciarCarrito(){
    localStorage.removeItem("carrito");  // No seria clear?
    var totalElemento = document.getElementById("total");
    var total = 0;
    totalElemento.textContent = `Total: $${total.toFixed(2)}`;

    actualizarCarrito();
}




function actualizarCarrito(){
    var carrito = JSON.parse(localStorage.getItem("carrito")) || [];
    var listaCarrito = document.getElementById("lista-carrito"); 
    listaCarrito.innerHTML = "";
    for (var i = 0; i < carrito.length; i++){
        var producto = carrito[i]; 
        var li = document.createElement("li"); 
        li.textContent = producto.nombre + " - $" + producto.precio; 
        listaCarrito.appendChild(li); 
    }
}






function toggleCarrito() {
    var ventanaCarrito = document.getElementById("ventana-carrito");
    if (ventanaCarrito.style.right === "0px") {
        ventanaCarrito.style.right = "-350px"; // Ocultar ventana
    } else {
        ventanaCarrito.style.right = "0px"; // Mostrar ventana
        mostrarCarrito(); // Mostrar productos actualizados
    }
}

// Función para mostrar el carrito
function mostrarCarrito() {
    var carrito = JSON.parse(localStorage.getItem("carrito")) || [];
    var listaCarrito = document.getElementById("lista-carrito");
    var totalElemento = document.getElementById("total");

    // Limpiar contenido anterior
    listaCarrito.innerHTML = "";
    var total = 0;

    // Crear elementos para cada producto
    carrito.forEach(function (producto) {
        var divProducto = document.createElement("div");
        divProducto.classList.add("producto");

        // Mostrar nombre y precio
        divProducto.innerHTML = `
            <span>${producto.nombre}</span>
            <span>$${producto.precio}</span>
        `;

        // Botón para eliminar producto
        var btnEliminar = document.createElement("button");
        btnEliminar.textContent = "X";
        btnEliminar.classList.add("btn-eliminar");
        btnEliminar.onclick = function () {
            eliminarProducto(producto.id);
            mostrarCarrito(); // Actualizar vista
        };

        divProducto.appendChild(btnEliminar);
        listaCarrito.appendChild(divProducto);

        // Sumar al total
        total += parseFloat(producto.precio);
    });

    // Mostrar total
    totalElemento.textContent = `Total: $${total.toFixed(2)}`;
}