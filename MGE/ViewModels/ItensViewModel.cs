using System.Collections.Generic;
using MGE.Models;

namespace MGE.ViewModels
{
    public class ItensViewModel
    {
        public string MsgSucess { get; set; }
        
        public string MsgFail { get; set; }
        public ICollection<CategoriasEntity> Categorias { get; set; }
        public ICollection<ItensEntity> Itens { get; set; }

        public ItensViewModel()
        {
            Categorias = new List<CategoriasEntity>();
            Itens = new List<ItensEntity>();
        }
    }
    
}