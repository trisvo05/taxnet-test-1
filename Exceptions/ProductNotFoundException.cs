namespace ProductApi.Exceptions;

public class ProductNotFoundException : Exception
{
    public int ProductId { get; }

    public ProductNotFoundException(int productId)
        : base($"TaxNet Không tìm thấy sản phẩm có ID {productId}.")
    {
        ProductId = productId;
    }
}
