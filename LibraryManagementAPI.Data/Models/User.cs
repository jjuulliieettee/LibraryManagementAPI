using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagementAPI.Data.Enums;

namespace LibraryManagementAPI.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRole Role { get; set; }

        public virtual ICollection<Order> LibrarianOrders { get; set; }

        public virtual ICollection<Order> ReaderOrders { get; set; }
    }
}
