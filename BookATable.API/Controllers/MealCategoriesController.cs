using BookATable.Application.DTO;
using BookATable.Application.UseCases.Commands.MealCategories;
using BookATable.Application.UseCases.Queries.MealCategories;
using BookATable.Application.UseCases.Queries.Users;
using BookATable.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookATable.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealCategoriesController : ControllerBase
    {
        private UseCaseHandler _commandHandler;
        public MealCategoriesController(UseCaseHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        // GET: api/<MealCategoriesCOntroller>
        [HttpGet]
        public IActionResult Search([FromQuery] SearchNamedEntityDTO search, [FromServices] IGetMealCategoriesQuery query)
            => Ok(_commandHandler.HandleQuery(query, search));



        // GET api/<MealCategoriesCOntroller>/5
        [HttpGet("{id}")]
        public IActionResult Find(int id, [FromServices] IGetMealCategoryQuery query)
            => Ok(_commandHandler.HandleQuery(query, id));



        // POST api/<MealCategoriesCOntroller>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateNamedEntity dto, [FromServices] ICreateMealCategoryCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }


        // PUT api/<MealCategoriesCOntroller>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateNamedEntity dto, [FromServices] IUpdateMealCategoryCommand cmd)
        {
            dto.Id = id;
            _commandHandler.HandleCommand(cmd, dto);
            return NoContent();
        }

        // DELETE api/<MealCategoriesCOntroller>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteMealCategoryCommand cmd)
        {
            _commandHandler.HandleCommand(cmd, id);
            return NoContent();
        }
    }
}
