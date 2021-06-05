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
    [ApiController]
    [EnableCors("MyAllowSpecificOrigins")]
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
        /// 
        [Route("api/person")]
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
        /// Get person by id
        /// </summary>
        /// <returns></returns>
        // GET: PersonController/Details/5
        [Route("api/person/details/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Person Details(int id)
        {
            var person = _personService.GetPersonById(id);

            return person;
        }

        /// <summary>
        /// Create new person
        /// </summary>
        /// <returns></returns>
        // POST: PersonController/Create
        [Route("api/person/create/{name}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //GET: PersonController/Create
        public int Create(string name)
        {
            var person = _personService.AddNewPerson(name);

            //to test
            var people = _personService.GetCurrentPeopleList();

            return person.Id;
        }

        // POST: PersonController/Create
        /*[HttpPost]
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
        }*/

        // GET: PersonController/Edit/5
        /*public string Edit(int id)
        {
            return string.Empty;
        }*/

        // POST: PersonController/Edit/5

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateAntiForgeryToken]
        public int Edit(int id, Person person)
        {
            //todo
            var personToUpdate = _personService.GetPersonById(id);

            _personService.UpdatePerson(personToUpdate, person);

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
        /*public string Delete(int id)
        {
            return string.Empty;
        }*/

        // POST: PersonController/Delete/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public void Delete(int id, IFormCollection collection)
        {
            try
            {

            }
            catch
            {
                
            }
        }*/
    }
}
