﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zero.Shared.Common
{
    public class PersonalInfoDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string NTN { get; set; }
        public string BusinessName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
