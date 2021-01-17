using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Core.Dtos
{
    public class BookEditDto
    {
        public int Id { get; set; }

        [Required, StringLength(255, MinimumLength = 1), RegularExpression("^[a-zA-Z]+")]
        public string Title { get; set; }

        [Required]
        public int YearOfPublishing { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        [StringLength(120, MinimumLength = 2), RegularExpression("^[a-zA-Z]+")]
        public string NewAuthorName { get; set; }

        [StringLength(120, MinimumLength = 2), RegularExpression("^[a-zA-Z]+")]
        public string NewGenreName { get; set; }
    }
}
