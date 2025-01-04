using Application.FeedbackService.Commands;
using Application.FeedbackService.Models;
using Application.FeedbackService.Models.RequestModels;
using Application.FeedbackService.Queries;
using Application.ProductCategoryService.Queries;
using Application.ProductService.Models;
using Application.ProductService.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("feebacks-from-product/{id}")]
        public async Task<IActionResult> FeedbackGetByProductQuery(FeedbackGetByProductQuery feedbackGetByProductQuery)
        {
            try
            {
                var result = await _mediator.Send(feedbackGetByProductQuery);
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
        [HttpGet("feebacks-website")]
        public async Task<IActionResult> FeedbackGetForWebsiteQuery([FromQuery] FeedbackFilterModel feedbackFilterModel)
        {
            try
            {
                var query = new FeedbackGetForWebsiteQuery(feedbackFilterModel);
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
        [HttpGet("feebacks-from-shop/{shopId}")]
        public async Task<IActionResult> FeedbackGetByShopQuery(Guid shopId)
        {
            try
            {
                var query = new FeedbackGetByShopQuery(shopId);

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
        [HttpPost]
        public async Task<IActionResult> FeedbackAddForShopOrWebsiteCommand(FeedbackAddForShopOrWebsiteModel feedbackAddForShopOrWebsiteModel)
        {
            try
            {
                var command = new FeedbackAddForShopOrWebsiteCommand(feedbackAddForShopOrWebsiteModel);
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
