using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int YearOfPublishing { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        public virtual Author Author { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
