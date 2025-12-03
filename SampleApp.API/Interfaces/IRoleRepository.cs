using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApp.API.Entities;

namespace SampleApp.API.Interfaces
{
    public interface IRoleRepository
{
    Role CreateRole(Role Role);
    List<Role> GetRoles();
    Role EditRole(Role role, int id);
    bool DeleteRole(int id);
    Role FindRoleById(int id);
}
}