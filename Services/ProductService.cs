using ProductApi.Models;
using ProductApi.Repositories;
using ProductApi.Exceptions;

namespace ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }

        return product;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException(
                "Tên sản phẩm không được để trống"
            );
        }

        if (product.Price <= 0)
        {
            throw new ArgumentException(
                "Giá sản phẩm phải lớn hơn 0"
            );
        }

        return await _repository.CreateAsync(product);
    }
}
