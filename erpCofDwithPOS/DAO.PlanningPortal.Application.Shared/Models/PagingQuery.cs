namespace zero.Shared.Models
{
    public class PagingQuery
    {
        public string Sorting { get; set; } = string.Empty;
        public byte SortDirection { get; set; } = 1;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}