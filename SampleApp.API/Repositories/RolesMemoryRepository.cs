using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApp.API.Entities;
using SampleApp.API.Interfaces;

namespace SampleApp.API.Repositories;

    public class RolesMemoryRepository : IRoleRepository
{
    public List<Role> Roles { get; set; } = new List<Role>();

    public Role CreateRole(Role Role)
    {
        Roles.Add(Role);
        return Role;
    }

    public bool DeleteRole(int id)
    {
        var result = FindRoleById(id);
        Roles.Remove(result);
        return true;
    }

    public Role EditRole(Role Role, int id)
    {
        var result = FindRoleById(id);
        result.Name = Role.Name;
        return result;
    }

    public Role FindRoleById(int id)
    {
        var result = Roles.Where(u => u.Id == id).FirstOrDefault();

        if (result == null)
        {
            throw new Exception($"Нет роли с id = {id}");
        }

        return result;
    }

    public List<Role> GetRoles()
    {
        return Roles;
    }
}

