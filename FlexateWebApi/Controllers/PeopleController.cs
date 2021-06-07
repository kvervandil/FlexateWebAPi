using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlexateWebApi.Controllers
{    
    /// <summary>
    /// crud operations on Person
    /// </summary>
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        public readonly ILogger<PeopleController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        /// <param name="logger"></param>
        public PeopleController(IPeopleService personService, ILogger<PeopleController> logger)
        {
            _peopleService = personService;
            _logger = logger;
        }

        /// <summary>
        /// Get all people
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<Person>>> Get()
        {
            _logger.LogInformation("we are in Index action");

            var model = await _peopleService.GetAllPeople(10, 1, string.Empty);

            return Ok(model);
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        /// <param name="id"></param>
        /// 
        /// <returns></returns>
        // GET: PersonController/5
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Person> Get(int id)
        {
            var person = _peopleService.GetPersonById(id);

            return Ok(person);
        }

        /// <summary>
        /// Create new person
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        // POST: PersonController/Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Create([FromBody]CreatePersonDto personDto)
        {
            var person = _peopleService.AddNewPerson(personDto);

            return Created($"api/person/{person.Id}", person.Id);
        }

        // Patch: PersonController/Edit/5
        /// <summary>
        /// Update existing person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("{id}")]
        public ActionResult Update(int id, [FromBody]UpdatePersonDto personDto)
        {
            try
            {
                _peopleService.UpdatePerson(id, personDto);

                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }

        // Delete: PersonController/Delete/5
        /// <summary>
        /// Delete existing person
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _peopleService.DeletePerson(id);

                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
