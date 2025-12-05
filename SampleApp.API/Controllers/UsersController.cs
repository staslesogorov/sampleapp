using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleApp.API.Dto;
using SampleApp.API.Entities;
using SampleApp.API.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SampleApp.API.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private HMACSHA256 hmac = new HMACSHA256();
    private readonly IUserRepository _repo;
    private readonly ITokenService _token;
    
    public UsersController(IUserRepository repo, ITokenService token)
    {
        _repo = repo;
        _token = token;
    }


    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание нового пользователя",
        Description = "Создает нового пользователя",
        OperationId = "PostUsers"
    )]
    [SwaggerResponse(201, "Пользователь создан успешно", typeof(User))]
    public ActionResult CreateUser(UserDto userDto)
    {
        var user  = new User(){
                Login = userDto.Login,
                Token = _token.CreateToken(userDto.Login),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                PasswordSalt = hmac.Key,
                Name = ""
        };
        var validator = new UserValidator();
        var result = validator.Validate(user);
        
        if (!result.IsValid)
        {
            return BadRequest(result.Errors.First().ErrorMessage);
        }
        
        var createdUser = _repo.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }   
    
    [Authorize]
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
    [HttpPost("Login")]
    public ActionResult Login(UserDto userDto)
    {
        var user = _repo.FindUserByLogin(userDto.Login);
        return CheckPasswordHash(userDto, user);
    }
    private ActionResult CheckPasswordHash(UserDto userDto, User user)
        {

            using var hmac = new HMACSHA256(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized($"Неправильный пароль");
                }
            }

            return Ok(user);
        }
}
}