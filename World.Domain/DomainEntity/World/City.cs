using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace World.Domain.DomainEntity.World;

[Table("City")]
public partial class City
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(35)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string CountryCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string District { get; set; } = null!;

    public int Population { get; set; }

    [ForeignKey("CountryCode")]
    [InverseProperty("Cities")]
    public virtual Country CountryCodeNavigation { get; set; } = null!;
}
