using MGE.Models;
using Microsoft.EntityFrameworkCore;

namespace MGE.Data
{
    public class DataBaseContext : DbContext
    {
        
        public DbSet<CategoriasEntity> Categorias { get; set; }

        public DbSet<ItensEntity> Itens { get; set; }

        public DbSet<ParametrosEntity> Parametros { get; set; }
        
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
                   
        }
        
    }
}