using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PontoColeta.Data;
using PontoColeta.Models;

namespace PontoColeta.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CategoryController : Controller
    {

        /// <summary>
        /// Get the list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories
                .AsNoTracking()
                .ToListAsync();
                
            if (categories.Count == 0)
                return NotFound();

            return Ok(categories);
        }

        /// <summary>
        /// Get a category, specifying a code
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> GetById([FromServices] DataContext context, int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="context">DataContext</param>
        /// <param name="model">Category data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Post(
            [FromServices] DataContext context,
            [FromBody] Category model
        )
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { Category = model });
            }
            else 
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Remove a category, specifying a code
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(
            [FromServices] DataContext context,
            int id
        )
        {
            if (id <= 0)
                return BadRequest();

            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}