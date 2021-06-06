using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FlexateWebApi.Controllers
{    
    /// <summary>
    /// crud operations on Person
    /// </summary>
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public readonly ILogger<PersonController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        /// <param name="logger"></param>
        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        /// <summary>
        /// Get all peoples
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IList<Person>> Index()
        {
            _logger.LogInformation("we are in Index action");

            var model = _personService.GetAllPeople(10, 1, string.Empty);

            return Ok(model);
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: PersonController//5
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Person>Details(int id)
        {
            var person = _personService.GetPersonById(id);

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
            var person = _personService.AddNewPerson(personDto);

            return Created($"api/person/{person.Id}", person.Id);
        }

        // Patch: PersonController/Edit/5
        /// <summary>
        /// update existing person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("{id}")]
        public ActionResult Edit(int id, [FromBody]UpdatePersonDto personDto)
        {
            try
            {
                _personService.UpdatePerson(id, personDto);

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
                _personService.DeletePerson(id);

                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
