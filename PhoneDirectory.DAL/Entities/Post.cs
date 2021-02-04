using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneDirectory.DAL.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string NamePost { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<DivisionPost> DivisionPosts { get; set; }
    }
}
