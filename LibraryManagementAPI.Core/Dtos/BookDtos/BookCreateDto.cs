using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Core.Dtos
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int YearOfPublishing { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        public string NewAuthorName { get; set; }

        public string NewGenreName { get; set; }
    }
}
