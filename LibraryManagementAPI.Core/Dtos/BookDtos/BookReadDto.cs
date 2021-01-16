using System.Collections.Generic;

namespace LibraryManagementAPI.Core.Dtos
{
    public class BookReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int YearOfPublishing { get; set; }

        public bool IsAvailable { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }
    }
}
