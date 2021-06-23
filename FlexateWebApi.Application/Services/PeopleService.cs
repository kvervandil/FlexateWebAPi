using AutoMapper;
using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Dto.People;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain.Model;
using FlexateWebApi.Infrastructure.Entity.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;
        private readonly ILogger<PeopleService> _logger;

        public PeopleService(IPeopleRepository peopleRepository, IMapper mapper, ILogger<PeopleService> logger)
        {
            _peopleRepository = peopleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResultDto<SinglePersonDto>> GetPeople(int pageSize, int pageNo, string searchString,
                                                      CancellationToken cancellationToken)
        {
            var people = await _peopleRepository.GetPeople(pageSize, pageNo, searchString, cancellationToken);
            var noOfAllPeople = await _peopleRepository.GetNoOfPeople(cancellationToken);

            var peopleForListDto = _mapper.Map<List<SinglePersonDto>>(people);

            var model = new PagedResultDto<SinglePersonDto>() { 
                Items = peopleForListDto,
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

            var personDto = _mapper.Map<SinglePersonDto>(person);

            return personDto;
        }

        public async Task<int?> AddNewPerson(CreatePersonDto personDto, CancellationToken cancellationToken)
        {
            /*Person person = new Person()
            {
                Name = personDto.Name,
                Age = personDto.Age,
                Address = personDto.Address,
                IsDeleted = false
            };*/

            Person person = _mapper.Map<Person>(personDto);

            if (string.IsNullOrEmpty(person.Name)
                || string.IsNullOrEmpty(person.Address)
                || person.Age < 0)
            {
                return null;
            }

            var id = await _peopleRepository.AddPerson(person, cancellationToken);

            return id;
        }

        public async Task<bool> UpdatePerson(int id, UpdatePersonDto personDto,
                                             CancellationToken cancellationToken)
        {
            if (personDto == null)
            {
                return false;
            }

            /*Person person = new Person()
            {
                Id = id,
                Address = personDto.Address,
                Age = personDto.Age,
                Name = personDto.Name
            };*/

            Person person = _mapper.Map<Person>(personDto);
            
            return await _peopleRepository.UpdatePerson(person, cancellationToken);
        }

        public async Task<bool> DeletePerson(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _peopleRepository.DeletePerson(id, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }            
        }

        public async Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _peopleRepository.UpdateWithDeletionFlag(id, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
