using System;

namespace LibraryManagementAPI.Core.Dtos
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int ReaderId { get; set; }
        public string Reader { get; set; }
        public int? LibrarianId { get; set; }
        public string Librarian { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsBorrowed { get; set; }

    }
}
