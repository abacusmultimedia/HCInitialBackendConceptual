using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace zero.Shared.Models.User
{
    public class UpdateRequestUserDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }
        public List<int> AreaIds { get; set; }
        public List<int> CityIds { get; set; }
    }
}