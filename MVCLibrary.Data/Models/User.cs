using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLibrary.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserRoleId { get; set; }

        public virtual UserRole UserRole { get; set; } = null!;
    }
}
