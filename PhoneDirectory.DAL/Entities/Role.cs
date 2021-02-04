using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneDirectory.DAL.Entities
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(13)]
        public string Name { get; set; }

        [StringLength(5)]
        public string Design { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
