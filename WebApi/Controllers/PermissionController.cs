using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProducerService _producerService;
        private readonly ISearchRepository<Permission> _searchRepository;

        public PermissionController(IUnitOfWork unitOfWork, IProducerService producerService, ISearchRepository<Permission> searchRepository)
        {
            _unitOfWork = unitOfWork;
            _producerService = producerService;
            _searchRepository = searchRepository;
        }

        [HttpGet(Name = "GetPermissions")]
        public async Task<IActionResult> GetPermissionsByEmployeeId()
        {
            var permissions = _unitOfWork.Permissions.GetPermissions();
            var message = JsonSerializer.Serialize(permissions);

            _producerService.Produce("permissionstopic", message);
            _searchRepository.Add(permissions);

            return Ok(permissions);
        }

        [HttpPost]
        public async Task<IActionResult> RequestPermission(Permission permission)
        {
            _unitOfWork.Permissions.Add(permission);
            _unitOfWork.Complete();

            var message = JsonSerializer.Serialize(permission);
            _producerService.Produce("RequestPermission", message);

            _searchRepository.Add((IEnumerable<Permission>)permission);

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> ModifyPermission(Permission permission)
        {
            _unitOfWork.Permissions.Update(permission);
            _unitOfWork.Complete();

            var message = JsonSerializer.Serialize(permission);
            _producerService.Produce("ModifyPermission", message);
            
            _searchRepository.Add((IEnumerable<Permission>)permission);

            return Ok();
        }
    }
}