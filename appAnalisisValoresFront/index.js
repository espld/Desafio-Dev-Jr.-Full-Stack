const numeros = [];

let agregarNumeroBtn = document.getElementById("agregarNumeroBtn");
agregarNumeroBtn.addEventListener('click', agregarNumeros);
let agregarCalcularBtn = document.getElementById("calcularBtn");
agregarCalcularBtn.addEventListener('click', calcular)
let vaciarBtn = document.getElementById("vaciarBtn");
vaciarBtn.addEventListener('click',vaciarValores);

function agregarNumeros(){
  const inputAgregar = document.getElementById("agregarNumero");
  const numero = parseInt(inputAgregar.value);
  inputAgregar.focus();

  if (!isNaN(numero)) {
    numeros.push(numero);
    document.getElementById("numerosAgregados").value = numeros.join(", ");
    inputAgregar.value = "";
  } else {
    Toastify({
        text: "Agregá un número válido.",
        duration: 2000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        style: {
          textTransform: "uppercase",
        },
        onClick: function () {}, // Callback after click
      }).showToast();
  }
}

function calcular(){

  const inputLimiteInferior = document.getElementById("limiteInferior");
  const inputLimiteSuperior = document.getElementById("limiteSuperior");
  const limiteInferior = parseInt(inputLimiteInferior.value);
  const limiteSuperior = parseInt(inputLimiteSuperior.value);

  if (isNaN(limiteInferior) || isNaN(limiteSuperior)) {
    Toastify({
        text: "Ingresá ambos límites.",
        duration: 2000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        style: {
          textTransform: "uppercase",
        },
        onClick: function () {}, // Callback after click
      }).showToast();
    return;
  }

  if (limiteSuperior < limiteInferior) {
    Toastify({
      text: "El límite superior no puede ser menor al inferior.",
      duration: 2000,
      close: true,
      gravity: "top", // `top` or `bottom`
      position: "center", // `left`, `center` or `right`
      stopOnFocus: true, // Prevents dismissing of toast on hover
      style: {
        textTransform: "uppercase",
      },
      onClick: function () {}, // Callback after click
    }).showToast();
    return;
  }

  if (numeros.length === 0) {
    Toastify({
        text: "Agregá al menos un número",
        duration: 2000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        style: {
          textTransform: "uppercase",
        },
        onClick: function () {}, // Callback after click
      }).showToast();
    return;
  }

  const calculo = {
    limiteInferior,
    limiteSuperior,
    valores: numeros
  };

  realizarCalculo(calculo);

}

function realizarCalculo(calculo){
  fetch("http://localhost:5143/api/Analisis", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(calculo)
  })
    .then(response => {
      return response.json();
    })
    .then(datos => {
      document.getElementById("valoresOrdenados").innerHTML = `Valores ordenados: ${datos.valoresOrdenados}`;
      document.getElementById("promedioResultado").innerHTML = `Promedio: ${datos.promedio.toFixed(1)}`;
      document.getElementById("medianaResultado").innerHTML = `Mediana: ${datos.mediana}`
      document.getElementById("desviacionResultado").innerHTML = `Desviación estándar: ${datos.desviacion.toFixed(2)}`
      document.getElementById("varianzaResultado").innerHTML = `Varianza: ${datos.varianza.toFixed(2)}`
      document.getElementById("fueraResultado").innerHTML = `Valores fuera de especificación: ${datos.fueraDeEspecificacion}`
      document.getElementById("resultados").style.display = "flex";
    })
    .catch(error => {
      console.error("Error al analizar los datos.", error);
    });
}


function vaciarValores()
{
  numeros.length = 0;
  document.getElementById("numerosAgregados").value = "";
  document.getElementById("resultados").style.display = "none";
}

