using System.Collections.Generic;

namespace MVCLibrary.DTO
{
    public class UserRoleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<UserDTO>? Users { get; set; }
    }
}
