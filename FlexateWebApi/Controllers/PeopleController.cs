using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
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
        public async Task<ActionResult<PeopleForListDto>> Get(string searchString = "", int pageSize = 10, int pageNo = 1, CancellationToken cancellationToken)
        {
            _logger.LogInformation("we are in Index action");

            var model = await _peopleService.GetPeople(pageSize, pageNo, searchString, cancellationToken);

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
        public async Task<ActionResult<SinglePersonDto>> Get(int id)
        {
            var person = await _peopleService.GetPersonById(id);

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
        public async Task<ActionResult> Create([FromBody]CreatePersonDto personDto)
        {
            var id = await _peopleService.AddNewPerson(personDto);

            if (id == null)
            {
                return BadRequest();
            }

            return Created($"api/person/{id}", id);
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
        public async Task<ActionResult> Put(int id, [FromBody]UpdatePersonDto personDto)
        {
            var result = await _peopleService.UpdatePerson(id, personDto);

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
        public async Task<ActionResult> Patch(int id)
        {
            var result = await _peopleService.UpdateWithDeleteFlag(id);
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
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _peopleService.DeletePerson(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
