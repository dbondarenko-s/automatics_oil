using AS.Oil.BLL.Interfaces;
using AS.Oil.BLL.Models.DTO;
using AS.Oil.DAL.Entities;
using AS.Oil.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.Oil.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ILogger<CategoryService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;

            _unitOfWork = unitOfWork;
        }

        public Task AddAsync(CategoryDto model)
        {
            var entity = new Category { Id = 0, Name = model.Name };

            _unitOfWork.Categories.Insert(entity);

            return _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var domain = await _unitOfWork.Categories.GetAll().Where(x => x.Id == id && !x.Storages.Any()).SingleOrDefaultAsync();

            if (domain != null)
            {
                _unitOfWork.Categories.Delete(domain);

                await _unitOfWork.SaveChangesAsync();
            }
            else 
            {
                _logger.LogWarning($"Не удалось найти запись в БД с ИД {id} или категория используется");
            }
        }

        public async Task EditAsync(CategoryDto model)
        {
            var domain = await _unitOfWork.Categories.GetAll().Where(x => x.Id == model.Id).SingleOrDefaultAsync();

            if (domain != null)
            {
                domain.Name = model.Name;

                _unitOfWork.Categories.Update(domain);

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Не удалось найти запись в БД с ИД {model.Id}");
            }
        }

        public Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return _unitOfWork.Categories.GetAll().Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).AsNoTracking().ToListAsync();
        }

        public Task<CategoryDto> GetCategoryAsync(long id)
        {
            return _unitOfWork.Categories.GetAll().Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).Where(x => x.Id == id).AsNoTracking().SingleOrDefaultAsync();
        }
    }
}
