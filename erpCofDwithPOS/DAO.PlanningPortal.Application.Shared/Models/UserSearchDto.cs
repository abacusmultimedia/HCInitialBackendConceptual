namespace zero.Shared.Models
{
    public class UserSearchDto : PagingQuery
    {
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
}