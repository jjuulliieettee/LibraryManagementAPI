using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
