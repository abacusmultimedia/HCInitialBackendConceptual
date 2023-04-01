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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class INV_MSD_Color  
    {
        [Key]
        public long Id { get; set; }
        public string Number { get; set; }
        public string NameEng { get; set; }
        public string ShortEng { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string NameAra { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string ShortAra { get; set; }
        public bool IsDefault { get; set; }
        public int StatusTypeId { get; set; }
        public int ActionUserId { get; set; }
        public System.DateTime ActionDate { get; set; }
        public int ApprvTypeId { get; set; }
        public int ApprvUserId { get; set; }
        public System.DateTime ApprvDate { get; set; }
    }

    public partial class  INV_MSD_Category 
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public string NameEng { get; set; }
        public string ShortEng { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string NameAra { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string ShortAra { get; set; }
        public bool IsDefault { get; set; }
        public int StatusTypeId { get; set; }
        public int ActionUserId { get; set; }
        public System.DateTime ActionDate { get; set; }
        public int ApprvTypeId { get; set; }
        public int ApprvUserId { get; set; }
        public System.DateTime ApprvDate { get; set; }

    }

}