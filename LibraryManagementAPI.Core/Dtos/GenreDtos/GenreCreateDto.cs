using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Core.Dtos
{
    public class GenreCreateDto
    {
        [Required, StringLength(120, MinimumLength = 2), RegularExpression("^[a-zA-Z]+")]
        public string Name { get; set; }
    }
}
