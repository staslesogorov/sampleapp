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
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;
    
    public UsersController(IUserRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание нового пользователя",
        Description = "Создает нового пользователя",
        OperationId = "PostUsers"
    )]
    [SwaggerResponse(201, "Пользователь создан успешно", typeof(User))]
    public ActionResult CreateUser(User user)
    {
        var validator = new UserValidator();
        var result = validator.Validate(user);
        
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }
        
        var createdUser = _repo.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }   
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка пользователей",
        Description = "Возвращает всех пользователей",
        OperationId = "GetUsers"
    )]
    [SwaggerResponse(200, "Список пользователей получен успешно", typeof(List<User>))]
    public ActionResult GetUsers()
    {
        return Ok(_repo.GetUsers());
    }
    
    [HttpPut]
    [SwaggerOperation(
        Summary = "Изменение пользователя",
        Description = "Изменяет пользователя",
        OperationId = "PutUsers"
    )]
    [SwaggerResponse(200, "Пользователь изменен успешно", typeof(User))]
    public ActionResult UpdateUser(User user)
    {
        return Ok(_repo.EditUser(user, user.Id));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Получение пользователя",
        Description = "Возвращает пользователя",
        OperationId = "GetUsers/id"
    )]
    [SwaggerResponse(200, "Пользователь получен", typeof(User))]
    public ActionResult GetUserById(int id)
    {
        return Ok(_repo.FindUserById(id));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Удаление пользователя",
        Description = "Удаляет пользователя",
        OperationId = "DeleteUsers"
    )]
    [SwaggerResponse(200, "Пользователь удален", typeof(bool))]
    public ActionResult DeleteUser(int id)
    {
        return Ok(_repo.DeleteUser(id));
    }
}
}