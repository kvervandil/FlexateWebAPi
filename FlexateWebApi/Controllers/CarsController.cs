using FlexateWebApi.Application.Dto.Cars;
using FlexateWebApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Controllers
{
    /// <summary>
    /// crud operations on cars
    /// </summary>
    [Route("api/cars")]
    public class CarsController : Controller
    {
        private readonly ICarsService _carsService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarsService carsService, ILogger<CarsController> logger)
        {
            _carsService = carsService;
            _logger = logger;
        }

        /// <summary>
        /// Get filtered cars
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarsForListDto>> Get(CancellationToken cancellationToken,
                                                              string searchString = "", int pageSize = 10,
                                                              int pageNo = 1)
        {
            _logger.LogInformation("we are in Index action");

            var model = await _carsService.GetCars(pageSize, pageNo, searchString, cancellationToken);

            if (model.Count == 0)
            {
                return NotFound();
            }

            return Ok(model);
        }

        /// <summary>
        /// Get car by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SingleCarDto>> Get(int id, CancellationToken cancellationToken)
        {
            var car = await _carsService.GetCarById(id, cancellationToken);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        /// <summary>
        /// Create new car
        /// </summary>
        /// <param name="carDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateCarDto carDto, CancellationToken cancellationToken)
        {
            var id = await _carsService.AddNewCar(carDto, cancellationToken);

            if (id == null)
            {
                return BadRequest();
            }

            return Created($"api/car/{id}", id);
        }

        /// <summary>
        /// Update existing car
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateCarDto carDto, CancellationToken cancellationToken)
        {
            var result = await _carsService.UpdatePerson(id, carDto, cancellationToken);

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
            var result = await _carsService.UpdateWithDeletionFlag(id, cancellationToken);
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
            var result = await _carsService.DeleteCar(id, cancellationToken);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
