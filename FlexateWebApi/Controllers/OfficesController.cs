using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Dto.Offices;
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
    [Route("api/offices")]
    public class OfficesController : Controller
    {
        IOfficesService _officesService;

        public ILogger _logger { get; }

        public OfficesController(IOfficesService officesService, ILogger<OfficesController> logger)
        {
            _officesService = officesService;
            _logger = logger;
        }


        /// <summary>
        /// Get filtered offices
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedResultDto<OfficeForListDto>>> Get(CancellationToken cancellationToken,
                                                              string searchString = "", int pageSize = 10,
                                                              int pageNo = 1)
        {
            _logger.LogInformation("we are in Get action");

            var model = await _officesService.GetOffices(pageSize, pageNo, searchString, cancellationToken);

            if (model.Count == 0)
            {
                return NotFound();
            }

            return Ok(model);
        }

        /// <summary>
        /// Get office by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SingleOfficeDto>> Get(int id, CancellationToken cancellationToken)
        {
            var office = await _officesService.GetOfficeById(id, cancellationToken);

            if (office == null)
            {
                return NotFound();
            }

            return Ok(office);
        }

        /// <summary>
        /// Create new office
        /// </summary>
        /// <param name="officeDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateOfficeDto officeDto, CancellationToken cancellationToken)
        {
            var id = await _officesService.AddNewOffice(officeDto, cancellationToken);

            if (id == null)
            {
                return BadRequest();
            }

            //return Created($"api/offices/{id}", id);
            return Created(nameof(Get), id);
        }

        /// <summary>
        /// Update existing office
        /// </summary>
        /// <param name="id"></param>
        /// <param name="officeDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateOfficeDto officeDto, CancellationToken cancellationToken)
        {
            var result = await _officesService.UpdateOffice(id, officeDto, cancellationToken);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        /// <summary>
        /// Update one property in office entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Patch(int id, CancellationToken cancellationToken)
        {
            var result = await _officesService.UpdateWithDeletionFlag(id, cancellationToken);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Delete existing office
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _officesService.DeleteOffice(id, cancellationToken);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
