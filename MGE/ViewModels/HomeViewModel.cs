using System.Collections.Generic;
using MGE.Models;

namespace MGE.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<ItemApoioEntity> it { get; set; }

        public HomeViewModel()
        {
            it = new List<ItemApoioEntity>();
        }
    }
}