using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Ratings;
using BookATable.Application.UseCases.Queries.Cities;
using BookATable.Application.UseCases.Queries.MealCategoryRestaurants;
using BookATable.Application.UseCases.Queries.Ratings;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public RatingsController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        // GET: api/<RatingsController>
        [HttpGet]
        public IActionResult Find([FromQuery] SearchRatingDTO dto, [FromServices] IGetRatingsQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<RatingsController>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetRatingQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<RatingsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateRatingDTO dto, [FromServices] ICreateRatingCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<RatingsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRatingDTO dto, [FromServices] IUpdateRatingCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<RatingsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRatingCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
