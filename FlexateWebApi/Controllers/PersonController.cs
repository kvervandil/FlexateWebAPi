using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexateWebApi.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        public readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService ,ILogger<PersonController> logger)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IList<Person> Index()
        {
            _logger.LogInformation("we are in Index action");

            var model = _personService.GetAllPeople(10, 1, string.Empty);

            return model.ToArray();
        }

        /// <summary>
        /// Get all peoples
        /// </summary>
        /// <returns></returns>
        // GET: PersonController/Details/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string Details(int id)
        {
            return string.Empty;
        }

        // GET: PersonController/Create
        public string Create()
        {
            return string.Empty;
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int Create(IFormCollection collection)
        {
            try
            {
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        // GET: PersonController/Edit/5
        public string Edit(int id)
        {
            return string.Empty;
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int Edit(int id, IFormCollection collection)
        {
            try
            {
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        // GET: PersonController/Delete/5
        public string Delete(int id)
        {
            return string.Empty;
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Delete(int id, IFormCollection collection)
        {
            try
            {

            }
            catch
            {
                
            }
        }
    }
}
