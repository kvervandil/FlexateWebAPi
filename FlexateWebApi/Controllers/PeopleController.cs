using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Dto.People;
using FlexateWebApi.Application.Interfaces;
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
        private readonly ICarsService _carsService;
        private readonly IOfficesService _officesService;
        public readonly ILogger<PeopleController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        /// <param name="logger"></param>
        public PeopleController(IPeopleService personService, ILogger<PeopleController> logger, ICarsService carsService,
                                IOfficesService officesService)
        {
            _peopleService = personService;
            _carsService = carsService;
            _officesService = officesService;
            _logger = logger;
        }

        /// <summary>
        /// Get filtered people
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenericForListDto<PersonForListDto>>> Get(CancellationToken cancellationToken,
                                                              string searchString = "", int pageSize = 10,
                                                              int pageNo = 1)
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SinglePersonDto>> Get(int id, CancellationToken cancellationToken)
        {
            var person = await _peopleService.GetPersonById(id, cancellationToken);

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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreatePersonDto personDto, CancellationToken cancellationToken)
        {
            var id = await _peopleService.AddNewPerson(personDto, cancellationToken);

            if (id == null)
            {
                return BadRequest();
            }

            return Created($"api/person/{id}", id);
        }

        /// <summary>
        /// Update existing person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdatePersonDto personDto, CancellationToken cancellationToken)
        {
            var result = await _peopleService.UpdatePerson(id, personDto, cancellationToken);

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
        /// <param name="cancellationToken"></param>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Patch(int id, CancellationToken cancellationToken)
        {
            var result = await _peopleService.UpdateWithDeletionFlag(id, cancellationToken);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Delete existing person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var succeed = await _peopleService.DeletePerson(id, cancellationToken);
            if (succeed)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("cars,offices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetFiltered(CancellationToken cancellationToken)
        {
            var cars = _carsService.GetAllCars(cancellationToken);
            var offices = _officesService.GetAllOffices(cancellationToken);

            Task.WaitAll(cars, offices);

            var carsResult = cars;
            var officesResult = offices;

            if (carsResult == null || officesResult == null)
            {
                return NotFound();
            }

            var result = new object[] { carsResult, officesResult };

            return Ok(result);
        }
    }
}
