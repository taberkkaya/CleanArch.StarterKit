using CleanArch.StarterKit.Api.Abstractions;
using CleanArch.StarterKit.Application.Features.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.StarterKit.Api.Controllers
{
    /// <summary>
    /// Handles API requests related to products.
    /// </summary>

    public class ProductsController : ApiController
    {
        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetProductListQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
