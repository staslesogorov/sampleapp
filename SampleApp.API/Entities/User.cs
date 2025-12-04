using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.API.Entities
{
    public class User : Base
    {
        public string Name { get; set; } = string.Empty;

        public required string Login {get; set;}
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}

    }
}