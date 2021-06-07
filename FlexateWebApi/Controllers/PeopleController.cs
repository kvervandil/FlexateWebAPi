using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PeopleForListDto>> Get(string searchString = "", int pageSize = 10, int pageNo = 1)
        {
            _logger.LogInformation("we are in Index action");

            var model = await _peopleService.GetPeople(pageSize, pageNo, searchString);

            if (model.Count == 0)
            {
                return NotFound();
            }

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> Get(int id)
        {
            var person = _peopleService.GetPersonById(id);

            if (person == null)
            {
                return NotFound();
            }

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody]CreatePersonDto personDto)
        {
            var person = _peopleService.AddNewPerson(personDto);

            if (person == null)
            {
                return BadRequest();
            }

            return Created($"api/person/{person.Id}", person.Id);
        }

        // Put: PersonController/Edit/5
        /// <summary>
        /// Update existing person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public ActionResult Put(int id, [FromBody]UpdatePersonDto personDto)
        {
            var result = _peopleService.UpdatePerson(id, personDto);

            if (result)
            {
                return NoContent();
            }

            return NotFound();            
        }

        /// <summary>
        /// Update one property in person entity
        /// </summary>
        /// <param name="id"></param>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public ActionResult Patch(int id)
        {
            var result = _peopleService.UpdateWithDeleteFlag(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        // Delete: PersonController/Delete/5
        /// <summary>
        /// Delete existing person
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _peopleService.DeletePerson(id);

            if (result)
            {
                return NoContent();
            }
            return NotFound();            
        }
    }
}
