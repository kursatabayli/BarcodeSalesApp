using Barkod.Interfaces;
using Barkod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barkod.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<Product> GetProductByBarcode(long barcode) => await _productRepository.GetByBarcodeAsync(barcode);

        public async Task UpdateStock(long barcode, int quantity)
        {
            var product = await _productRepository.GetByBarcodeAsync(barcode);
            if (product != null && product.StockQuantity >= quantity)
            {
                product.StockQuantity -= quantity;
                await _productRepository.Update(product.Id, product);
            }
        }
    }
}
