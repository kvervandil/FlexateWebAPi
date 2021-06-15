using FlexateWebApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexateWebApi.Infrastructure.Entity.Interfaces
{
    public interface ICarsRepository
    {
        Task<List<Car>> GetCars(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken);
        Task<List<Car>> GetAllCars(CancellationToken cancellationToken);
        Task<Car> GetCarById(int id, CancellationToken cancellationToken);
        Task<int> GetNoOfCars(CancellationToken cancellationToken);
        Task<int> AddCar(Car car, CancellationToken cancellationToken);
        Task<bool> UpdateCar(Car car, CancellationToken cancellationToken);
        Task<bool> DeleteCar(int id, CancellationToken cancellationToken);
        Task<bool> UpdateWithDeletionFlag(int id, CancellationToken cancellationToken);
    }
}
