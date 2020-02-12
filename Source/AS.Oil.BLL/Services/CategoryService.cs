using AS.Oil.BLL.Interfaces;
using AS.Oil.BLL.Models.DTO;
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
