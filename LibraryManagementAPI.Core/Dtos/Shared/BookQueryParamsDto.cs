namespace LibraryManagementAPI.Core.Dtos
{
    public class BookQueryParamsDto
    {
        public string Author { get; set; }
        public string Genre { get; set; }
        public int? Year { get; set; }
        public bool? IsAvailable { get; set; }
        public string PropertyNameToOrder { get; set; } = "Title";
        public bool Ascending { get; set; } = true;
        public string PropertyNameToGroup { get; set; }
    }
}
