using System;
using System.Collections.Generic;
using System.Linq;
using MGE.Data;
using Microsoft.EntityFrameworkCore;

namespace MGE.Models
{
    public class ItensService
    {
        private readonly DataBaseContext _dataBaseContext;

        public ItensService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        
        public void InserirItem(CategoriasEntity categoria, String nome, string descricao, decimal consumoWatts, int horaUso, String dataFabricacao)
        {
            ItensEntity i = new ItensEntity();
            
            i.Categoria = categoria;
            i.Nome = nome;
            i.Descricao = descricao;
            i.ConsumoWatts = consumoWatts;
            i.HorasUsoDiario = horaUso;
            i.DataFabricacao = DateTime.Parse(dataFabricacao);

            _dataBaseContext.Itens.Add(i);
            _dataBaseContext.SaveChanges();
        }
        
        public void EditarItem(Guid id, CategoriasEntity categoria, String nome, string descricao, decimal consumoWatts, int horaUso, String dataFabricacao)
        {
            ItensEntity i = new ItensEntity();

            i.Id = id;
            i.Categoria = categoria;
            i.Nome = nome;
            i.Descricao = descricao;
            i.ConsumoWatts = consumoWatts;
            i.HorasUsoDiario = horaUso;
            i.DataFabricacao = DateTime.Parse(dataFabricacao);

            _dataBaseContext.Itens.Update(i);
            _dataBaseContext.SaveChanges();
        }
        
        public void Remover(Guid id)
        {
            var i = _dataBaseContext.Itens.Find(id);
            _dataBaseContext.Itens.Remove(i);
            _dataBaseContext.SaveChanges();

        }

        public List<ItensEntity> BuscarTodos()
        {
            return _dataBaseContext.Itens
                .Include(i => i.Categoria)
                .ToList();
        }

        public ItensEntity BuscarPeloId(Guid id)
        {
            var it = _dataBaseContext.Itens
                .Include(i => i.Categoria)
                .Single(i => i.Id.Equals(id));
            
            return it;
        }
    }
}