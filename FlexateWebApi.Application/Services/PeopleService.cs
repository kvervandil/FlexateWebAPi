using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain.Interfaces;
using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Application.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task<PeopleForListDto> GetPeople(int pageSize, int pageNo, string searchString,
                                                      CancellationToken cancellationToken)
        {
            var people = await _peopleRepository.GetPeople(pageSize, pageNo, searchString, cancellationToken);
            var noOfAllPeople = await _peopleRepository.GetNoOfPeople(cancellationToken);

            List<PersonForListDto> ListForPeople = new List<PersonForListDto>();

            foreach (var person in people)
            {
                ListForPeople.Add(new PersonForListDto() { Id = person.Id, Name = person.Name });
            }

            var model = new PeopleForListDto()
            {
                PeopleList = ListForPeople,
                CurrentPage = pageNo,
                PageSize = pageSize,
                SearchString = searchString,
                Count = noOfAllPeople
            };

            return model;
        }

        public async Task<SinglePersonDto> GetPersonById(int id, CancellationToken cancellationToken)
        {
            var person = await _peopleRepository.GetPersonById(id, cancellationToken);

            if (person == null || person.IsDeleted == true)
            {
                return null;
            }

            var personDto = new SinglePersonDto()
            {
                Name = person.Name,
                Address = person.Address,
                Age = person.Age
            };

            return personDto;
        }

        public async Task<int?> AddNewPerson(CreatePersonDto personDto, CancellationToken cancellationToken)
        {
            Person person = new Person()
            {
                Name = personDto.Name,
                Age = personDto.Age,
                Address = personDto.Address,
                IsDeleted = false
            };

            if (string.IsNullOrEmpty(person.Name)
                || string.IsNullOrEmpty(person.Address)
                || person.Age < 0)
            {
                return null;
            }

            int id = await _peopleRepository.AddPerson(person, cancellationToken);

            return person.Id;
        }

        public async Task<bool> UpdatePerson(int id, UpdatePersonDto personDto,
                                             CancellationToken cancellationToken)
        {
            if (personDto == null)
            {
                return false;
            }

            Person person = new Person()
            {
                Id = id,
                Address = personDto.Address,
                Age = personDto.Age,
                Name = personDto.Name
            };

            
            return await _peopleRepository.UpdatePerson(person, cancellationToken);
        }

        public async Task<bool> DeletePerson(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _peopleRepository.DeletePerson(id, cancellationToken);
            }
            catch (Exception)
            {
                return false;
            }            
        }

        public async Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _peopleRepository.UpdateWithDeletionFlag(id, cancellationToken);
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
