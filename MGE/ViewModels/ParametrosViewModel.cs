using System.Collections.Generic;
using MGE.Models;

namespace MGE.ViewModels
{
    public class ParametrosViewModel
    {
        public string MsgSucess { get; set; }
        
        public string MsgFail { get; set; }
        public ICollection<ParametrosEntity> parametros { get; set; }
 
        public ParametrosViewModel()
        {
            parametros = new List<ParametrosEntity>();
        }
    }
}