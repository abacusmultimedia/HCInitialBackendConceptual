//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POSERP.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ADM_ORG_Division_Master
    {
        public int Id { get; set; }
        public Nullable<int> GroupId { get; set; }
        public string NameEng { get; set; }
        public string ShortEng { get; set; }
        public string NameAra { get; set; }
        public string ShortAra { get; set; }
        public Nullable<int> StatusTypeId { get; set; }
        public Nullable<int> ActionUserId { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public Nullable<int> ApprvTypeId { get; set; }
        public Nullable<int> ApprvUserId { get; set; }
        public Nullable<System.DateTime> ApprvDate { get; set; }
    }
}
