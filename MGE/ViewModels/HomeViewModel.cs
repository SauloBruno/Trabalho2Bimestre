using System.Collections.Generic;
using MGE.Models;

namespace MGE.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<ItemApoioEntity> it { get; set; }

        public DadosApoio dados { get; set; }
        public ICollection<CategoriaApoioEntity> ct { get; set; }

        
        public HomeViewModel()
        {
            it = new List<ItemApoioEntity>();
            ct = new List<CategoriaApoioEntity>();
            dados = new DadosApoio();
        }
    }
}