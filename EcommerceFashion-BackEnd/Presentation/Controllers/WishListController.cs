using Application.ProductService.Queries;
using Application.WishListService.Commands;
using Application.WishListService.Models;
using Application.WishListService.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WishListController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> WishListAddOrDeleteCommand([FromBody] WishListAddModel wishListAddModel)
        {
            try
            {
                var command = new WishListAddOrDeleteCommand(wishListAddModel);
                var result = await _mediator.Send(command);
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
