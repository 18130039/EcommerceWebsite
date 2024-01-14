using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWebsite.Models;

public partial class TDanhMucSp
{
    [Required(ErrorMessage = "Mã sản phẩm là bắt buộc.")]
    [DisplayName("Mã sản phẩm")]
    public string MaSp { get; set; } = null!;
    
    [DisplayName("Tên sản phẩm")]
    public string? TenSp { get; set; }
    
    [DisplayName("Mã chất liệu")]
    public string? MaChatLieu { get; set; }

    [DisplayName("Ngăn Laptop")]
    public string? NganLapTop { get; set; }

    public string? Model { get; set; }

    [DisplayName("Cân nặng")]
    public double? CanNang { get; set; }

    [DisplayName("Độ nới")]
    public double? DoNoi { get; set; }

    [DisplayName("Hãng sản xuất")]
    public string? MaHangSx { get; set; }

    [DisplayName("Nước sản xuất")]
    public string? MaNuocSx { get; set; }

    public string? MaDacTinh { get; set; }

    public string? Website { get; set; }

    [DisplayName("Thời gian bảo hành")]
    public double? ThoiGianBaoHanh { get; set; }

    [DisplayName("Giới thiệu sản phẩm")]
    public string? GioiThieuSp { get; set; }

    public double? ChietKhau { get; set; }

    [DisplayName("Loại sản phẩm")]
    public string? MaLoai { get; set; }

    [DisplayName("Đối tượng")]
    public string? MaDt { get; set; }

    [DisplayName("Ảnh")]
    public string? AnhDaiDien { get; set; }

    [DisplayName("Giá nhỏ nhất")]
    public decimal? GiaNhoNhat { get; set; }

    [DisplayName("Giá lớn nhất")]
    public decimal? GiaLonNhat { get; set; }

    public virtual TChatLieu? MaChatLieuNavigation { get; set; }

    public virtual TLoaiDt? MaDtNavigation { get; set; }

    public virtual THangSx? MaHangSxNavigation { get; set; }

    public virtual TLoaiSp? MaLoaiNavigation { get; set; }

    public virtual TQuocGium? MaNuocSxNavigation { get; set; }

    public virtual ICollection<TAnhSp> TAnhSps { get; } = new List<TAnhSp>();

    public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; } = new List<TChiTietSanPham>();
}
