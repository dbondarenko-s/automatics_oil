using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS.Oil.BLL.Interfaces;
using AS.Oil.BLL.Models.DTO;
using AS.Oil.BLL.Services;
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

        #region Section Storage

        [HttpPost]
        public object DeleteStorage(long id)
        {
            QueueService.Add(new BLL.Models.QueueItem { Type = BLL.Enums.OperationType.Delete, Storage = new StorageDto { Id = id } });

            return Ok();
        }

        [HttpPost]
        public object EditStorage([FromBody] StorageViewModel model)
        {
            if (!ModelState.IsValid)
                return new { errors = ModelState.ToList() };

            var entity = new StorageDto
            {
                Id         = model.Id.Value,
                CategoryId = model.CategoryId.Value,
                MaxVolume  = model.MaxVolume.Value,
                MinVolume  = model.MinVolume.Value,
                Name       = model.Name,
                Volume     = model.Volume.Value
            };

            QueueService.Add(new BLL.Models.QueueItem { Type = BLL.Enums.OperationType.Update, Storage = entity });

            return Ok();
        }

        [HttpPost]
        public object CreateStorage([FromBody] StorageVM model)
        {
            if (!ModelState.IsValid)
                return new { errors = ModelState.ToList() };

            var entity = new StorageDto
            {
                Id             = 0,
                CategoryId     = model.CategoryId.Value,
                MaxVolume      = model.MaxVolume.Value,
                MinVolume      = model.MinVolume.Value,
                Name           = model.Name,
                CreateDateTime = DateTime.Now,
                IsDeleted      = false,
                Volume         = model.Volume.Value
            };

            QueueService.Add(new BLL.Models.QueueItem { Type = BLL.Enums.OperationType.Insert, Storage = entity });

            return Ok();
        }

        [HttpGet]
        public async Task<object> Storage(long id)
        {
            var data = await _storageService.GetStorageAsync(id);

            if (data != null)
            {
                var model = new StorageViewModel
                {
                    Id = data.Id,
                    CategoryId = data.CategoryId,
                    CreateDateTime = data.CreateDateTime,
                    IsDeleted = data.IsDeleted,
                    MaxVolume = data.MaxVolume,
                    MinVolume = data.MinVolume,
                    Name = data.Name,
                    Volume = data.Volume,
                    Category = new CategoryViewModel
                    {
                        Id = data.Category.Id,
                        Name = data.Category.Name
                    }
                };

                return model;
            }

            return NoContent();
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

        #endregion

        #region Section Category

        [HttpPost]
        public async Task<object> DeleteCategory(long id)
        {
            await _categoryService.DeleteAsync(id);

            return Ok();
        }

        [HttpPost]
        public async Task<object> EditCategory([FromBody] CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return new { errors = ModelState.ToList() };

            var entity = new CategoryDto { Id = model.Id.Value, Name = model.Name };

            await _categoryService.EditAsync(entity);

            return Ok();
        }

        [HttpPost]
        public async Task<object> CreateCategory([FromBody] CategoryVM model)
        {
            if (!ModelState.IsValid)
                return new { errors = ModelState.ToList() };

            var entity = new CategoryDto { Id = 0, Name = model.Name };

            await _categoryService.AddAsync(entity);

            return Ok();
        }

        [HttpGet]
        public async Task<object> Category(long id)
        {
            var data = await _categoryService.GetCategoryAsync(id);

            if (data != null)
            {
                var model = new CategoryViewModel
                {
                    Id = data.Id,
                    Name = data.Name
                };

                return model;
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<object> Categories()
        {
            var list = await _categoryService.GetCategoriesAsync();

            var data = list.Select(x => new CategoryViewModel { Id = x.Id, Name = x.Name });

            return new { Count = data.Count(), Data = data };
        }

        #endregion
    }
}