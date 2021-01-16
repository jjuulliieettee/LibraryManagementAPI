using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Core.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
