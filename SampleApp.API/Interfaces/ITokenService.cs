using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string UserLogin);
    }
}