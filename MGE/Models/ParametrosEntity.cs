namespace MGE.Models
{
    public class ParametrosEntity
    {
        public int Id { get; set; }
        
        public decimal ValorKwh { get; set; }
        
        public decimal FaixaConsumoAlto { get; set; }
        
        public decimal FaixaConsumoMedio { get; set; }
    }
}