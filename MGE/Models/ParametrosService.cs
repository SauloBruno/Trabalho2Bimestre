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
    }
}