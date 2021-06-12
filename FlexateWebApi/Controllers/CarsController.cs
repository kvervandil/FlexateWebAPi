using FlexateWebApi.Application.Dto.Cars;
using FlexateWebApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService _carsService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarsService carsService, ILogger<CarsController> logger)
        {
            _carsService = carsService;
            _logger = logger;
        }

        public async Task<ActionResult<CarsForListDto>> Get(CancellationToken cancellationToken, string searchString = "", int pageSize = 10,
                                                              int pageNo = 1)
        {
            return BadRequest();
        }
    }
}
