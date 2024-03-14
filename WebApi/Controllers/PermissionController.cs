using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PermissionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet(Name = "GetPermissions")]
        public IActionResult GetPermissionsByEmployeeId()
        {
            var permissions = _unitOfWork.Permissions.GetPermissions();
            return Ok(permissions);
        }

        [HttpPost]
        public IActionResult RequestPermission(Permission permission)
        {
            permission.Guid = new Guid();

            _unitOfWork.Permissions.Add(permission);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpPatch]
        public IActionResult ModifyPermission(Permission _permission)
        {
            var permission = _unitOfWork.Permissions.GetPermissionById(_permission.Guid);

            if (_permission.EmployeeId != null) permission.EmployeeId = _permission.EmployeeId;
            if (_permission.PermissionTypeId != null) permission.PermissionTypeId = _permission.PermissionTypeId;

            permission.Guid = new Guid();

            _unitOfWork.Permissions.Update(permission);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}