namespace AnalisisValoresApi.Models
{
    public class AnalisisPostDTO
    {
        public decimal Promedio { get; set; }
        public decimal Mediana { get; set; }
        public decimal Varianza { get; set; }
        public decimal Desviacion { get; set; }
        public int FueraDeEspecificacion { get; set; }
        public List<int> ValoresOrdenados { get; set; }
    }
}
