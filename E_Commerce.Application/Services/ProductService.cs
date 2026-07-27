using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
          _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            var data = _mapper.Map<IReadOnlyList<BrandDto>>(brands);

            return Result<IReadOnlyList<BrandDto>>.Ok(data);

        }

        public async Task<Result<IReadOnlyList<ProductsDto>>> GetAllProductsAsync(CancellationToken ct = default)
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(ct);
            var data = _mapper.Map<IReadOnlyList<ProductsDto>>(products);

            return Result<IReadOnlyList<ProductsDto>>.Ok(data);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);
            var data = _mapper.Map<IReadOnlyList<TypeDto>>(types);

            return Result<IReadOnlyList<TypeDto>>.Ok(data);
        }

        public async Task<Result<ProductsDto>> GetProductByIdAsync(int id, CancellationToken ct)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id, ct);
            if (product == null)
                return Result<ProductsDto>.Fail(Error.NotFound("Product.NotFound" , $"Product With Id {id} Not Found"));

          else
            return Result<ProductsDto>.Ok(_mapper.Map<ProductsDto>(product));
        }
    }
}
