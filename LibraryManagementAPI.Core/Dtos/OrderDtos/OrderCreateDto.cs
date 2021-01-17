using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Core.Dtos
{
    public class OrderCreateDto
    {
        [Required]
        public int BookId { get; set; }

        public int ReaderId { get; set; }
    }
}
