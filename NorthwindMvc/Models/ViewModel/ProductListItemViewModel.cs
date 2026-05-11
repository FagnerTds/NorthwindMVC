namespace NorthwindMvc.Models.ViewModel
{
    public class ProductListItemViewModel
    {
        public int ProductId { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public string? Categoria { get; set; }
        public string? Fornecedor { get; set; }
    }
}
