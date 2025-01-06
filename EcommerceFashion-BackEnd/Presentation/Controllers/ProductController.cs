using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Common;
using MediatR;
using Application.ProductService.Queries;
using Newtonsoft.Json;
using Application.ProductService.Models;
using Application.ProductCategoryService.Queries;
using Application.ProductService.Models.RequestModels;

namespace Presentation.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("details")]
        public async Task<IActionResult> ProductGetDetailQuery(Guid id)
        {
            try
            {
                var query = new ProductGetDetailQuery(id);
                var result = await _mediator.Send(query);
                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("new-products")]
        public async Task<IActionResult> ProductGetNewQuery()
        {
            try
            {
                var query = new ProductGetNewQuery();
                var result = await _mediator.Send(query);
                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }      
        }
        [HttpGet("top-selling-products")]
        public async Task<IActionResult> ProductGetSaleQuery([FromQuery] ProductFilterModel productFilterModel)
        {
            try
            {
                var query = new ProductGetSaleQuery(productFilterModel);
                var result = await _mediator.Send(query);
                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpGet("products-shop/{shopId}")]
        public async Task<IActionResult> ProductGetByShopQuery(Guid shopId, [FromQuery] ProductFilterModel productFilterModel)
        {
            try
            {
                var query = new ProductGetByShopQuery(shopId, productFilterModel);
                var result = await _mediator.Send(query);
                if (result.Status)
                {
                    return Ok(result);
                }

                return BadRequest(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
      
    }
}
