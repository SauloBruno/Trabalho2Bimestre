using System;
using System.Net.Mime;

namespace MGE.Models
{
    public class ItensEntity
    {
        public Guid Id { get; set; }
        
        public CategoriasEntity Categoria { get; set; }
        
        public String Nome { get; set; }
        
        public String Descricao { get; set; }
        
        public DateTime DataFabricacao { get; set; }
        
        public decimal ConsumoWatts { get; set; }
        
        public int HorasUsoDiario { get; set; }
    }
}