namespace AnalisisValoresApi.Models
{
    public class Analisis
    {
        public int LimiteInferior {  get; set; }
        public int LimiteSuperior { get; set; }
        public List<int> Valores { get; set; }
         
    }
}
