using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Core.Dtos
{
    public class AuthorCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
