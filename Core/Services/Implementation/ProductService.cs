using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared;
using Shared.Dtos;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            
            return _mapper.Map<IEnumerable<BrandResultDto>>(Brands);

        }

        public async Task<PaginationResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {

            var specification = new ProductsWithTypesAndBrandsSpecification(parameters);
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            var Products = productRepo.GetAllAsync(specification);
            var ProductsResult= _mapper.Map<IEnumerable<ProductResultDto>>(Products);
            var PageCount= ProductsResult.Count();
            var countSpecification= new ProductCountSpecification(parameters);
            var totalCount= await productRepo.CountAsync(countSpecification);
            return new PaginationResult<ProductResultDto>(parameters.PageIndex, PageCount, totalCount, ProductsResult);
           

            
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types =await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeResultDto>>(Types);
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int Id)
        {
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductsWithTypesAndBrandsSpecification(Id));
            return Product is null? throw new ProductNotFoundException(Id): _mapper.Map<ProductResultDto>(Product);
        }
    }
}
