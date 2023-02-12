using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
          

        public int? UserRoleId { get; set; }

        public virtual UserRoleDTO? UserRole { get; set; } = null!;
    }
}
