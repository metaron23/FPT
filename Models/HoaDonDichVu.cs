using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_MVC_Project.Models;

[Table("HoaDonDichVu")]
[Index("MaHddv", Name = "HoaDonDichVu_maHDDV", IsUnique = true)]
[Index("MaDp", Name = "UQ__HoaDonDi__7A3EF416B63E72E9", IsUnique = true)]
public partial class HoaDonDichVu
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("maHDDV")]
    [StringLength(10)]
    [Unicode(false)]
    public string MaHddv { get; set; } = null!;

    [Column("ngayHoaDon", TypeName = "datetime")]
    public DateTime NgayHoaDon { get; set; }

    [Column("maNV")]
    [StringLength(10)]
    [Unicode(false)]
    public string MaNv { get; set; } = null!;

    [Column("maDP")]
    [StringLength(10)]
    [Unicode(false)]
    public string MaDp { get; set; } = null!;

    public virtual DatPhong MaDpNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
