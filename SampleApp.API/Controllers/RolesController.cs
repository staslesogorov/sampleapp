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
        Summary = "Получение списка пользователей",
        Description = "Возвращает все пользователей",
        OperationId = "GetProducts"
    )]
    [SwaggerResponse(200, "Список пользователей получен успешно", typeof(List<User>))]
    public ActionResult GetRoles()
    {
        return Ok(_repo.GetRoles());
    }
    
    [HttpPut]
    [SwaggerOperation(
        Summary = "Получение списка пользователей",
        Description = "Возвращает все пользователей",
        OperationId = "GetProducts"
    )]
    [SwaggerResponse(200, "Список пользователей получен успешно", typeof(List<User>))]
    public ActionResult UpdateRole(Role Role)
    {
        return Ok(_repo.EditRole(Role, Role.Id));
    }

    [HttpGet("{id}")]
    public ActionResult GetRoleById(int id)
    {
        return Ok(_repo.FindRoleById(id));
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteRole(int id)
    {
        return Ok(_repo.DeleteRole(id));
    }
}
}