using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CoreMentoringApp.WebSite.Areas.Api.Controllers
{
    [Area("api")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ProductsController(IDataRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _repository.GetProductsAsync()));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDto)
        {
            if (productDto.CategoryId.HasValue)
            {
                var category = await _repository.GetCategoryByIdAsync(productDto.CategoryId.Value);
                if (category == null)
                {
                    return BadRequest("CategoryId is invalid.");
                }
            }

            if (productDto.SupplierId.HasValue)
            {
                var category = await _repository.GetSupplierByIdAsync(productDto.SupplierId.Value);
                if (category == null)
                {
                    return BadRequest("SupplierId is invalid.");
                }
            }

            var product = _mapper.Map<Product>(productDto);
            await _repository.CreateProductAsync(product);
            if (await _repository.CommitAsync() > 0)
            {
                var link = _linkGenerator.GetUriByAction(HttpContext,"Get", "Products",
                    new {id = product.ProductId});
                return Created(link, _mapper.Map<ProductDTO>(product));
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> Put(int id, ProductDTO productDto)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (productDto.CategoryId.HasValue)
            {
                var category = await _repository.GetCategoryByIdAsync(productDto.CategoryId.Value);
                if (category == null)
                {
                    return BadRequest("CategoryId is invalid.");
                }
            }

            if (productDto.SupplierId.HasValue)
            {
                var category = await _repository.GetSupplierByIdAsync(productDto.SupplierId.Value);
                if (category == null)
                {
                    return BadRequest("SupplierId is invalid.");
                }
            }

            _mapper.Map(productDto, product);

            if (await _repository.CommitAsync() > 0)
            {
                return Ok(_mapper.Map<ProductDTO>(product));
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _repository.DeleteProductAsync(product);

            if (await _repository.CommitAsync() > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
