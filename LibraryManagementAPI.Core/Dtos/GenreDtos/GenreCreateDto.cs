using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Core.Dtos
{
    public class GenreCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
