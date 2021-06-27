using System.Collections.Generic;
using MGE.Models;

namespace MGE.ViewModels
{
    public class CategoriasViewModel
    {
        public string MsgSucess { get; set; }
        
        public string MsgFail { get; set; }

        public ICollection<CategoriasEntity> Categorias { get; set; }

        public CategoriasViewModel()
        {
            Categorias = new List<CategoriasEntity>();
        }
    }
}