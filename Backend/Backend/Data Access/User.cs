using Backend.Data_Access;

namespace Backend.Models
{
    public class User:Entity
    {
        #region Properties
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string LastName { get; set; }
            public int RoleId { get; set; }
            public virtual ICollection<Role> Role { get; set;}=new HashSet<Role>();
        #endregion

    }
}
