namespace MGE.ViewModels
{
    public class ParametrosEditViewModel
    {
        public int Id { get; set; }

        public Param Parametro { get; set; }
        
        public string msgFail { get; set; }
    }

    public class Param
    {
        public decimal ValorKwh { get; set; }
        
        public decimal FaixaConsumoAlto { get; set; }
        
        public decimal FaixaConsumoMedio { get; set; }
    }
}