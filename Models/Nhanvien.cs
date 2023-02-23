using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_MVC_Project.Models;

[Table("NhanVien")]
[Index("MaNv", Name = "NhanVien_maNV", IsUnique = true)]
public partial class NhanVien
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("maNV")]
    [StringLength(10)]
    [Unicode(false)]
    public string MaNv { get; set; } = null!;

    [Column("tenNV")]
    [StringLength(50)]
    public string TenNv { get; set; } = null!;

    [Column("userName")]
    [StringLength(225)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    public virtual ICollection<HoaDonDatPhong> HoaDonDatPhongs { get; } = new List<HoaDonDatPhong>();

    public virtual ICollection<HoaDonDichVu> HoaDonDichVus { get; } = new List<HoaDonDichVu>();

    public virtual ICollection<LuongNhanVien> LuongNhanViens { get; } = new List<LuongNhanVien>();

    public virtual TaiKhoan UserNameNavigation { get; set; } = null!;
}
