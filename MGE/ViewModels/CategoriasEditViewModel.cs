namespace MGE.ViewModels
{
    public class CategoriasEditViewModel
    {
        public int Id { get; set; }
        public Categ Categoria { get; set; }
        public string msgFail { get; set; }
    }

    public class Categ
    {
        public string Descricao { get; set; }
    }

}