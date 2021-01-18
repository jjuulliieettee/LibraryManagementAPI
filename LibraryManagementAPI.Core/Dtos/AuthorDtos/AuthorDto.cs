using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Core.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }

        [Required, StringLength(120, MinimumLength = 2), RegularExpression("^[\\p{L} \\.'\\-\\s]+$")]
        public string Name { get; set; }
    }
}
