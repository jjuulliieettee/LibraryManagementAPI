using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public bool IsBorrowed { get; set; }

        [Required]
        public int ReaderId { get; set; }

        public int? LibrarianId { get; set; }

        public int BookId { get; set; }
        
        public virtual Book Book { get; set; }

        public virtual User Reader { get; set; }

        public virtual User Librarian { get; set; }
    }
}
