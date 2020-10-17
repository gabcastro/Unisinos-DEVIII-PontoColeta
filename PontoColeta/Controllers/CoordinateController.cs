using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PontoColeta.Data;
using PontoColeta.Models;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace PontoColeta.Controllers
{
    [ApiController]
    [Route("api/v1/coordinates")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CoordinateController : Controller
    {
        /// <summary>
        /// Get the list of all coordinates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Coordinate>>> Get([FromServices] DataContext context)
        {
            var coordinates = await context.Coordinates
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();

            if (coordinates.Count == 0)
                return NotFound();

            return Ok(coordinates);
        }

        /// <summary>
        /// Get a coordinate, specifying a code
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Category>>> GetById([FromServices] DataContext context, int id)
        {
            if (id <= 0)
                return BadRequest();

            var coordinate = await context.Coordinates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (coordinate == null)
                return NotFound();
            
            return Ok(coordinate);
        }

        /// <summary>
        /// Get all coordinates, specifying a category code
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{idCategory:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Coordinate>>> GetByCategory([FromServices] DataContext context, int idCategory)
        {
            if (idCategory <= 0)
                return BadRequest();

            var coordinates = await context.Coordinates
                .Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.Category.Id == idCategory)
                .ToListAsync();
            
            if (coordinates.Count == 0)
                return NotFound();

            return Ok(coordinates);
        }

        /// <summary>
        /// Add a new coordinate
        /// Conditions: 
        ///     - Category must exist
        ///     - Latitude, longitude and idCategory cannot be the same in a new post
        /// </summary>
        /// <param name="context">DataContext</param>
        /// <param name="model">Coordinate data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Coordinate>> Post(
            [FromServices] DataContext context,
            [FromBody] Coordinate model
        )
        {
            bool isValidCategory = false;
            bool isInvalidPost = false;

            List<Coordinate> coordinates = context.Coordinates.ToList();

            foreach (var item in context.Categories.ToList())
            {
                if (model.Category.Id == item.Id) 
                {
                    isValidCategory = true;
                    break;
                }
            }

            if (coordinates.Count > 0)
            {
                foreach (var item in context.Coordinates.ToList())
                {
                    if (item.Latitude.Equals(model.Latitude) &&
                        item.Longitude.Equals(model.Longitude) &&
                        (item.Category.Id == model.Category.Id)
                    )
                    {
                        isInvalidPost = true;
                        break;
                    }
                }
            }
             
            if (ModelState.IsValid && isValidCategory && !isInvalidPost)
            {
                context.Coordinates.Add(model);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { Coordinate = model });
            }
            else 
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Remove a coordinate, specifying a code
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

            var coordinate = await context.Coordinates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (coordinate == null)
                return NotFound();

            context.Coordinates.Remove(coordinate);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
