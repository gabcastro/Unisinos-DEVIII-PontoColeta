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
    public class CoordinateController : ControllerBase
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
                .Where(x => x.IdCategory == idCategory)
                .ToListAsync();
            
            if (coordinates.Count == 0)
                return NotFound();

            return Ok(coordinates);
        }
    }
}
