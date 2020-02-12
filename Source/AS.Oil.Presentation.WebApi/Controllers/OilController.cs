using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS.Oil.BLL.Interfaces;
using AS.Oil.Presentation.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AS.Oil.Presentation.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OilController : ControllerBase
    {
        private readonly ILogger<OilController> _logger;
        private readonly IStorageService _storageService;
        private readonly ICategoryService _categoryService;

        public OilController(ILogger<OilController> logger, IStorageService storageService, ICategoryService categoryService)
        {
            _logger = logger;
            _storageService = storageService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<object> Categories()
        {
            var list = await _categoryService.GetCategoriesAsync();

            var data = list.Select(x => new CategoryViewModel { Id = x.Id, Name = x.Name });

            return new { Count = data.Count(), Data = data };
        }

        [HttpGet]
        public async Task<object> Storages()
        {
            var list = await _storageService.GetStoragesAsync();

            var data = list.Select(x => new StorageViewModel
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                CreateDateTime = x.CreateDateTime,
                IsDeleted = x.IsDeleted,
                MaxVolume = x.MaxVolume,
                MinVolume = x.MinVolume,
                Name = x.Name,
                Volume = x.Volume,
                Category = new CategoryViewModel
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                }
            }).ToList();

            return new { Count = data.Count, Data = data };
        } 
    }
}