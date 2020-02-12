using AS.Oil.BLL.Interfaces;
using AS.Oil.BLL.Models.DTO;
using AS.Oil.DAL.Entities;
using AS.Oil.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS.Oil.BLL.Services
{
    public class StorageService : IStorageService
    {
        private readonly ILogger<StorageService> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public StorageService(ILogger<StorageService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;

            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(StorageDto model)
        {
            var entity = new Storage()
            {
                Id             = 0,
                CategoryId     = model.CategoryId,
                CreateDateTime = DateTime.Now,
                IsDeleted      = false,
                MaxVolume      = model.MaxVolume,
                MinVolume      = model.MinVolume,
                Volume         = model.Volume,
                Name           = model.Name
            };

            Check(ref entity);

            try
            {
                _unitOfWork.Storages.Insert(entity);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"Успешно добавлена запись с ИД {entity.Id}");
            } 
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                var entity = await _unitOfWork.Storages.GetAll().Where(x => x.Id == id && !x.IsDeleted).SingleOrDefaultAsync();

                if (entity != null)
                {
                    entity.IsDeleted = true;

                    _unitOfWork.Storages.Update(entity);

                    await _unitOfWork.SaveChangesAsync();  
                }

                _logger.LogInformation($"Успешно удалена запись с ИД {id}");
            } 
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public async Task EditAsync(StorageDto model)
        {
            try
            {
                var entity = await _unitOfWork.Storages.GetAll().Where(x => x.Id == model.Id).SingleOrDefaultAsync();

                if (entity != null)
                {
                    entity.MaxVolume  = model.MaxVolume;
                    entity.MaxVolume  = model.MinVolume;
                    entity.Volume     = model.Volume;
                    entity.Name       = model.Name;
                    entity.CategoryId = model.CategoryId;

                    Check(ref entity);

                    _unitOfWork.Storages.Update(entity);

                    await _unitOfWork.SaveChangesAsync();

                    _logger.LogInformation($"Успешно отредактирована запись с ИД {model.Id}");
                }
                else
                {
                    _logger.LogInformation($"Не удалось найти запись резервуара в БД с ИД {model.Id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public async Task<List<KeyValuePair<long, double>>> GetKeyValuePairIdAndVolumeAsync()
        {
            var result = new List<KeyValuePair<long, double>>();

            try
            {
                var list = await _unitOfWork.Storages.GetAll().Select(x => x.Id).ToListAsync();

                var random = new Random();

                foreach(var id in list)
                {
                    result.Add(new KeyValuePair<long, double>(id, Convert.ToDouble(random.Next(-10, 10))));
                }
            } 
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return result;
        }

        public async Task SetVolumeAsync(long id, double volume)
        {
            try
            {
                var entity = await _unitOfWork.Storages.GetAll().Where(x => x.Id == id).SingleOrDefaultAsync();

                if (entity != null)
                {
                    entity.Volume = volume;

                    Check(ref entity);

                    _unitOfWork.Storages.Update(entity);

                    await _unitOfWork.SaveChangesAsync();

                    _logger.LogInformation($"Успешно отредактирована запись с ИД {id}");
                }
                else
                {
                    _logger.LogInformation($"Не удалось найти запись резервуара в БД с ИД {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        private void Check(ref Storage entity)
        {
            if (entity.Volume > entity.MaxVolume || entity.Volume < entity.MinVolume)
            {
                entity.Volume = entity.Volume > entity.MaxVolume ? entity.MaxVolume : (entity.Volume < entity.MinVolume ? entity.MinVolume : entity.Volume);

                _logger.LogWarning($"Текущее значение уровня заполнения резервуара выходит за границы, изменено на Volume = {entity.Volume}");
            }
        }
    }
}
