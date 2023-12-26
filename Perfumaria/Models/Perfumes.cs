namespace Perfumaria.Models
{
    public class Perfumes
    {
        public int id {  get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        //public Categoria idCategoria { get; set; }
        public decimal valor { get; set; }
        public int quantidade { get; set; }
    }
}
