using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PontoColeta.Data;
using PontoColeta.Models;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using PontoColeta.ViewModels.CoordinateViewModels;
using PontoColeta.Repositories;

namespace PontoColeta.Controllers
{
    [ApiController]
    [Route("api/v1/coordinates")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CoordinateController : Controller
    {
        private readonly CoordinateRepository _repositoryCoordinate;
        private readonly CategoryRepository _repositoryCategory;

        public CoordinateController(CoordinateRepository repositoryCoordinate, CategoryRepository repositoryCategory)
        {
            _repositoryCoordinate = repositoryCoordinate;
            _repositoryCategory = repositoryCategory;
        }

        /// <summary>
        /// Get the list of all coordinates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<ListCoordinateViewModel>> Get()
        {
            var coordinates = _repositoryCoordinate.Get();                

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
        public ActionResult<List<ListCoordinateViewModel>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var coordinate = _repositoryCoordinate.Get(id);

            if (coordinate == null)
                return NotFound();
            
            return Ok(coordinate);
        }

        /// <summary>
        /// Get all coordinates, specifying a category code
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<ListCoordinateViewModel>> GetByCategory(int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest();

            var coordinates = _repositoryCoordinate.GetByCategory(categoryId);
            
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
        /// <param name="model">Coordinate data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ResultViewModel> Post(
            [FromBody] EditorCoordinateViewModel model
        )
        {
            bool isValidCategory = false;
            bool isInvalidPost = false;

            List<ListCoordinateViewModel> coordinates = _repositoryCoordinate.Get();
            List<Category> categories = _repositoryCategory.Get();

            foreach (var item in categories)
            {
                if (model.CategoryId == item.Id) 
                {
                    isValidCategory = true;
                    break;
                }
            }

            if (coordinates.Count > 0)
            {
                foreach (var item in coordinates)
                {
                    if (item.Latitude.Equals(model.Latitude) &&
                        item.Longitude.Equals(model.Longitude) &&
                        (item.CategoryId == model.CategoryId)
                    )
                    {
                        isInvalidPost = true;
                        break;
                    }
                }
            }

            model.Validate();
            if (model.Invalid)
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = model.Notifications
                });
            
             
            if (ModelState.IsValid && isValidCategory && !isInvalidPost)
            {
                var coordinate = new Coordinate();
                coordinate.Latitude = model.Latitude;
                coordinate.Longitude = model.Longitude;
                coordinate.NameOfPlace = model.NameOfPlace;
                coordinate.CategoryId = model.CategoryId;

                _repositoryCoordinate.Save(coordinate);

                return CreatedAtAction(
                    nameof(Post), 
                    new { Coordinate = model }
                );
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
        public ActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var coordinateModel = _repositoryCoordinate.Get(id);

            if (coordinateModel == null)
                return NotFound();

            _repositoryCoordinate.Delete(coordinateModel);

            return NoContent();
        }
    }
}
