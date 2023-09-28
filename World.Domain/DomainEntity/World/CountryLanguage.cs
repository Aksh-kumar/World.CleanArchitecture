using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace World.Domain.DomainEntity.World;

[PrimaryKey("CountryCode", "Language")]
[Table("CountryLanguage")]
public partial class CountryLanguage
{
    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string CountryCode { get; set; } = null!;

    [Key]
    [StringLength(30)]
    [Unicode(false)]
    public string Language { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string IsOfficial { get; set; } = null!;

    [Column(TypeName = "numeric(4, 1)")]
    public decimal Percentage { get; set; }

    [ForeignKey("CountryCode")]
    [InverseProperty("CountryLanguages")]
    public virtual Country CountryCodeNavigation { get; set; } = null!;
}
