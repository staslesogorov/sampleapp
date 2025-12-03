using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleApp.API.Entities;
using SampleApp.API.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SampleApp.API.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _repo;
    
    public RolesController(IRoleRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание новой роли",
        Description = "Создает новую роль",
        OperationId = "PostRoles"
    )]
    [SwaggerResponse(201, "Роль создана успешно", typeof(Role))]
    public ActionResult CreateRole(Role Role)
    {
        var validator = new RoleValidator();
        var result = validator.Validate(Role);
        
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }
        
        var createdRole = _repo.CreateRole(Role);
        return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка ролей",
        Description = "Возвращает все роли",
        OperationId = "GetRoles"
    )]
    [SwaggerResponse(200, "Список ролей получен успешно", typeof(List<Role>))]
    public ActionResult GetRoles()
    {
        return Ok(_repo.GetRoles());
    }
    
    [HttpPut]
    [SwaggerOperation(
        Summary = "Изменение роли",
        Description = "Изменяет роль",
        OperationId = "PutRoles"
    )]
    [SwaggerResponse(200, "Роль изменена успешно", typeof(Role))]
    public ActionResult UpdateRole(Role Role)
    {
        return Ok(_repo.EditRole(Role, Role.Id));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Получение роли",
        Description = "Возвращает роль",
        OperationId = "GetRoles/id"
    )]
    [SwaggerResponse(200, "Роль получена", typeof(Role))]
    public ActionResult GetRoleById(int id)
    {
        return Ok(_repo.FindRoleById(id));
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Удаление роли",
        Description = "Удаляет роль",
        OperationId = "DeleteRoles"
    )]
    [SwaggerResponse(200, "Роль удалена", typeof(bool))]
    public ActionResult DeleteRole(int id)
    {
        return Ok(_repo.DeleteRole(id));
    }
}
}