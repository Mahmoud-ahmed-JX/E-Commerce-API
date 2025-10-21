using Shared;
using Shared.Dtos;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IProductService
    {
        //get all products
        Task<PaginationResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        //get product by id
        Task<ProductResultDto> GetProductByIdAsync(int Id);
        //get all brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //get all types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
