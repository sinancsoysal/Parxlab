using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Data.Dtos.User
{
    public record UpdateUserPasswordDto
    {
        public string Password { get; init; }
    }
}
