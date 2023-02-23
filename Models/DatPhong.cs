using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_MVC_Project.Models;

[Table("DatPhong")]
[Index("MaDp", Name = "DatPhong_maDP", IsUnique = true)]
public partial class DatPhong
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("maDP")]
    [Display(Name = "Mã Đặt Phòng")]
    [Unicode(false)]
    public string MaDp { get; set; } = null!;

    [Column("soNguoi")]
    [Display(Name = "Số Người")]
    public int SoNguoi { get; set; }

    [Display(Name = "Ngày Bắt Đầu")]
    [Column("ngayBatDau", TypeName = "datetime")]
    public DateTime NgayBatDau { get; set; }

    [Display(Name = "Ngày Kết Thúc")]
    [Column("ngayKetThuc", TypeName = "datetime")]
    public DateTime NgayKetThuc { get; set; }

    [Column("maKH")]
    [Unicode(false)]
    public string MaKh { get; set; } = null!;

    [Column("maP")]
    [Unicode(false)]
    public string MaP { get; set; } = null!;

    public virtual ICollection<DatDichVu> DatDichVus { get; } = new List<DatDichVu>();

    public virtual HoaDonDatPhong? HoaDonDatPhong { get; set; }

    public virtual HoaDonDichVu? HoaDonDichVu { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; } = null!;

    public virtual Phong? MaPNavigation { get; set; } = null!;
}
