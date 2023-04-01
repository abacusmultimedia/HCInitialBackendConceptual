using DAO.PlanningPortal.Domain.Common.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAO.PlanningPortal.Domain.Entities;

[Table(nameof(Language))]
public class Language : IBaseEntity<byte>
{
    public Language()
    {
        this.Translations = new HashSet<Translation>();
    }

    [Required]
    public byte Id { get; set; }

    /******************************************************/

    [Required, MaxLength(50)]
    public string Name { get; set; }

    public ICollection<Translation> Translations { get; set; }
}