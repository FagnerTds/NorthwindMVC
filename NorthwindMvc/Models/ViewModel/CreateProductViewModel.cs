using NorthwindMvc.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace NorthwindMvc.Models.ViewModel
{
    public class CreateProductViewModel
    {
        public int? ProductId { get; set; }

        [Required(ErrorMessage = "O Nome é Obrigatório")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Por favor descreva um preço")]
        [Range(0.01, 9999999, ErrorMessage = "Preço deve ser maior que zero")]
        public decimal? UnitPrice { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Selecione um Fornecedor")]
        public int? SupplierId { get; set; }

        public List<Category> Categories { get; set; }

        public List<Supplier> Suppliers { get; set; }
    }
}
