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
    public class ProductsController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(_repository.GetProducts()));
        }
    }
}
