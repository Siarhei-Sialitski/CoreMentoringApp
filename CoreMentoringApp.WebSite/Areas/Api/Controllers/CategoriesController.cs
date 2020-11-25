using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Areas.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreMentoringApp.WebSite.Areas.Api.Controllers
{
    [Area("api")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(IDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(await _repository.GetCategoriesAsync()));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPut("{id:int}/image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> Put(int id, [FromBody]ImageDTO imageDto)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Picture = Convert.FromBase64String(imageDto.Image);
            await _repository.UpdateCategoryAsync(category);

            if (await _repository.CommitAsync() > 0)
            {
                return Ok(_mapper.Map<CategoryDTO>(category));
            }

            return BadRequest();
        }
    }
}
