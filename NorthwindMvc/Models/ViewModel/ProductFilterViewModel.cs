using NorthwindMvc.Models.Entities;

namespace NorthwindMvc.Models.ViewModel;

public class ProductFilterViewModel
{
    public string? FilterType { get; set; }
    public int? FilterId { get; set; }
    public List<Category> Categories { get; set; }
    public List<Supplier> Suppliers { get; set; }
    public List<ProductListItemViewModel> Products { get; set; }


}
