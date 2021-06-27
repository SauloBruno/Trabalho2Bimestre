using System.Collections.Generic;
using System.Linq;
using MGE.Data;

namespace MGE.Models
{
    public class CategoriasService
    {
        private readonly DataBaseContext _dataBaseContext;

        public CategoriasService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public void InserirCategoria(string descricao)
        {
            CategoriasEntity c = new CategoriasEntity();
            c.Descricao = descricao;

            _dataBaseContext.Categorias.Add(c);
            _dataBaseContext.SaveChanges();

        }

        public void EditarCategortia(int id, string descricao)
        {
            var ct = new CategoriasEntity();
            ct.Id = id;
            ct.Descricao = descricao;

            _dataBaseContext.Categorias.Update(ct);
            _dataBaseContext.SaveChanges();
        }

        public void Remover(int id)
        {
            var c = _dataBaseContext.Categorias.Find(id);
            _dataBaseContext.Categorias.Remove(c);
            _dataBaseContext.SaveChanges();

        }

        public CategoriasEntity BuscarPeloId(int id)
        {
            var c = _dataBaseContext.Categorias.Single(c => c.Id == id);
            return c;
        }

        public List<CategoriasEntity> BuscarTodos()
        {
            return _dataBaseContext.Categorias.ToList();
        }

    }
}