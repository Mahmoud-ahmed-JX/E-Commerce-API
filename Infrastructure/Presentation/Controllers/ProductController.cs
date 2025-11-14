using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos.ProductDtos;
using Shared.Enums;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
  
    public class ProductController(IServiceManager _serviceManager):ApiController
    {
        //get all products
        [HttpGet]
        public async Task<ActionResult<PaginationResult<ProductResultDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationParameters parameters)
            =>Ok(await _serviceManager.ProductService.GetAllProductsAsync(parameters));

        //get all brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            =>Ok(await _serviceManager.ProductService.GetAllBrandsAsync());
        //get all types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            =>Ok(await _serviceManager.ProductService.GetAllTypesAsync());
        //get product by id
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductResultDto),StatusCodes.Status200OK)]
       


        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
    }
}
