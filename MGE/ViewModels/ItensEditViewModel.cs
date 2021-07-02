using System;
using System.Collections.Generic;
using MGE.Models;

namespace MGE.ViewModels
{
    public class ItensEditViewModel
    {
        public Guid Id { get; set; }
        
        public string MsgFail { get; set; }

        public It Item { get; set; }
        public ICollection<CategoriasEntity> Categorias { get; set; }

        public ItensEditViewModel()
        {
            Categorias = new List<CategoriasEntity>();
            Item = new It();
        }
    }

    public class It
    {
        public String Nome { get; set; }
        
        public String Descricao { get; set; }

        public int IdCat { get; set; }
        
        public decimal ConsumoWatts { get; set; }
        
        public int HorasUsoDiario { get; set; }
        
        public String DataFabricacao { get; set; }
    }
}