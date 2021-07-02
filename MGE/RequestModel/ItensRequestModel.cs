using System;

namespace MGE.RequestModel
{
    public class ItensRequestModel
    {
        public Guid Ed { get; set; }
        public int Categoria { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public decimal ConsumoWatts { get; set; }
        public int HorasUsoDiario { get; set; }
        public String DataFabricacao { get; set; }
    }
}