using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_MVC_Project.Models;

[Table("KhachHang")]
[Index("MaKh", Name = "KhachHang_maKH", IsUnique = true)]
public partial class KhachHang
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("maKH")]
    [StringLength(10)]
    [Unicode(false)]
    public string MaKh { get; set; } = null!;

    [Column("tenKH")]
    [StringLength(50)]
    public string TenKh { get; set; } = null!;

    public virtual ICollection<DatPhong> DatPhongs { get; } = new List<DatPhong>();
}
