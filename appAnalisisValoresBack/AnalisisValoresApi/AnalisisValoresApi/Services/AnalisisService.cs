using AnalisisValoresApi.Models;

namespace AnalisisValoresApi.Services
{
    public class AnalisisService
    {
        public AnalisisPostDTO Analizar(Analisis analisis)
        {
            List<int> valores = analisis.Valores;
            List<int> valoresOrdenados = new List<int>(valores);
            valoresOrdenados.Sort();

            decimal promedio = this.CalcularPromedio(valores);
            decimal mediana = this.CalcularMediana(valoresOrdenados);
            decimal varianza = this.CalcularVarianza(valores, promedio);
            decimal desviacion = this.CalcularDesviacionEstandar(varianza);
            int fueraDeEspecificacion = valores.Count(v => v < analisis.LimiteInferior || v > analisis.LimiteSuperior);

            AnalisisPostDTO resultado = new AnalisisPostDTO
            {               
                Promedio = promedio,
                Mediana = mediana,
                Varianza = varianza,
                Desviacion = desviacion,
                FueraDeEspecificacion = fueraDeEspecificacion,
                ValoresOrdenados = valoresOrdenados,
            };

            return resultado;
        }

        //Funcion para calcular el promedio, que es la suma de los elementos de la lista dividido la cantidad de elementos.
        private decimal CalcularPromedio(List<int> valores)
        {
            int acumulador = 0;
            int cantidad = 0;

            foreach (int numero in valores)
            {
                acumulador = acumulador + numero;
                cantidad += 1;
            }

            return (decimal)acumulador / cantidad;
        }

        //Funcion para calcular la varianza
        //que es la suma de los elementos - promedio al cuadrado
        //=> SUMA(x-p)^2 dividido la cantidad de elementos totales(poblacion) cantidad de elementos - 1(muestra).
        private decimal CalcularVarianza(List<int> valores, decimal promedio)
        {
            decimal acumuladorSumaDiferenciasAlCuadrado = 0;

            foreach (int numero in valores)
            {
                decimal diferencia = numero - promedio;
                acumuladorSumaDiferenciasAlCuadrado += diferencia * diferencia;
            }

            return acumuladorSumaDiferenciasAlCuadrado / valores.Count;
        }

        //Funcion para calcular la desviacion. A partir de la varianza, al hacer raiz cuadrada de la varianza obtengo la desviacion.
        private decimal CalcularDesviacionEstandar(decimal varianza)
        {
            double raizCuadrada = Math.Sqrt((double)varianza);
            decimal desviacionEstandar = (decimal)raizCuadrada;

            return desviacionEstandar;
        }

        //Funcion para calcular la mediana, que en datos ordenados, la mediana es el valor de la mitad.
        //Si la cantidad de elementos es impar es la del medio.
        //Si la cantidad de elementos es par es el promedio de los dos datos del medio.
        private decimal CalcularMediana(List<int> valoresOrdenados)
        {

            int cantidadDeElementos = valoresOrdenados.Count;

            if(cantidadDeElementos % 2 == 0)
            {
                int centralUno = valoresOrdenados[(cantidadDeElementos / 2) - 1];
                int centralDos = valoresOrdenados[cantidadDeElementos / 2];
                return ((decimal)centralUno + centralDos) / 2 ;
            }

            return valoresOrdenados[cantidadDeElementos/2];
        }
    }
}
