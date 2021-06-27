using System.Collections.Generic;
using System.Linq;
using MGE.Data;

namespace MGE.Models
{
    public class ParametrosService
    {
        private readonly DataBaseContext _dataBaseContext;

        public ParametrosService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public void InserirParametro(decimal valorKwh, decimal faixaConsumoAlto, decimal faixaConsumoMedio)
        {
            var p = new ParametrosEntity();
            p.ValorKwh = valorKwh;
            p.FaixaConsumoAlto = faixaConsumoAlto;
            p.FaixaConsumoMedio = faixaConsumoMedio;

            _dataBaseContext.Parametros.Add(p);
            _dataBaseContext.SaveChanges();
        }

        public void Editar(int id, decimal valorKwh, decimal faixaConsumoAlto, decimal faixaConsumoMedio)
        {
            var p = new ParametrosEntity();
            p.Id = id;
            p.ValorKwh = valorKwh;
            p.FaixaConsumoAlto = faixaConsumoAlto;
            p.FaixaConsumoMedio = faixaConsumoMedio;

            _dataBaseContext.Parametros.Update(p);
            _dataBaseContext.SaveChanges();
        }

        public void Remover(int id)
        {
            var p = _dataBaseContext.Parametros.Find(id);
            _dataBaseContext.Parametros.Remove(p);
            _dataBaseContext.SaveChanges();
        }
        
        public ParametrosEntity BuscarPeloId(int id)
        {
            var p = _dataBaseContext.Parametros.Single(p => p.Id == id);
            return p;
        }

        public List<ParametrosEntity> BuscarTodos()
        {
            return _dataBaseContext.Parametros.ToList();
        }
        
    }
}