--1. MAT HANG
--1.1 GetMaMatHang MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetNewMaMatHang' AND type = 'P')
    DROP PROCEDURE sp_GetNewMaMatHang
GO

CREATE PROC sp_GetNewMaMatHang
@MaMatHangMoi nvarchar(10) output
AS	
	DECLARE	@temp int	
	SET @temp = 1
	SET @MaMatHangMoi = 'MH'
	WHILE (EXISTS(SELECT * FROM MAT_HANG WHERE CONVERT(int, RIGHT(MaMatHang,LEN(MaMatHang)-2 )) = @temp))
		SET @temp = @temp + 1
	
	SET @MaMatHangMoi = @MaMatHangMoi + CONVERT(varchar,@temp)
GO

--1.2 GETTABLE MAT_HANG===============================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetBangMatHang' AND type = 'P')
    DROP PROCEDURE sp_GetBangMatHang
GO

CREATE PROC sp_GetBangMatHang
AS
	BEGIN TRAN
		SELECT  MH.*,LMH.TenLoaiMatHang
		FROM MAT_HANG MH,LOAI_MAT_HANG LMH
		WHERE LMH.MaLoaiMatHang=MH.MaLoaiMatHang AND MH.TrangThai=1
		ORDER BY TenMatHang
	COMMIT TRAN
GO

--1.3 Insert MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_InsertMatHang' AND type = 'P')
    DROP PROCEDURE sp_InsertMatHang
GO

CREATE PROC sp_InsertMatHang
	@MaMatHang nvarchar(10),
	@TenMatHang nvarchar(250),
	@MaLoaiMatHang nvarchar(10),
	@DonViTinh nvarchar(255),
	@DonGia float,
	@GiaMua float,
	@GiaBanSi float,
	@GiaBanLe float,
	@PT_GiaBanSi float,
	@PT_GiaBanLe float,
	@LuongMin int,
	@LuongMax int,
	@SoLuongTon int,
	@ThueVAT float,
	@XuatXu nvarchar(255),
	@DienGiai ntext,
	@MaVach nvarchar(50)
AS	
	DECLARE @ProID INT
	SET @ProID=-1
	IF (EXISTS(SELECT * FROM MAT_HANG WHERE TenMatHang=@TenMatHang AND MaLoaiMatHang=@MaLoaiMatHang))
	BEGIN
		RETURN 0
	END

	BEGIN TRAN
		INSERT INTO MAT_HANG(MaMatHang,TenMatHang,MaLoaiMatHang,DonViTinh,DonGia,GiaMua,GiaBanSi,GiaBanLe,
							PT_GiaBanSi,PT_GiaBanLe,LuongMin,LuongMax,SoLuongTon,ThueVAT,XuatXu,DienGiai,
							MaVach,NgayTao,TrangThai)
		VALUES(@MaMatHang,@TenMatHang,@MaLoaiMatHang,@DonViTinh,@DonGia,@GiaMua,@GiaBanSi,@GiaBanLe,@PT_GiaBanSi,
				@PT_GiaBanLe,@LuongMin,@LuongMax,@SoLuongTon,@ThueVAT,@XuatXu,@DienGiai,@MaVach,getdate(),1)
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--sp_InsertMatHang 'MH10', N'Giày Adiadas','NH4',N'Đôi',100000,90000,110000,120000,110,120,1,100,20,10,N'Mỹ',N'Hàng mới nhập','00445TH'

--1.4 Update MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_UpdateMatHang' AND type = 'P')
    DROP PROCEDURE sp_UpdateMatHang
GO

CREATE PROC sp_UpdateMatHang
	@MaMatHang nvarchar(10),
	@TenMatHang nvarchar(250),
	@MaLoaiMatHang nvarchar(10),
	@DonViTinh nvarchar(255),
	@DonGia float,
	@GiaMua float,
	@GiaBanSi float,
	@GiaBanLe float,
	@PT_GiaBanSi float,
	@PT_GiaBanLe float,
	@LuongMin int,
	@LuongMax int,
	@SoLuongTon int,
	@ThueVAT float,
	@XuatXu nvarchar(255),
	@DienGiai ntext,
	@MaVach nvarchar(50)
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE MAT_HANG
		SET
			TenMatHang=@TenMatHang,
			MaLoaiMatHang=@MaLoaiMatHang,
			DonViTinh=@DonViTinh,
			DonGia=@DonGia,
			GiaMua=@GiaMua,
			GiaBanSi=@GiaBanSi,
			GiaBanLe=@GiaBanLe,
			PT_GiaBanSi=@PT_GiaBanSi,
			PT_GiaBanLe=@PT_GiaBanLe,
			LuongMin=@LuongMin,
			LuongMax=@LuongMax,
			SoLuongTon=@SoLuongTon,
			ThueVAT=@ThueVAT,
			XuatXu=@XuatXu,
			DienGiai=@DienGiai,
			MaVach=@MaVach,
			NgayTao=getdate()
		WHERE MaMatHang=@MaMatHang AND TrangThai=1
	SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--sp_UpdateMatHang 'MH10', N'Giày purma','NH4',N'Đôi',100000,90000,110000,120000,110,120,1,100,20,10,N'Mỹ',N'Hàng mới nhập','00445TH'

--1.5 Update SO_LUONG_TON_MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_UpdateSoLuongTonMatHang' AND type = 'P')
    DROP PROCEDURE sp_UpdateSoLuongTonMatHang
GO

CREATE PROC sp_UpdateSoLuongTonMatHang
	@MaMatHang nvarchar(10),
	@SoLuongTon int
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE MAT_HANG
		SET
			SoLuongTon=@SoLuongTon
		WHERE MaMatHang=@MaMatHang AND TrangThai=1
	SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO
--sp_UpdateSoLuongTonMatHang 'MH10',120

--1.6 Delete MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_DeleteMatHang' AND type = 'P')
    DROP PROCEDURE sp_DeleteMatHang
GO

CREATE PROC sp_DeleteMatHang
	@MaMatHang nvarchar(10)
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE MAT_HANG
		SET
			TrangThai=0
		WHERE MaMatHang=@MaMatHang AND TrangThai=1
	SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO
--sp_DeleteMatHang 'MH10'


--2. LOAI MAT HANG
--2.1 GetMaLoaiMatHang LOAI_MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetNewMaLoaiMatHang' AND type = 'P')
    DROP PROCEDURE sp_GetNewMaLoaiMatHang
GO

CREATE PROC sp_GetNewMaLoaiMatHang
@MaLoaiMatHangMoi nvarchar(10) output
AS	
	DECLARE	@temp int	
	SET @temp = 1
	SET @MaLoaiMatHangMoi = 'NH'
	WHILE (EXISTS(SELECT * FROM LOAI_MAT_HANG WHERE CONVERT(int, RIGHT(MaLoaiMatHang,LEN(MaLoaiMatHang)-2 )) = @temp))
		SET @temp = @temp + 1
	
	SET @MaLoaiMatHangMoi = @MaLoaiMatHangMoi + CONVERT(varchar,@temp)
GO

--2.2 GETTABLE LOAI_MAT_HANG===============================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetBangLoaiMatHang' AND type = 'P')
    DROP PROCEDURE sp_GetBangLoaiMatHang
GO

CREATE PROC sp_GetBangLoaiMatHang
AS
	BEGIN TRAN
		SELECT  *
		FROM LOAI_MAT_HANG 
		WHERE TrangThai=1
		ORDER BY TenLoaiMatHang
	COMMIT TRAN
GO

--2.3 Insert LOAI_MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_InsertLoaiMatHang' AND type = 'P')
    DROP PROCEDURE sp_InsertLoaiMatHang
GO

CREATE PROC sp_InsertLoaiMatHang
	@MaLoaiMatHang nvarchar(10),
	@TenLoaiMatHang nvarchar(255),
	@DienGiai ntext
AS	
	DECLARE @ProID INT
	SET @ProID=-1
	IF (EXISTS(SELECT * FROM LOAI_MAT_HANG WHERE TenLoaiMatHang=@TenLoaiMatHang))
	BEGIN
		RETURN 0
	END

	BEGIN TRAN
		INSERT INTO LOAI_MAT_HANG(MaLoaiMatHang,TenLoaiMatHang,DienGiai,NgayTao,TrangThai)
		VALUES(@MaLoaiMatHang,@TenLoaiMatHang,@DienGiai,getdate(),1)
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--sp_InsertLoaiMatHang 'NH7', N'Sách',N''

--2.4 Update LOAI_MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_UpdateLoaiMatHang' AND type = 'P')
    DROP PROCEDURE sp_UpdateLoaiMatHang
GO

CREATE PROC sp_UpdateLoaiMatHang
	@MaLoaiMatHang nvarchar(10),
	@TenLoaiMatHang nvarchar(255),
	@DienGiai ntext
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE LOAI_MAT_HANG
		SET
			TenLoaiMatHang=@TenLoaiMatHang,
			DienGiai=@DienGiai
		WHERE MaLoaiMatHang=@MaLoaiMatHang AND TrangThai=1
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--sp_UpdateLoaiMatHang 'NH7', N'Sách',N'sách tổng hợp'

--2.5 Delete LOAI_MAT_HANG=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_DeleteLoaiMatHang' AND type = 'P')
    DROP PROCEDURE sp_DeleteLoaiMatHang
GO

CREATE PROC sp_DeleteLoaiMatHang
	@MaLoaiMatHang nvarchar(10)
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE LOAI_MAT_HANG
		SET
			TrangThai=0
		WHERE MaLoaiMatHang=@MaLoaiMatHang AND TrangThai=1
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--3. NUOC SAN XUAT
--3.1 GetMaNuocSanXuat NUOC_SAN_XUAT=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetNewMaNuocSanXuat' AND type = 'P')
    DROP PROCEDURE sp_GetNewMaNuocSanXuat
GO

CREATE PROC sp_GetNewMaNuocSanXuat
@MaNuocSanXuatMoi nvarchar(10) output
AS	
	DECLARE	@temp int	
	SET @temp = 1
	SET @MaNuocSanXuatMoi = 'NSX'
	WHILE (EXISTS(SELECT * FROM NUOC_SAN_XUAT WHERE CONVERT(int, RIGHT(MaNuocSanXuat,LEN(MaNuocSanXuat)-3 )) = @temp))
		SET @temp = @temp + 1
	
	SET @MaNuocSanXuatMoi = @MaNuocSanXuatMoi + CONVERT(varchar,@temp)
GO

--3.2 GETTABLE NUOC_SAN_XUAT===============================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetBangNuocSanXuat' AND type = 'P')
    DROP PROCEDURE sp_GetBangNuocSanXuat
GO

CREATE PROC sp_GetBangNuocSanXuat
AS
	BEGIN TRAN
		SELECT  *
		FROM NUOC_SAN_XUAT 
		WHERE TrangThai=1
		ORDER BY TenNuocSanXuat
	COMMIT TRAN
GO

--3.3 Insert NUOC_SAN_XUAT=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_InsertNuocSanXuat' AND type = 'P')
    DROP PROCEDURE sp_InsertNuocSanXuat
GO

CREATE PROC sp_InsertNuocSanXuat
	@MaNuocSanXuat nvarchar(10),
	@TenNuocSanXuat nvarchar(255)
AS	
	DECLARE @ProID INT
	SET @ProID=-1
	IF (EXISTS(SELECT * FROM NUOC_SAN_XUAT WHERE TenNuocSanXuat=@TenNuocSanXuat))
	BEGIN
		RETURN 0
	END

	BEGIN TRAN
		INSERT INTO NUOC_SAN_XUAT(MaNuocSanXuat,TenNuocSanXuat,TrangThai)
		VALUES(@MaNuocSanXuat,@TenNuocSanXuat,1)
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--sp_InsertNuocSanXuat 'NH7', N'Sách',N''

--2.4 Update NUOC_SAN_XUAT=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_UpdateNuocSanXuat' AND type = 'P')
    DROP PROCEDURE sp_UpdateNuocSanXuat
GO

CREATE PROC sp_UpdateNuocSanXuat
	@MaNuocSanXuat nvarchar(10),
	@TenNuocSanXuat nvarchar(255)
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE NUOC_SAN_XUAT
		SET
			TenNuocSanXuat=@TenNuocSanXuat
		WHERE MaNuocSanXuat=@MaNuocSanXuat AND TrangThai=1
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--sp_UpdateNuocSanXuat 'NH7', N'Sách',N'sách tổng hợp'

--2.5 Delete NUOC_SAN_XUAT=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_DeleteNuocSanXuat' AND type = 'P')
    DROP PROCEDURE sp_DeleteNuocSanXuat
GO

CREATE PROC sp_DeleteNuocSanXuat
	@MaNuocSanXuat nvarchar(10)
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE NUOC_SAN_XUAT
		SET
			TrangThai=0
		WHERE MaNuocSanXuat=@MaNuocSanXuat AND TrangThai=1
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--4. DON VI TINH
--4.1 GetMaNuocSanXuat DON_VI_TINH=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetNewMaDonViTinh' AND type = 'P')
    DROP PROCEDURE sp_GetNewMaDonViTinh
GO

CREATE PROC sp_GetNewMaDonViTinh
@MaDonViTinhMoi nvarchar(10) output
AS	
	DECLARE	@temp int	
	SET @temp = 1
	SET @MaDonViTinhMoi = 'DVT'
	WHILE (EXISTS(SELECT * FROM DON_VI_TINH WHERE CONVERT(int, RIGHT(MaDonViTinh,LEN(MaDonViTinh)-3 )) = @temp))
		SET @temp = @temp + 1
	
	SET @MaDonViTinhMoi = @MaDonViTinhMoi + CONVERT(varchar,@temp)
GO

--4.2 GETTABLE DON_VI_TINH===============================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetBangDonViTinh' AND type = 'P')
    DROP PROCEDURE sp_GetBangDonViTinh
GO

CREATE PROC sp_GetBangDonViTinh
AS
	BEGIN TRAN
		SELECT  *
		FROM DON_VI_TINH 
		WHERE TrangThai=1
		ORDER BY TenDonViTinh
	COMMIT TRAN
GO

--4.3 Insert DON_VI_TINH=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_InsertDonViTinh' AND type = 'P')
    DROP PROCEDURE sp_InsertDonViTinh
GO

CREATE PROC sp_InsertDonViTinh
	@MaDonViTinh nvarchar(10),
	@TenDonViTinh nvarchar(255)
AS	
	DECLARE @ProID INT
	SET @ProID=-1
	IF (EXISTS(SELECT * FROM DON_VI_TINH WHERE TenDonViTinh=@TenDonViTinh))
	BEGIN
		RETURN 0
	END

	BEGIN TRAN
		INSERT INTO DON_VI_TINH(MaDonViTinh,TenDonViTinh,TrangThai)
		VALUES(@MaDonViTinh,@TenDonViTinh,1)
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--sp_InsertDonViTinh 'NH7', N'Sách',N''

--4.4 Update DON_VI_TINH=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_UpdateDonViTinh' AND type = 'P')
    DROP PROCEDURE sp_UpdateDonViTinh
GO

CREATE PROC sp_UpdateDonViTinh
	@MaDonViTinh nvarchar(10),
	@TenDonViTinh nvarchar(255)
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE DON_VI_TINH
		SET
			TenDonViTinh=@TenDonViTinh
		WHERE MaDonViTinh=@MaDonViTinh AND TrangThai=1
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO

--sp_UpdateDonViTinh 'NH7', N'Sách',N'sách tổng hợp'

--4.5 Delete DON_VI_TINH=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_DeleteDonViTinh' AND type = 'P')
    DROP PROCEDURE sp_DeleteDonViTinh
GO

CREATE PROC sp_DeleteDonViTinh
	@MaDonViTinh nvarchar(10)
AS	
	DECLARE @ProID INT
	SET @ProID=-1

	BEGIN TRAN
		UPDATE DON_VI_TINH
		SET
			TrangThai=0
		WHERE MaDonViTinh=@MaDonViTinh AND TrangThai=1
		SET @ProID=1
	COMMIT TRAN
	RETURN @ProID
GO


--3.Phieu Nhap
--1.2 GetnewMaPhieuNhap PHIEU_NHAP=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetNewMaPhieuNhap' AND type = 'P')
    DROP PROCEDURE sp_GetNewMaPhieuNhap
GO

CREATE PROC sp_GetNewMaPhieuNhap
@MaPhieuNhapMoi nvarchar(10) output
AS	
	DECLARE	@temp int	
	SET @temp = 1
	SET @MaPhieuNhapMoi = 'PNK'
	WHILE (EXISTS(SELECT * FROM PHIEU_NHAP WHERE CONVERT(int, RIGHT(MaPhieuNhap,LEN(MaPhieuNhap)-3 )) = @temp))
		SET @temp = @temp + 1
	
	SET @MaPhieuNhapMoi = @MaPhieuNhapMoi + CONVERT(varchar,@temp)
GO

--3.Phieu Chi
--1.2 GetnewMaPhieuChi PHIEU_CHI=================================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_GetNewMaPhieuChi' AND type = 'P')
    DROP PROCEDURE sp_GetNewMaPhieuChi
GO

CREATE PROC sp_GetNewMaPhieuChi
@MaPhieuChiMoi nvarchar(10) output
AS	
	DECLARE	@temp int	
	SET @temp = 1
	SET @MaPhieuChiMoi = 'PC'
	WHILE (EXISTS(SELECT * FROM PHIEU_CHI WHERE CONVERT(int, RIGHT(MaPhieuChi,LEN(MaPhieuChi)-2 )) = @temp))
		SET @temp = @temp + 1
	
	SET @MaPhieuChiMoi = @MaPhieuChiMoi + CONVERT(varchar,@temp)
GO


--1.1 SEARCH PHIEU_CHI===============================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_SearchPhieuChiKhac' AND type = 'P')
    DROP PROCEDURE sp_SearchPhieuChiKhac
GO

CREATE PROC sp_SearchPhieuChiKhac
	@TuNgay smalldatetime,
	@DenNgay smalldatetime
AS
	BEGIN TRAN
		SELECT  * 
		FROM PHIEU_CHI
		WHERE LoaiPhieuChi= 'Phiếu chi khác' AND TrangThai=1 AND
		(convert(datetime, convert(varchar, NgayChi,101),101) >= @TuNgay
				AND convert(datetime, convert(varchar, NgayChi,101),101) <= @DenNgay)
	COMMIT TRAN
GO

--1.1 SEARCH PHIEU_CHI===============================================
IF EXISTS (SELECT name FROM sysobjects WHERE name = N'sp_SearchPhieuChiKhacTheoNCC' AND type = 'P')
    DROP PROCEDURE sp_SearchPhieuChiKhacTheoNCC
GO

CREATE PROC sp_SearchPhieuChiKhacTheoNCC
	@TuNgay smalldatetime,
	@DenNgay smalldatetime,
	@NhaCungCap nvarchar(255)
AS
	BEGIN TRAN
		SELECT  * 
		FROM PHIEU_CHI
		WHERE LoaiPhieuChi= 'Phiếu chi khác' AND NhaCungCap=@NhaCungCap  AND TrangThai=1 AND
		(convert(datetime, convert(varchar, NgayChi,101),101) >= @TuNgay
				AND convert(datetime, convert(varchar, NgayChi,101),101) <= @DenNgay)
	COMMIT TRAN
GO