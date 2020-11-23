using System.Collections.Generic;
using AutoMapper;
using CoreMentoringApp.Data;
using CoreMentoringApp.WebSite.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMentoringApp.WebSite.Areas.Api.Controllers
{
    [Area("api")]
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
