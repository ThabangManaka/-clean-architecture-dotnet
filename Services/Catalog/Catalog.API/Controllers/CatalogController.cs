
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : Controller
    {

        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }


        //[HttpGet("GetAllProducts")]
        //public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        //{
        //    var query = new GetAllProductsQuery(catalogSpecParams);
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}

    }
}
