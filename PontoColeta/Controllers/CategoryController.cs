using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PontoColeta.Models;
using PontoColeta.Repositories;

namespace PontoColeta.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    [Produces(MediaTypeNames.Application.Json)]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get the list of all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Category>> Get()
        {
            var categories = _repository.Get();

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
        public ActionResult<Category> GetById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = _repository.Get(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="model">Category data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Category> Post(
            [FromBody] Category model
        )
        {
            if (ModelState.IsValid)
            {
                _repository.Save(model);
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
        public ActionResult Delete(
            int id
        )
        {
            if (id <= 0)
                return BadRequest();

            var category = _repository.Get(id);

            if (category == null)
                return NotFound();

            _repository.Delete(category);
            return NoContent();
        }
    }
}