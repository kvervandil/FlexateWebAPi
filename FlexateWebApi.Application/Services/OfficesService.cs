using AutoMapper;
using FlexateWebApi.Application.Dto;
using FlexateWebApi.Application.Dto.Offices;
using FlexateWebApi.Application.Interfaces;
using FlexateWebApi.Domain.Model;
using FlexateWebApi.Infrastructure.Entity.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Application.Services
{
    public class OfficesService : IOfficesService
    {
        private readonly IOfficesRepository _officesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OfficesService> _logger;

        public OfficesService(IOfficesRepository officesRepository, IMapper mapper, ILogger<OfficesService> logger)
        {
            _officesRepository = officesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResultDto<SingleOfficeDto>> GetOffices(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            var offices = await _officesRepository.GetOffices(pageSize, pageNo, searchString, cancellationToken);

            var noOfOffices = await _officesRepository.GetNoOfOffices(cancellationToken);

            var officesDto = _mapper.Map<List<SingleOfficeDto>>(offices);

            var officesForListDto = new PagedResultDto<SingleOfficeDto>()
            {
                Items = officesDto,
                CurrentPage = pageNo,
                Count = noOfOffices,
                PageSize = pageSize
            };
            return officesForListDto;
        }

        public async Task<SingleOfficeDto> GetOfficeById(int id, CancellationToken cancellationToken)
        {
            var office = await _officesRepository.GetOfficeById(id, cancellationToken);

            if (office == null || office.IsDeleted == true)
            {
                return null;
            }

            var officeDto = _mapper.Map<SingleOfficeDto>(office);

            return officeDto;
        }

        public async Task<int?> AddNewOffice(CreateOfficeDto officeDto, CancellationToken cancellationToken)
        {

            Office office = _mapper.Map<Office>(officeDto);
            
            if (string.IsNullOrEmpty(office.SpaceType))
            {
                return null;
            }

            int id = await _officesRepository.AddOffice(office, cancellationToken);

            return id;
        }

        public async Task<bool> DeleteOffice(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _officesRepository.DeleteOffice(id, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateOffice(int id, UpdateOfficeDto officeDto, CancellationToken cancellationToken)
        {
            if (officeDto == null)
            {
                return false;
            }

            Office office = _mapper.Map<Office>(officeDto);
            office.Id = id;

            /*Office office = new Office()
            {
                Id = id,
                SpaceType = officeDto.SpaceType,
                IsGroundFloor = officeDto.IsGroundFloor
            };*/

            return await _officesRepository.UpdateOffice(office, cancellationToken);
        }

        public async Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _officesRepository.UpdateWithDeletionFlag(id, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<OfficeForListDto>> GetAllOffices(CancellationToken cancellationToken)
        {
            var offices = await _officesRepository.GetAllOffices(cancellationToken);

            List<OfficeForListDto> officesDto = _mapper.Map<List<OfficeForListDto>>(offices);

            return officesDto;
        }

        public async Task<List<SingleOfficeDto>> GetOfficesByPersonId(int personId, CancellationToken cancellationToken)
        {
            var offices = await _officesRepository.GetOfficesByPersonid(personId, cancellationToken);

            var officesDto = _mapper.Map<List<SingleOfficeDto>>(offices);

            return officesDto;
        }
    }
}
