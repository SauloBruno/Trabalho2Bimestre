using MGE.Data;

namespace MGE.Models
{
    public class ItensService
    {
        private readonly DataBaseContext _dataBaseContext;

        public ItensService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
    }
}