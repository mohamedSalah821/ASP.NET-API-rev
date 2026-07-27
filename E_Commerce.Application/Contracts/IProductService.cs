using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Products;

namespace E_Commerce.Application.Contracts
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductsDto>>> GetAllProductsAsync(CancellationToken ct = default);

        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default);

        Task<Result<ProductsDto>> GetProductByIdAsync(int id, CancellationToken ct);
    }
}
