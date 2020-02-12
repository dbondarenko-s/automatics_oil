using AS.Oil.BLL.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AS.Oil.BLL.Interfaces
{
    public interface IStorageService
    {
        Task AddAsync(StorageDto model);

        Task EditAsync(StorageDto model);

        Task DeleteAsync(long id);

        Task SetVolumeAsync(long id, double volume);

        Task<List<KeyValuePair<long, double>>> GetKeyValuePairIdAndVolumeAsync();

        Task<List<StorageDto>> GetStoragesAsync();

        Task<StorageDto> GetStorageAsync(long id);
    }
}
