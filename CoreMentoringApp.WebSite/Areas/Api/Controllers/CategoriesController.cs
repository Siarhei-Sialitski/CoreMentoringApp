using System;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(_repository.GetCategories()));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _repository.GetCategoryById(id);
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
        public ActionResult<ProductDTO> Put(int id, [FromBody]ImageDTO imageDto)
        {
            var category = _repository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Picture = Convert.FromBase64String(imageDto.Image);
            _repository.UpdateCategory(category);

            if (_repository.Commit() > 0)
            {
                return Ok(_mapper.Map<CategoryDTO>(category));
            }

            return BadRequest();
        }
    }
}
