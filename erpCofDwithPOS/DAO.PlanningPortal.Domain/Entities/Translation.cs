using DAO.PlanningPortal.Domain.Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities;

[Table(nameof(Translation))]
public class Translation : IBaseEntity<int>
{
    [Required]
    public int Id { get; set; }

    /******************************************************/

    [Required]
    public byte LanguageId { get; set; }

    [Required, MaxLength(128)]
    public string Key { get; set; }

    [Required]
    public string Value { get; set; }

    public Language Language { get; set; }
}