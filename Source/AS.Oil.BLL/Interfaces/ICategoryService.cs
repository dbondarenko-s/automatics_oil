using AS.Oil.BLL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AS.Oil.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryDto model);

        Task EditAsync(CategoryDto model);

        Task DeleteAsync(long id);

        Task<List<CategoryDto>> GetCategoriesAsync();

        Task<CategoryDto> GetCategoryAsync(long id);
    }
}
