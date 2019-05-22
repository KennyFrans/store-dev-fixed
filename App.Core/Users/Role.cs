using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Core.Users
{
    [Table("Role")]
    public class Role
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
