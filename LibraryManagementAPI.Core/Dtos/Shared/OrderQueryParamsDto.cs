using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Core.Dtos
{
    public class OrderQueryParamsDto
    {
        public string Reader { get; set; }
        public string Librarian { get; set; }
        public string PropertyNameToOrder { get; set; } = nameof(Order.IsBorrowed);
        public bool Ascending { get; set; } = true;
        public bool? IsBorrowed { get; set; }
        public string PropertyNameToGroup { get; set; }
    }
}
