using Domain.Entities.ProductModule;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductsWithTypesAndBrandsSpecification: BaseSpecification<Product, int>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParameters parameters) :
            base(P=>(!parameters.TypeId.HasValue || P.TypeId==parameters.TypeId) &&
            (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId) &&
            (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower())))
                                                                                
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (parameters.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(P=>P.Id==id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
