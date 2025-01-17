USE [master]
GO
/****** Object:  Database [QuanLyLinhKien]    Script Date: 10/12/2018 05:41:29 ******/
CREATE DATABASE [QuanLyLinhKien]

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyLinhKien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyLinhKien] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyLinhKien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyLinhKien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyLinhKien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyLinhKien] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyLinhKien] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyLinhKien] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyLinhKien] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyLinhKien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyLinhKien] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

EXEC sys.sp_db_vardecimal_storage_format N'QuanLyLinhKien', N'ON'
GO
USE [QuanLyLinhKien]
GO
/****** Object:  Table [dbo].[ChiTietDonDatHang]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonDatHang](
	[maDonDatHang] [nvarchar](20) NOT NULL,
	[maLinhKien] [nvarchar](20) NOT NULL,
	[soLuong] [int] NOT NULL,
	[giaBan] [float] NOT NULL,
	[mucGiamGia] [float] NOT NULL,
	[thanhTien] [float] NOT NULL,
 CONSTRAINT [pk_maDonDatHang_maHang] PRIMARY KEY CLUSTERED 
(
	[maDonDatHang] ASC,
	[maLinhKien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChiTietPhieuNhapKho]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuNhapKho](
	[maPhieuNhapKho] [nvarchar](20) NOT NULL,
	[maLinhKien] [nvarchar](20) NOT NULL,
	[soLuong] [int] NOT NULL,
	[giaMua] [float] NOT NULL,
	[thanhTien] [float] NOT NULL,
 CONSTRAINT [pk_maPhieuNhapKho_maLinhKien] PRIMARY KEY CLUSTERED 
(
	[maPhieuNhapKho] ASC,
	[maLinhKien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonDatHang]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonDatHang](
	[maDonDatHang] [nvarchar](20) NOT NULL,
	[maKhachHang] [nvarchar](20) NOT NULL,
	[maNhanVienThuNgan] [nvarchar](20) NOT NULL,
	[maNhanVienTuVan] [nvarchar](20) NOT NULL,
	[ngayLap] [date] NOT NULL,
	[tongTien] [float] NOT NULL,
	[noiNhanHang] [nvarchar](50) NOT NULL,
	[trangThai] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maDonDatHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[maKhachHang] [nvarchar](20) NOT NULL,
	[tenKhachHang] [nvarchar](50) NOT NULL,
	[soDienThoai] [nvarchar](15) NOT NULL,
	[eMail] [nvarchar](30) NOT NULL,
	[diaChi] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Linhkien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Linhkien](
	[maLinhKien] [nvarchar](20) NOT NULL,
	[tenLinhKien] [nvarchar](100) NOT NULL,
	[maLoai] [nvarchar](20) NOT NULL,
	[giaMua] [float] NOT NULL,
	[giaBan] [float] NOT NULL,
	[soLuong] [int] NOT NULL,
	[mucGiamGia] [float] NOT NULL,
	[maNhaCungCap] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maLinhKien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiLinhKien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiLinhKien](
	[maLoai] [nvarchar](20) NOT NULL,
	[tenLoai] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiNhanVien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiNhanVien](
	[maLoaiNhanVien] [nvarchar](20) NOT NULL,
	[tenLoaiNhanVien] [nvarchar](50) NOT NULL,
	[maPhanQuyen] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maLoaiNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[maNhaCungCap] [nvarchar](20) NOT NULL,
	[tenNhaCungCap] [nvarchar](100) NOT NULL,
	[soDienThoai] [nvarchar](15) NOT NULL,
	[eMail] [nvarchar](30) NOT NULL,
	[quocGia] [nvarchar](25) NOT NULL,
	[diaChi] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maNhaCungCap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[maNhanVien] [nvarchar](20) NOT NULL,
	[tenNhanVien] [nvarchar](50) NOT NULL,
	[cMND] [nvarchar](15) NOT NULL,
	[soDienThoai] [nvarchar](15) NOT NULL,
	[eMail] [nvarchar](30) NOT NULL,
	[diaChi] [nvarchar](50) NOT NULL,
	[trangThai] [nvarchar](20) NOT NULL,
	[maLoaiNhanVien] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhieuNhapKho]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuNhapKho](
	[maPhieuNhapKho] [nvarchar](20) NOT NULL,
	[maNhaCungCap] [nvarchar](20) NOT NULL,
	[maNhanVienThuKho] [nvarchar](20) NOT NULL,
	[maNhanVienKeToanKho] [nvarchar](20) NOT NULL,
	[ngayLap] [date] NOT NULL,
	[tongTien] [float] NOT NULL,
	[trangThai] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maPhieuNhapKho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[maTaiKhoan] [nvarchar](20) NOT NULL,
	[matKhau] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vw_InHoaDon]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create VIEW [dbo].[vw_InHoaDon]
AS
	SELECT a.maDonDatHang,NhanVienTuVan =  f.tenNhanVien,NhanVienThuNgan = e.tenNhanVien,d.tenKhachHang,d.soDienThoai,a.ngayLap,a.tongTien,a.noiNhanHang,
	c.tenLinhKien,b.soLuong,b.giaBan,b.mucGiamGia,thanhTien = SUM(b.giaBan*b.soLuong)
	FROM dbo.DonDatHang a JOIN dbo.ChiTietDonDatHang b 
	ON b.maDonDatHang = a.maDonDatHang JOIN dbo.Linhkien c
	ON c.maLinhKien = b.maLinhKien JOIN dbo.KhachHang d
	ON d.maKhachHang = a.maKhachHang JOIN dbo.NhanVien e 
	ON e.maNhanVien = a.maNhanVienThuNgan JOIN dbo.NhanVien f
	ON f.maNhanVien = a.maNhanVienTuVan
	GROUP BY a.maDonDatHang,f.tenNhanVien,e.tenNhanVien,d.tenKhachHang,d.soDienThoai,a.ngayLap,a.tongTien,a.noiNhanHang,c.tenLinhKien,b.soLuong,b.giaBan,b.mucGiamGia




GO
/****** Object:  View [dbo].[vw_inPhieuNhapKho]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_inPhieuNhapKho]
AS
	SELECT b.tenNhaCungCap,keToanKho = e.tenNhanVien,thuKho = f.tenNhanVien, a.maPhieuNhapKho, a.ngayLap, c.maLinhKien, d.tenLinhKien,c.soLuong,c.giaMua, c.thanhTien
	FROM dbo.PhieuNhapKho a JOIN dbo.NhaCungCap b
	ON b.maNhaCungCap = a.maNhaCungCap JOIN dbo.ChiTietPhieuNhapKho c
	ON c.maPhieuNhapKho = a.maPhieuNhapKho JOIN dbo.Linhkien d
	ON d.maLinhKien = c.maLinhKien JOIN dbo.NhanVien e
	ON e.maNhanVien = a.maNhanVienKeToanKho JOIN dbo.NhanVien f
	ON f.maNhanVien = a.maNhanVienThuKho
GO
/****** Object:  View [dbo].[vw_ThongKeDonDatHang]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_ThongKeDonDatHang]
AS
	SELECT MaDonDatHang = a.maDonDatHang,NhanVienTuVan = c.tenNhanVien,ThuNgan = b.tenNhanVien,KhachHang = d.tenKhachHang,TongDoanhThu = a.tongTien,a.ngayLap,ngayBatDau = N'asd', ngayKetThuc = N'asdas',loai = N'asdasd'
	FROM dbo.DonDatHang a JOIN dbo.NhanVien b
	ON b.maNhanVien = a.maNhanVienThuNgan JOIN dbo.NhanVien c
	ON c.maNhanVien = a.maNhanVienTuVan JOIN dbo.KhachHang d
	ON d.maKhachHang = a.maKhachHang
	WHERE a.trangThai = N'Đã thanh toán'



GO
/****** Object:  View [dbo].[vw_ThongKeKhachHang]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_ThongKeKhachHang]
AS
	SELECT MaKhachHang = a.maKhachHang,TenKhachHang = c.tenKhachHang,a.maDonDatHang,d.tenLinhKien,ngayMua = a.ngayLap,b.soLuong,b.giaBan,b.thanhTien,TongSoDonDatHang = (SELECT COUNT(*) FROM dbo.DonDatHang WHERE a.maKhachHang = maKhachHang), ngayBatDau = N'asd', NgayKetThuc = N'asd', loai = N'asdasd'
	FROM dbo.DonDatHang a JOIN dbo.ChiTietDonDatHang b
	ON b.maDonDatHang = a.maDonDatHang JOIN dbo.KhachHang c
	ON c.maKhachHang = a.maKhachHang JOIN dbo.Linhkien d
	ON d.maLinhKien = b.maLinhKien
	WHERE a.trangThai = N'Đã thanh toán'



GO
/****** Object:  View [dbo].[vw_ThongKeLinhKien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_ThongKeLinhKien]
AS
	SELECT a.maLinhKien,tenLinhKien,tenNhaCungCap,soLuongTon = a.soLuong,DaBan = c.soLuong,d.ngayLap,ngayBatDau = N'asd',ngayKetThuc = N'asd',loai = 'asdasd'
	FROM dbo.Linhkien a JOIN dbo.NhaCungCap b
	ON b.maNhaCungCap = a.maNhaCungCap JOIN dbo.ChiTietDonDatHang c
	ON c.maLinhKien = a.maLinhKien JOIN dbo.DonDatHang d
	ON d.maDonDatHang = c.maDonDatHang
	WHERE d.trangThai = N'Đã thanh toán'
GO
/****** Object:  View [dbo].[vw_ThongKeNhaCungCap]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_ThongKeNhaCungCap]
AS
	SELECT a.maNhaCungCap,b.tenNhaCungCap,a.maPhieuNhapKho,d.tenLinhKien,a.ngayLap,c.soLuong,c.giaMua,c.thanhTien,tongSoPhieuNhapKho = (SELECT COUNT(*) FROM dbo.PhieuNhapKho WHERE a.maNhaCungCap = maNhaCungCap), ngayBatDau = N'asd', NgayKetThuc = N'asd', loai = N'asdasd'
	FROM dbo.PhieuNhapKho a JOIN dbo.NhaCungCap b
	ON b.maNhaCungCap = a.maNhaCungCap JOIN dbo.ChiTietPhieuNhapKho c
	ON c.maPhieuNhapKho = a.maPhieuNhapKho JOIN dbo.Linhkien d
	ON d.maLinhKien = c.maLinhKien
	WHERE a.trangThai = N'Đã thanh toán'
GO
/****** Object:  View [dbo].[vw_ThongKeNhanVien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_ThongKeNhanVien]
AS
	SELECT c.maNhanVien,c.tenNhanVien,tongSoLuong = SUM(d.soLuong),a.tongTien,a.ngayLap,ngayBatDau = N'asd',ngayKetThuc = N'asd',loai = 'asdasd',tongDonDatHang = N''
	FROM dbo.DonDatHang a JOIN dbo.NhanVien c
	ON c.maNhanVien = a.maNhanVienTuVan JOIN dbo.ChiTietDonDatHang d
	ON d.maDonDatHang = a.maDonDatHang
	WHERE a.trangThai = N'Đã thanh toán'
	GROUP BY c.maNhanVien,a.maDonDatHang, c.tenNhanVien,a.tongTien,a.ngayLap


GO
/****** Object:  View [dbo].[vw_ThongKePhieuNhapKho]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_ThongKePhieuNhapKho]
AS
	SELECT a.maPhieuNhapKho,tenNhanVienKeToanKho = b.tenNhanVien, tenNhanVienThuKho = c.tenNhanVien, d.tenNhaCungCap, a.tongTien, a.ngayLap, ngayBatDau = N'asd', ngayKetThuc = N'asdas',loai = N'asdasd'
	FROM dbo.PhieuNhapKho a JOIN dbo.NhanVien b
	ON b.maNhanVien = a.maNhanVienKeToanKho JOIN dbo.NhanVien c
	ON c.maNhanVien = a.maNhanVienThuKho JOIN dbo.NhaCungCap d
	ON d.maNhaCungCap = a.maNhaCungCap
	WHERE a.trangThai = N'Đã thanh toán'



GO
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-1', N'LK-1', 50, 790000, 0, 3950000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-1', N'LK-10', 20, 5490000, 0, 10980000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-1', N'LK-11', 20, 1990000, 0, 3980000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-10', N'LK-2', 1, 1989000, 0, 198900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-11', N'LK-1', 1, 790000, 0, 79000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-11', N'LK-10', 1, 5490000, 0, 549000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-11', N'LK-11', 1, 1990000, 0, 199000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-11', N'LK-2', 1, 1989000, 0, 198900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-12', N'LK-2', 1, 1989000, 0, 198900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-12', N'LK-4', 1, 1880000, 0, 188000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-12', N'LK-6', 1, 1199000, 0, 119900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-12', N'LK-7', 1, 1093000, 0, 109300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-13', N'LK-2', 1, 1989000, 0, 198900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-13', N'LK-4', 1, 1880000, 0, 188000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-13', N'LK-5', 1, 320000, 0, 32000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-13', N'LK-6', 1, 1199000, 0, 119900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-13', N'LK-7', 1, 1093000, 0, 109300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-14', N'LK-6', 1, 1199000, 0, 119900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-14', N'LK-7', 3, 1093000, 0, 327900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-14', N'LK-8', 2, 2550000, 0, 510000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-14', N'LK-9', 5, 4330000, 0, 2165000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-15', N'LK-3', 1, 403000, 0, 40300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-15', N'LK-5', 1, 320000, 0, 32000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-15', N'LK-7', 2, 1093000, 0, 218600000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-15', N'LK-9', 5, 4330000, 0, 2165000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-16', N'LK-10', 2, 5490000, 0, 1098000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-16', N'LK-11', 6, 1990000, 0, 1194000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-16', N'LK-4', 3, 1880000, 0, 564000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-17', N'LK-11', 1, 1990000, 0, 199000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-17', N'LK-4', 1, 1880000, 0, 188000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-17', N'LK-6', 1, 1199000, 0, 119900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-18', N'LK-1', 1, 790000, 0, 79000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-18', N'LK-11', 1, 1990000, 0, 199000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-18', N'LK-2', 1, 1989000, 0, 198900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-18', N'LK-6', 1, 1199000, 0, 119900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-18', N'LK-7', 1, 1093000, 0, 109300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-18', N'LK-8', 1, 2550000, 0, 255000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-18', N'LK-9', 1, 4330000, 0, 433000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-19', N'LK-2', 1, 1989000, 0, 198900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-19', N'LK-3', 1, 403000, 0, 40300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-19', N'LK-4', 1, 1880000, 0, 188000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-19', N'LK-6', 1, 1199000, 0, 119900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-2', N'LK-1', 2, 790000, 0, 158000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-2', N'LK-10', 3, 5490000, 0, 1647000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-2', N'LK-8', 4, 2550000, 0, 1020000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-2', N'LK-9', 5, 4330000, 0, 2165000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-20', N'LK-1', 1, 790000, 0, 79000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-20', N'LK-11', 1, 1990000, 0, 199000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-20', N'LK-2', 1, 1989000, 0, 198900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-20', N'LK-4', 1, 1880000, 0, 188000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-21', N'LK-3', 1, 403000, 0, 40300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-21', N'LK-5', 1, 320000, 0, 32000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-21', N'LK-7', 1, 1093000, 0, 109300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-21', N'LK-9', 1, 4330000, 0, 433000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-22', N'LK-1', 1, 790000, 0, 79000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-22', N'LK-10', 1, 5490000, 0, 549000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-22', N'LK-11', 1, 1990000, 0, 199000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-22', N'LK-2', 1, 1989000, 0, 198900000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-22', N'LK-3', 1, 403000, 0, 40300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-23', N'LK-1', 1, 790000, 0, 79000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-23', N'LK-11', 1, 1990000, 0, 199000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-23', N'LK-4', 1, 1880000, 0, 188000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-23', N'LK-5', 1, 320000, 0, 32000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-23', N'LK-7', 1, 1093000, 0, 109300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-23', N'LK-8', 1, 2550000, 0, 255000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-24', N'LK-10', 295, 5490000, 0, 161955000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-3', N'LK-10', 2, 5490000, 0, 1098000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-3', N'LK-11', 2, 1990000, 0, 398000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-3', N'LK-2', 2, 1989000, 0, 397800000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-3', N'LK-4', 2, 1880000, 0, 376000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-3', N'LK-5', 2, 320000, 0, 64000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-4', N'LK-1', 20, 790000, 0, 1580000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-4', N'LK-6', 5, 1199000, 0, 599500000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-4', N'LK-8', 3, 2550000, 0, 765000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-4', N'LK-9', 12, 4330000, 0, 5196000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-5', N'LK-1', 12, 790000, 0, 948000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-5', N'LK-11', 5, 1990000, 0, 995000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-5', N'LK-5', 7, 320000, 0, 224000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-5', N'LK-7', 2, 1093000, 0, 218600000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-6', N'LK-1', 2, 790000, 0, 158000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-6', N'LK-10', 3, 5490000, 0, 1647000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-6', N'LK-2', 4, 1989000, 0, 795600000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-6', N'LK-4', 5, 1880000, 0, 940000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-6', N'LK-7', 1, 1093000, 0, 109300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-7', N'LK-10', 1, 5490000, 0, 549000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-7', N'LK-3', 4, 403000, 0, 161200000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-7', N'LK-5', 3, 320000, 0, 96000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-7', N'LK-8', 2, 2550000, 0, 510000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-8', N'LK-11', 1, 1990000, 0, 199000000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-8', N'LK-3', 1, 403000, 0, 40300000)
INSERT [dbo].[ChiTietDonDatHang] ([maDonDatHang], [maLinhKien], [soLuong], [giaBan], [mucGiamGia], [thanhTien]) VALUES (N'DDH-9', N'LK-3', 1, 403000, 0, 40300000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-1', N'LK-1', 300, 700000, 237000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-1', N'LK-2', 200, 1800000, 397800000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-1', N'LK-3', 200, 300000, 80600000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-2', N'LK-4', 100, 1700000, 188000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-2', N'LK-5', 150, 280000, 48000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-2', N'LK-6', 120, 1000000, 143880000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-3', N'LK-7', 200, 950000, 218600000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-3', N'LK-8', 150, 2200000, 382500000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-3', N'LK-9', 250, 3800000, 1082500000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-4', N'LK-10', 130, 5000000, 713700000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-4', N'LK-11', 100, 1700000, 199000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-5', N'LK-1', 200, 700000, 158000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-5', N'LK-2', 100, 1800000, 198900000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-5', N'LK-3', 100, 300000, 40300000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-6', N'LK-13', 250, 700000, 225000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-6', N'LK-16', 360, 1500000, 720000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-7', N'LK-15', 500, 600000, 400000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-8', N'LK-18', 350, 1800000, 700000000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-9', N'LK-10', 370, 5000000, 2031300000)
INSERT [dbo].[ChiTietPhieuNhapKho] ([maPhieuNhapKho], [maLinhKien], [soLuong], [giaMua], [thanhTien]) VALUES (N'PNK-9', N'LK-12', 280, 500000, 168000000)
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-1', N'KH-1', N'NV-3', N'NV-2', CAST(N'2018-10-08' AS Date), 189100000, N'15 Đ.Tiên Trí Q.2', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-10', N'KH-1', N'NV-12', N'NV-2', CAST(N'2018-11-25' AS Date), 1989000, N'15 Đ.Tiên Trí Q.2', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-11', N'KH-8', N'NV-12', N'NV-8', CAST(N'2018-11-25' AS Date), 10259000, N'22 Đ.Đồng Khởi Q.1', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-12', N'KH-9', N'NV-12', N'NV-7', CAST(N'2018-11-25' AS Date), 6161000, N'13 Đ.Kiên Mỹ Q.12', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-13', N'KH-10', N'NV-12', N'NV-8', CAST(N'2018-12-01' AS Date), 6481000, N'235 Đ.Phan Tuấn Minh Q.5', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-14', N'KH-11', N'NV-3', N'NV-8', CAST(N'2018-12-01' AS Date), 31228000, N'17/685 Đ.Lê Đức Thọ Q.Gò Vấp', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-15', N'KH-12', N'NV-3', N'NV-7', CAST(N'2018-12-01' AS Date), 24559000, N'185A Đ.Thống Nhất Q.Gò Vấp', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-16', N'KH-13', N'NV-3', N'NV-7', CAST(N'2018-12-01' AS Date), 28560000, N'12 Đ.Lê Văn Thọ Q.Gò Vấp', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-17', N'KH-14', N'NV-3', N'NV-6', CAST(N'2018-12-03' AS Date), 5069000, N'18A Đ.Duy Tôn Q.Gò Vấp', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-18', N'KH-15', N'NV-3', N'NV-6', CAST(N'2018-12-03' AS Date), 13941000, N'15 Đ.Lê Thánh Tôn Q.1', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-19', N'KH-16', N'NV-12', N'NV-8', CAST(N'2018-12-03' AS Date), 5471000, N'18 Đ.Nguyễn Cư Trinh Q.2', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-2', N'KH-2', N'NV-3', N'NV-6', CAST(N'2018-10-08' AS Date), 49900000, N'13 Đ.Khởi Đào Q.1', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-20', N'KH-17', N'NV-12', N'NV-2', CAST(N'2018-12-08' AS Date), 6649000, N'16 Đ.Trần Trọng Minh Q.3', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-21', N'KH-18', N'NV-12', N'NV-2', CAST(N'2018-12-08' AS Date), 6146000, N'15 Đ.Trần Chí Dũng Q.5', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-22', N'KH-19', N'NV-12', N'NV-2', CAST(N'2018-12-08' AS Date), 10662000, N'98 Đ.Đồng Khởi Q.5', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-23', N'KH-20', N'NV-12', N'NV-2', CAST(N'2018-12-08' AS Date), 8623000, N'156 Đ.Dũng Trí Q.6', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-24', N'KH-1', N'NV-1', N'NV-2', CAST(N'2018-12-10' AS Date), 1619550000, N'15 Đ.Tiên Trí Q.2', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-3', N'KH-3', N'NV-3', N'NV-8', CAST(N'2018-10-08' AS Date), 23338000, N'22 Đ.Hà Minh Giáp Q.Gò vấp', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-4', N'KH-4', N'NV-3', N'NV-7', CAST(N'2018-10-15' AS Date), 81405000, N'55 Đ.Trinh Minh Q.3', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-5', N'KH-5', N'NV-3', N'NV-2', CAST(N'2018-10-15' AS Date), 23856000, N'125 Đ.Minh Mạng Q.5', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-6', N'KH-6', N'NV-12', N'NV-6', CAST(N'2018-10-15' AS Date), 36499000, N'115 Đ.Trung Trinh Q.4', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-7', N'KH-7', N'NV-12', N'NV-2', CAST(N'2018-11-08' AS Date), 13162000, N'55 Đ.Paster Q.1', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-8', N'KH-7', N'NV-12', N'NV-8', CAST(N'2018-11-08' AS Date), 2393000, N'55 Đ.Paster Q.1', N'Đã thanh toán')
INSERT [dbo].[DonDatHang] ([maDonDatHang], [maKhachHang], [maNhanVienThuNgan], [maNhanVienTuVan], [ngayLap], [tongTien], [noiNhanHang], [trangThai]) VALUES (N'DDH-9', N'KH-3', N'NV-12', N'NV-6', CAST(N'2018-11-08' AS Date), 403000, N'22 Đ.Hà Minh Giáp Q.Gò vấp', N'Đã thanh toán')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-1', N'Nguyễn Lê Nam Anh', N'0123546852', N'namanh@gmail.com', N'15 Đ.Tiên Trí Q.2')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-10', N'Minh Hào', N'0128989685', N'minhhao@gmail.com', N'235 Đ.Phan Tuấn Minh Q.5')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-11', N'Trần Minh Tú', N'01625358245', N'tu@gmail.com', N'17/685 Đ.Lê Đức Thọ Q.Gò Vấp')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-12', N'Minh Tâm', N'01523485268', N'tam@gmail.com', N'185A Đ.Thống Nhất Q.Gò Vấp')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-13', N'Đào Anh Duy', N'01253486987', N'duy@gmail.com', N'12 Đ.Lê Văn Thọ Q.Gò Vấp')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-14', N'Việt Trung', N'01524785469', N'trung@gmail.com', N'18A Đ.Duy Tôn Q.Gò Vấp')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-15', N'Trần Minh Tiến', N'04526358524', N'minhtien@gmail.com', N'15 Đ.Lê Thánh Tôn Q.1')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-16', N'Thu Thảo', N'08546257382', N'thuthao@gmail.com', N'18 Đ.Nguyễn Cư Trinh Q.2')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-17', N'Trần Phú', N'04526312345', N'tranphu@gmail.com', N'16 Đ.Trần Trọng Minh Q.3')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-18', N'Trần Minh Tú', N'05648257985', N'minhgtu@gmail.com', N'15 Đ.Trần Chí Dũng Q.5')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-19', N'Trần Đăng Thiên', N'05462235215', N'dangthiên@gmail.com', N'98 Đ.Đồng Khởi Q.5')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-2', N'Lâm Hồng Huy', N'0125364789', N'huy@gmail.com', N'13 Đ.Khởi Đào Q.1')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-20', N'Thiên Thu', N'01556632485', N'thienthu@gmail.com', N'156 Đ.Dũng Trí Q.6')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-3', N'Nguyễn Tuấn Kiệt', N'0152478654', N'tuankiet@gmail.com', N'22 Đ.Hà Minh Giáp Q.Gò vấp')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-4', N'Phan Tuấn Tài', N'0124536852', N'tuantai@gmail.com', N'55 Đ.Trinh Minh Q.3')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-5', N'Quang Vinh', N'0524368547', N'quangvinh@gmail.com', N'125 Đ.Minh Mạng Q.5')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-6', N'Lê Hoàng', N'0623154789', N'hoang@gmail.com', N'115 Đ.Trung Trinh Q.4')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-7', N'Trần Đăng Khoa', N'0652348521', N'dangkhoa@gmail.com', N'55 Đ.Paster Q.1')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-8', N'Trần Minh Giàu', N'0192468523', N'giau@gmail.com', N'22 Đ.Đồng Khởi Q.1')
INSERT [dbo].[KhachHang] ([maKhachHang], [tenKhachHang], [soDienThoai], [eMail], [diaChi]) VALUES (N'KH-9', N'Trần Minh Nghĩa', N'0163235368', N'nghia@gmail.com', N'13 Đ.Kiên Mỹ Q.12')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-1', N'Main Gigabyte H81M-DS2', N'LLK-1', 700000, 790000, 609, 0, N'NCC-1')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-10', N'Asrock X299 Extreme4', N'LLK-1', 5000000, 5490000, 370, 0, N'NCC-4')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-11', N'GIGABYTE™ B250M-Gaming 3', N'LLK-1', 1700000, 1990000, 254, 0, N'NCC-4')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-12', N'Chuột Lazer AS4', N'LLK-8', 500000, 600000, 280, 0, N'NCC-4')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-13', N'CPU Dia Join 4S', N'LLK-2', 700000, 900000, 250, 0, N'NCC-1')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-14', N'KeyBoard Lazer VIP join', N'LLK-10', 1200000, 1400000, 0, 0, N'NCC-2')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-15', N'Dialog RAM 8G', N'LLK-3', 600000, 800000, 500, 0, N'NCC-2')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-16', N'Screen Siz 12 inch', N'LLK-6', 1500000, 2000000, 360, 0, N'NCC-1')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-17', N'Vega 12MS', N'LLK-4', 520000, 700000, 0, 0, N'NCC-3')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-18', N'Screen 21 inch', N'LLK-6', 1800000, 2000000, 350, 0, N'NCC-3')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-19', N'Chuột Tianan VSS', N'LLK-8', 790000, 900000, 0, 0, N'NCC-1')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-2', N'Main ASUS STRIX B250H GAMING', N'LLK-1', 1800000, 1989000, 483, 0, N'NCC-1')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-3', N'Main ASUS H81M-D', N'LLK-1', 300000, 403000, 489, 0, N'NCC-1')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-4', N'Main Gigabyte B360M-D2V', N'LLK-1', 1700000, 1880000, 278, 0, N'NCC-2')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-5', N'Main Asus H81 M-E', N'LLK-1', 280000, 320000, 332, 0, N'NCC-2')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-6', N'Main Asrock H110M-DVS R3.0', N'LLK-1', 1000000, 1199000, 304, 0, N'NCC-2')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-7', N'Main Gigabyte H110 M_DS2 DDR4 Socket 1151', N'LLK-1', 950000, 1093000, 373, 0, N'NCC-3')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-8', N'Main GIGABYTE B250M D3H (GA-B250M-D3H)', N'LLK-1', 2200000, 2550000, 329, 0, N'NCC-3')
INSERT [dbo].[Linhkien] ([maLinhKien], [tenLinhKien], [maLoai], [giaMua], [giaBan], [soLuong], [mucGiamGia], [maNhaCungCap]) VALUES (N'LK-9', N'ASUS PRIME-X370-PRO', N'LLK-1', 3800000, 4330000, 414, 0, N'NCC-3')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-1', N'Mainboard')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-10', N'keyboard')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-2', N'CPU')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-3', N'Ram')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-4', N'Thẻ đồ hoạ')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-5', N'Ổ cứng')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-6', N'Màn hình')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-7', N'Bàn phím')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-8', N'chuột')
INSERT [dbo].[LoaiLinhKien] ([maLoai], [tenLoai]) VALUES (N'LLK-9', N'Bộ nguồn')
INSERT [dbo].[LoaiNhanVien] ([maLoaiNhanVien], [tenLoaiNhanVien], [maPhanQuyen]) VALUES (N'LNV-1', N'Quản lý', N'11111111111111111111111111111111')
INSERT [dbo].[LoaiNhanVien] ([maLoaiNhanVien], [tenLoaiNhanVien], [maPhanQuyen]) VALUES (N'LNV-2', N'Nhân viên tư vấn', N'00000000000000000000010000000000')
INSERT [dbo].[LoaiNhanVien] ([maLoaiNhanVien], [tenLoaiNhanVien], [maPhanQuyen]) VALUES (N'LNV-3', N'Thu ngân', N'11000011010000001101000000101000')
INSERT [dbo].[LoaiNhanVien] ([maLoaiNhanVien], [tenLoaiNhanVien], [maPhanQuyen]) VALUES (N'LNV-4', N'Thủ kho', N'00000100000000000000100011000100')
INSERT [dbo].[LoaiNhanVien] ([maLoaiNhanVien], [tenLoaiNhanVien], [maPhanQuyen]) VALUES (N'LNV-5', N'Kế toán kho', N'00110100000000110000101011000111')
INSERT [dbo].[NhaCungCap] ([maNhaCungCap], [tenNhaCungCap], [soDienThoai], [eMail], [quocGia], [diaChi]) VALUES (N'NCC-1', N'CTy Kỹ Thuật Hưng Thịnh', N'02873001986', N'nguyenlk@hungthinh.com', N'Việt Nam', N'864 Cách Mạng Tháng Tám, P. 5, Q. Tân Bình, Tp. Hồ Chí Minh ')
INSERT [dbo].[NhaCungCap] ([maNhaCungCap], [tenNhaCungCap], [soDienThoai], [eMail], [quocGia], [diaChi]) VALUES (N'NCC-2', N'CTy Phúc Tường', N'0908010077', N'phuctuongcoltd@gmail.com', N'Việt Nam', N'Số 64 Đ. D9, KDC Chánh Nghĩa, P. Chánh Nghĩa, Tp. Thủ Dầu Một, Bình Dương')
INSERT [dbo].[NhaCungCap] ([maNhaCungCap], [tenNhaCungCap], [soDienThoai], [eMail], [quocGia], [diaChi]) VALUES (N'NCC-3', N'CTy Phát Triển Công Nghệ Minh Châu', N'02432022999', N'kinhdoanh@leadtek.vn', N'Việt Nam', N'C7 Tầng 7, Số 96B, Định Công, Thanh Xuân, Hà Nội')
INSERT [dbo].[NhaCungCap] ([maNhaCungCap], [tenNhaCungCap], [soDienThoai], [eMail], [quocGia], [diaChi]) VALUES (N'NCC-4', N'CTy Minh Tiến', N'02363730469', N'minhtiendanang@gmail.com', N'Việt Nam', N'469 Tôn Đức Thắng, P. Hòa Khánh Nam, Q. Liên Chiểu, Tp. Đà Nẵng')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-1', N'Công Phạm Quốc Việt', N'025142563', N'01524789632', N'congphamquocviet@gmail.com', N'17/350 Đ.Lê Đức Thọ F.15 Q.Gò Vấp TP.HCM', N'Đang làm', N'LNV-1')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-10', N'Thiên Linh', N'025784963', N'01685234859', N'thienlinh@gmail.com', N'12/52 Đ.Trinh Sinh F.15 Q.13', N'Đang làm', N'LNV-4')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-11', N'Quách Kim', N'025164747', N'01647586932', N'quachkim@gmail.com', N'12/45 Đ.Minh Mang Q.12 TPHCM', N'Đang làm', N'LNV-4')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-12', N'Long Trí', N'025448559', N'01647285963', N'longtri@gmail.com', N'25/5 Đ.Nguyễn Kim Q.1 TPHCM', N'Đang làm', N'LNV-3')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-13', N'Thiên Thiên', N'025164728', N'01685493652', N'thienthien@gmail.com', N'16/66 Đ.Lê Lợi F.15 Q.Gò Vấp', N'Đang làm', N'LNV-5')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-2', N'Nguyễn Trần Trung Hiếu', N'025478585', N'01524632525', N'trunghieu@gmail.com', N'12/3 Đ.QuangTrung F.13 Q.2 TPHCM', N'Đang làm', N'LNV-2')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-3', N'Cao Quốc Đông', N'025485257', N'01647285636', N'quocdong@gmail.com', N'22/8 Đ.Tôn Đức Thắng F15 Q.1 TPHCM', N'Đang làm', N'LNV-3')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-4', N'Bùi Lưu Tường Vy', N'0123456789', N'0123456789', N'vyviet20092016@gmail.com', N'Đ.Đinh Tiên Hoàng F.15 Q.Bình Thạnh', N'Đang làm', N'LNV-5')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-5', N'Lâm Hồng Huy', N'0123456789', N'05213624582', N'huy@gmail.com', N'16/8 Đ.Lê Tái Tỏ Q.Bình Thạnh TPHCM', N'Đang làm', N'LNV-4')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-6', N'Minh Tú', N'025436528', N'01647858964', N'minhtu@gmail.com', N'11/2 Đ.Quang Trung F.15 Q.Gò Vấp TPHCM', N'Đang làm', N'LNV-2')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-7', N'Minh Hào', N'025478596', N'01647859685', N'minhhao@gmail.com', N'11/32 Đ.Lê Minh Tân F.16 Q.Tinh Minh', N'Đang làm', N'LNV-2')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-8', N'Trung Tín', N'025462525', N'01647858596', N'trungtin@gmail.com', N'22/965 Đ.Lê Đức Thọ F.15 Q.Gò Vấp TPHCM', N'Đang làm', N'LNV-2')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [cMND], [soDienThoai], [eMail], [diaChi], [trangThai], [maLoaiNhanVien]) VALUES (N'NV-9', N'Phi Tinh', N'0254528563', N'01647589674', N'phitinh@gmail.com', N'22/5 Đ.Paster Q.1 TPHCM', N'Đang làm', N'LNV-4')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-1', N'NCC-1', N'NV-5', N'NV-4', CAST(N'2018-11-08' AS Date), 715400000, N'Đã thanh toán')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-2', N'NCC-2', N'NV-9', N'NV-4', CAST(N'2018-11-15' AS Date), 379880000, N'Đã thanh toán')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-3', N'NCC-3', N'NV-10', N'NV-4', CAST(N'2018-12-01' AS Date), 1683600000, N'Đã thanh toán')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-4', N'NCC-4', N'NV-11', N'NV-4', CAST(N'2018-12-08' AS Date), 912700000, N'Đã thanh toán')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-5', N'NCC-1', N'NV-11', N'NV-1', CAST(N'2018-12-09' AS Date), 397200000, N'Đã thanh toán')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-6', N'NCC-1', N'NV-10', N'NV-13', CAST(N'2018-12-10' AS Date), 945000000, N'Đã thanh toán')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-7', N'NCC-2', N'NV-10', N'NV-13', CAST(N'2018-12-10' AS Date), 400000000, N'Đã thanh toán')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-8', N'NCC-3', N'NV-9', N'NV-13', CAST(N'2018-12-10' AS Date), 700000000, N'Đã thanh toán')
INSERT [dbo].[PhieuNhapKho] ([maPhieuNhapKho], [maNhaCungCap], [maNhanVienThuKho], [maNhanVienKeToanKho], [ngayLap], [tongTien], [trangThai]) VALUES (N'PNK-9', N'NCC-4', N'NV-5', N'NV-13', CAST(N'2018-12-10' AS Date), 2199300000, N'Đã thanh toán')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-1', N'-1623739142')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-10', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-11', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-12', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-13', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-14', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-15', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-16', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-17', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-18', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-19', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-2', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-20', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-3', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-4', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-5', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-6', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-7', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-8', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'KH-9', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-1', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-10', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-11', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-12', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-13', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-2', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-3', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-4', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-5', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-6', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-7', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-8', N'-842352753')
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [matKhau]) VALUES (N'NV-9', N'-842352753')
ALTER TABLE [dbo].[ChiTietDonDatHang]  WITH CHECK ADD  CONSTRAINT [fk_maDonDatHang] FOREIGN KEY([maDonDatHang])
REFERENCES [dbo].[DonDatHang] ([maDonDatHang])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonDatHang] CHECK CONSTRAINT [fk_maDonDatHang]
GO
ALTER TABLE [dbo].[ChiTietDonDatHang]  WITH CHECK ADD  CONSTRAINT [fk_maLinhKien] FOREIGN KEY([maLinhKien])
REFERENCES [dbo].[Linhkien] ([maLinhKien])
GO
ALTER TABLE [dbo].[ChiTietDonDatHang] CHECK CONSTRAINT [fk_maLinhKien]
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapKho]  WITH CHECK ADD  CONSTRAINT [fk_maLinhKienNhapHang] FOREIGN KEY([maLinhKien])
REFERENCES [dbo].[Linhkien] ([maLinhKien])
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapKho] CHECK CONSTRAINT [fk_maLinhKienNhapHang]
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapKho]  WITH CHECK ADD  CONSTRAINT [maPhieuNhapKho] FOREIGN KEY([maPhieuNhapKho])
REFERENCES [dbo].[PhieuNhapKho] ([maPhieuNhapKho])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuNhapKho] CHECK CONSTRAINT [maPhieuNhapKho]
GO
ALTER TABLE [dbo].[DonDatHang]  WITH CHECK ADD  CONSTRAINT [fk_maKhachHang] FOREIGN KEY([maKhachHang])
REFERENCES [dbo].[KhachHang] ([maKhachHang])
GO
ALTER TABLE [dbo].[DonDatHang] CHECK CONSTRAINT [fk_maKhachHang]
GO
ALTER TABLE [dbo].[DonDatHang]  WITH CHECK ADD  CONSTRAINT [fk_maNhanVienThuNgan] FOREIGN KEY([maNhanVienThuNgan])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[DonDatHang] CHECK CONSTRAINT [fk_maNhanVienThuNgan]
GO
ALTER TABLE [dbo].[DonDatHang]  WITH CHECK ADD  CONSTRAINT [fk_maNhanVienTuVan] FOREIGN KEY([maNhanVienTuVan])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[DonDatHang] CHECK CONSTRAINT [fk_maNhanVienTuVan]
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [fk_taiKhoanKhachHang] FOREIGN KEY([maKhachHang])
REFERENCES [dbo].[TaiKhoan] ([maTaiKhoan])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [fk_taiKhoanKhachHang]
GO
ALTER TABLE [dbo].[Linhkien]  WITH CHECK ADD  CONSTRAINT [fk_maLoai] FOREIGN KEY([maLoai])
REFERENCES [dbo].[LoaiLinhKien] ([maLoai])
GO
ALTER TABLE [dbo].[Linhkien] CHECK CONSTRAINT [fk_maLoai]
GO
ALTER TABLE [dbo].[Linhkien]  WITH CHECK ADD  CONSTRAINT [fk_maNhaCungCap] FOREIGN KEY([maNhaCungCap])
REFERENCES [dbo].[NhaCungCap] ([maNhaCungCap])
GO
ALTER TABLE [dbo].[Linhkien] CHECK CONSTRAINT [fk_maNhaCungCap]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [fk_loaiNhanVien] FOREIGN KEY([maLoaiNhanVien])
REFERENCES [dbo].[LoaiNhanVien] ([maLoaiNhanVien])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [fk_loaiNhanVien]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [fk_taiKhoanNhanVien] FOREIGN KEY([maNhanVien])
REFERENCES [dbo].[TaiKhoan] ([maTaiKhoan])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [fk_taiKhoanNhanVien]
GO
ALTER TABLE [dbo].[PhieuNhapKho]  WITH CHECK ADD  CONSTRAINT [fk_maNhaCungCapNhapHang] FOREIGN KEY([maNhaCungCap])
REFERENCES [dbo].[NhaCungCap] ([maNhaCungCap])
GO
ALTER TABLE [dbo].[PhieuNhapKho] CHECK CONSTRAINT [fk_maNhaCungCapNhapHang]
GO
ALTER TABLE [dbo].[PhieuNhapKho]  WITH CHECK ADD  CONSTRAINT [fk_maNhanVienKeToanKho] FOREIGN KEY([maNhanVienKeToanKho])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[PhieuNhapKho] CHECK CONSTRAINT [fk_maNhanVienKeToanKho]
GO
ALTER TABLE [dbo].[PhieuNhapKho]  WITH CHECK ADD  CONSTRAINT [fk_maNhanVienThuKho] FOREIGN KEY([maNhanVienThuKho])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[PhieuNhapKho] CHECK CONSTRAINT [fk_maNhanVienThuKho]
GO
/****** Object:  Trigger [dbo].[giamSoLuongLinhKien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[giamSoLuongLinhKien] ON [dbo].[ChiTietDonDatHang]
AFTER INSERT AS
BEGIN
	DECLARE @id NVARCHAR(20),@soLuongMua INT
	SELECT @id = Inserted.maLinhKien FROM Inserted
	SELECT @soLuongMua = Inserted.soLuong FROM Inserted
	UPDATE dbo.Linhkien SET soLuong = soLuong - @soLuongMua WHERE maLinhKien=@id
END



GO
/****** Object:  Trigger [dbo].[tangSoLuongLinhKien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[tangSoLuongLinhKien] ON [dbo].[ChiTietDonDatHang]
AFTER DELETE as
BEGIN
	DECLARE @id NVARCHAR(20),@soLuong INT
	SELECT @id = Deleted.maLinhKien FROM Deleted
	SELECT @soLuong = Deleted.soLuong FROM Deleted
	UPDATE dbo.Linhkien SET soLuong = soLuong + @soLuong WHERE maLinhKien=@id
END



GO
/****** Object:  Trigger [dbo].[themTaiKhoanKhachHang]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[themTaiKhoanKhachHang] ON [dbo].[KhachHang]
INSTEAD OF INSERT AS
BEGIN
	DECLARE @id NVARCHAR(20)
	SELECT @id = Inserted.maKhachHang FROM Inserted
	INSERT INTO dbo.TaiKhoan VALUES  ( @id, N'-842352753')
	INSERT INTO dbo.KhachHang SELECT * FROM Inserted
END



GO
/****** Object:  Trigger [dbo].[themTaiKhoanNhanVien]    Script Date: 10/12/2018 05:41:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[themTaiKhoanNhanVien] ON [dbo].[NhanVien]
INSTEAD OF INSERT AS
BEGIN
	DECLARE @id NVARCHAR(20)
	SELECT @id = Inserted.maNhanVien FROM Inserted
	INSERT INTO dbo.TaiKhoan VALUES  ( @id, N'-842352753')
	INSERT INTO dbo.NhanVien SELECT * FROM Inserted
END



GO
USE [master]
GO
ALTER DATABASE [QuanLyLinhKien] SET  READ_WRITE 
GO
