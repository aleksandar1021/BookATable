using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.Cities;
using BookATable.Application.UseCases.Queries.Cities;
using BookATable.Application.UseCases.Queries.MealCategories;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public CitiesController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<CitiesContoller>
        [HttpGet]
        public IActionResult Find([FromQuery] SearchCityDTO dto, [FromServices] IGetCitiesQuery query)
            => Ok(_commandHandler.HandleQuery(query, dto));

        // GET api/<CitiesContoller>/5
        
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetCityQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));

        // POST api/<CitiesContoller>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateCityDTO dto, [FromServices] ICreateCityCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        // PUT api/<CitiesContoller>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCityDTO dto, [FromServices] IUpdateCityCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<CitiesContoller>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCityCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
