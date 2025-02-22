using Microsoft.AspNetCore.Components;
using Barkod.Interfaces;
using Barkod.Models;
using Microsoft.Extensions.Localization;
using Barkod.Resources.Locales;
using Barkod.Helpers;

namespace Barkod.Components.Pages.ProductList
{
    public partial class ProductList : ComponentBase
    {
        [Inject] public IProductRepository ProductRepository { get; set; }
        [Inject] public INavigationService NavigationService { get; set; }
        [Inject] public IStringLocalizer<Lang> Localizer { get; set; }

        public string SearchText = "";

        public List<Product> products { get; set; } = new();

        protected override async Task OnInitializedAsync() => products = await ProductRepository.GetAll();

        public void ProductDetail(Product product) => NavigationService.NavigateTo($"/product-detail/{product.Id}");

        public IEnumerable<Product> FilteredProducts =>
            products.FilterBySearchText(SearchText);
    }
}
