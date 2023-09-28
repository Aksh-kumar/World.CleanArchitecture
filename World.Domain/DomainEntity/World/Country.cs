using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace World.Domain.DomainEntity.World;

[Table("Country")]
public partial class Country
{
    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(52)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(13)]
    [Unicode(false)]
    public string Continent { get; set; } = null!;

    [StringLength(26)]
    [Unicode(false)]
    public string Region { get; set; } = null!;

    [Column(TypeName = "numeric(10, 2)")]
    public decimal SurfaceArea { get; set; }

    public short? IndepYear { get; set; }

    public int Population { get; set; }

    [Column(TypeName = "numeric(3, 1)")]
    public decimal? LifeExpectancy { get; set; }

    [Column("GNP", TypeName = "numeric(10, 2)")]
    public decimal? Gnp { get; set; }

    [Column("GNPOld", TypeName = "numeric(10, 2)")]
    public decimal? Gnpold { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string LocalName { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string GovernmentForm { get; set; } = null!;

    [StringLength(60)]
    [Unicode(false)]
    public string? HeadOfState { get; set; }

    public int? Capital { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string Code2 { get; set; } = null!;

    [InverseProperty("CountryCodeNavigation")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [InverseProperty("CountryCodeNavigation")]
    public virtual ICollection<CountryLanguage> CountryLanguages { get; set; } = new List<CountryLanguage>();
}
