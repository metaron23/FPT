﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_MVC_Project.Models;

[Index("IdRole", Name = "Roles_idRole", IsUnique = true)]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idRole")]
    public int IdRole { get; set; }

    [Column("roleName")]
    [StringLength(50)]
    public string RoleName { get; set; } = null!;

    public virtual ICollection<TaiKhoan> TaiKhoans { get; } = new List<TaiKhoan>();
}
