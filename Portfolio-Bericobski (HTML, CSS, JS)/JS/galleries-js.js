const modal = document.querySelector('.modal');
const modalImg = modal.querySelector('img');
const closeModal = document.querySelector('.close-modal');
const leftArrow = document.querySelector('.arrow.left');
const rightArrow = document.querySelector('.arrow.right');
const images = document.querySelectorAll('.gallery-item img');

let currentIndex = 0;

function openModal(index) {
  currentIndex = index;
  

  // Desactiva el scroll en la página principal
  document.body.style.overflow = 'hidden';

  // Mostrar el modal
  modal.style.display = 'flex';

  // Centrar el modal en el viewport
  modal.style.position = 'fixed'; // Asegurar que esté fijado al viewport
  modal.style.top = '50%';
  modal.style.left = '50%';
  modal.style.transform = 'translate(-50%,-50%)'; // Centrar horizontal y verticalmente
  modal.style.width = "100%"; // Ajustar al contenido
  modal.style.height = '100%';

  var scrollPosition = window.scrollY || document.documentElement.scrollTop; // Barra, conseguir su altura

  var viewportHeight = window.innerHeight || document.documentElement.clientHeight // Navegador 

  var newmodaltop = viewportHeight/2 + scrollPosition;



  modal.style.top = newmodaltop + "px";

  


  updateModalImage();
}


function closeModalFn() {
  modal.style.display = 'none';
  document.body.style.overflow = 'auto'; // Reactivar el scroll
}

function showNextImage() {
  currentIndex = (currentIndex + 1) % images.length;
  updateModalImage();
}

function showPreviousImage() {
  currentIndex = (currentIndex - 1 + images.length) % images.length;
  updateModalImage();
}

function updateModalImage() {
  const newSrc = images[currentIndex].src;
  
  modalImg.classList.remove('active');
  
  setTimeout(() => {
    modalImg.src = newSrc;
    modalImg.classList.add('active');
  }, 50);

  // Asegurarse de que la imagen no exceda el tamaño de la pantalla
  modalImg.style.maxWidth = `${window.innerWidth}px`;
  modalImg.style.maxHeight = `calc(100vh - 20px)`;
}

// Event listeners
images.forEach((img, index) => {
  img.addEventListener('click', () => openModal(index));
});

closeModal.addEventListener('click', closeModalFn);
rightArrow.addEventListener('click', showNextImage);
leftArrow.addEventListener('click', showPreviousImage);

modal.addEventListener('click', (e) => {
  if (e.target === modal) {
    closeModalFn();
  }
});

document.addEventListener("DOMContentLoaded", () => {
  document.body.classList.add("loaded"); 
});




//CONTACT CODE 



const form = document.getElementById('contactForm');
const successMessage = document.querySelector('.form-message.success');
const errorMessage = document.querySelector('.form-message.error');

form.addEventListener('submit', async (event) => {
    event.preventDefault(); // Evita la recarga de la página

    // Obtiene los valores de los campos
    const name = form.name.value.trim();
    const email = form.email.value.trim();
    const message = form.message.value.trim();

    // Verifica que todos los campos estén completos
    if (!name || !email || !message) {
        successMessage.hidden = true;
        errorMessage.hidden = false;
        return;
    }

    // Si los campos están completos, envía el formulario
    const formData = new FormData(form);
    const endpoint = form.action;
    const options = {
        method: form.method,
        body: formData,
        headers: { 'Accept': 'application/json' }
    };

    try {
        const response = await fetch(endpoint, options);
        if (response.ok) {
            successMessage.hidden = false;
            errorMessage.hidden = true;
            form.reset(); // Limpia el formulario
        } else {
            throw new Error('Error al enviar el formulario');
        }
    } catch (error) {
        successMessage.hidden = true;
        errorMessage.hidden = false;
    }
});

