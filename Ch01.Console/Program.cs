using System;
using System.Collections.Generic;

// Đã tích hợp sẵn lệnh sửa lỗi font tiếng Việt
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("*********************************************");
Console.WriteLine("   CHƯƠNG TRÌNH QUẢN LÝ ĐỘI NHÓM SINH VIÊN   ");
Console.WriteLine("*********************************************\n");

try
{
    // 1. Tạo dữ liệu người dùng
    var svPhuTrong = SinhVien.Create(202401, "Phú Trọng", "trong.np@dthu.edu.vn");
    Console.WriteLine($"[+] Người dùng mới đăng nhập: {svPhuTrong.TenSinhVien} ({svPhuTrong.DiaChiEmail})");

    // 2. Nghiệp vụ: Nộp đơn thành lập CLB
    Console.WriteLine("\n---> TIẾN HÀNH DUYỆT ĐƠN THÀNH LẬP CLB <---");
    var donDangKy = DonDangKyCLB.Create(1, svPhuTrong.MaID, "CLB Lập Trình Web");
    Console.WriteLine($">> Đơn số {donDangKy.MaID} đang ở tình trạng: {donDangKy.TinhTrangDuyet}");

    // Admin duyệt đơn
    donDangKy.XacNhanDuyet();
    var clbWeb = CauLacBo.Create(99, donDangKy.TenCLBMongMuon, "Công nghệ phần mềm");
    Console.WriteLine($">> Ban giám hiệu đã cấp phép hoạt động cho: {clbWeb.TenGoi} (Chuyên môn: {clbWeb.MangHoatDong})");

    // 3. Nghiệp vụ: Gửi thông báo đến thành viên
    Console.WriteLine("\n---> PHÁT ĐI THÔNG BÁO NỘI BỘ <---");
    var tinNhan = ThongBaoCLB.Create(10, clbWeb.MaID, "Tuyển thành viên Gen 1", "Các bạn điền form đăng ký trước thứ 6 tuần này nhé.");
    Console.WriteLine($">> {clbWeb.TenGoi} vừa đăng thông báo mới: [{tinNhan.ChuDe}]");

    // 4. Nghiệp vụ: Sinh viên gửi góp ý/đánh giá
    Console.WriteLine("\n---> GHI NHẬN GÓP Ý TỪ SINH VIÊN <---");
    var gopY = DanhGiaHoatDong.Create(100, svPhuTrong.MaID, clbWeb.MaID, "Buổi training hôm qua rất thú vị và bổ ích!");
    Console.WriteLine($">> Sinh viên {svPhuTrong.TenSinhVien} nhận xét: '{gopY.ChiTietGopY}'");

    // 5. In báo cáo tóm tắt cuối ngày
    Console.WriteLine("\n=========================================");
    Console.WriteLine("        TỔNG KẾT DỮ LIỆU CUỐI NGÀY       ");
    Console.WriteLine("=========================================");
    Console.WriteLine($"- Danh sách CLB mới thành lập: {clbWeb.TenGoi}");
    Console.WriteLine($"- Số lượng đơn chờ xét duyệt: 0");
    Console.WriteLine($"- Số lượng thông báo đã phát ra: 1");
    Console.WriteLine($"- Tổng lượt đánh giá nhận được: 1");
    Console.WriteLine("=========================================\n");
}
catch (Exception ex)
{
    // Bắt lỗi nếu dữ liệu khởi tạo bị sai
    Console.WriteLine($"\n[CẢNH BÁO LỖI HỆ THỐNG]: {ex.Message}");
}

// ==========================================
// KHAI BÁO CÁC CLASS BÊN DƯỚI (Đã đổi tên)
// ==========================================

public class SinhVien
{
    public int MaID { get; private set; }
    public string TenSinhVien { get; private set; }
    public string DiaChiEmail { get; private set; }

    private SinhVien(int maId, string tenSinhVien, string diaChiEmail)
    {
        MaID = maId;
        TenSinhVien = tenSinhVien;
        DiaChiEmail = diaChiEmail;
    }

    public static SinhVien Create(int maId, string tenSinhVien, string diaChiEmail)
    {
        if (string.IsNullOrWhiteSpace(tenSinhVien))
            throw new ArgumentException("Thiếu thông tin tên sinh viên.");
        if (!diaChiEmail.Contains("@"))
            throw new ArgumentException("Định dạng email bị sai.");
        return new SinhVien(maId, tenSinhVien, diaChiEmail);
    }
}

public class CauLacBo
{
    public int MaID { get; private set; }
    public string TenGoi { get; private set; }
    public string MangHoatDong { get; private set; }
    public string TinhTrang { get; private set; }

    private CauLacBo(int maId, string tenGoi, string mangHoatDong, string tinhTrang)
    {
        MaID = maId;
        TenGoi = tenGoi;
        MangHoatDong = mangHoatDong;
        TinhTrang = tinhTrang;
    }

    public static CauLacBo Create(int maId, string tenGoi, string mangHoatDong)
    {
        if (string.IsNullOrWhiteSpace(tenGoi))
            throw new ArgumentException("Tên câu lạc bộ không hợp lệ.");
        return new CauLacBo(maId, tenGoi, mangHoatDong, "Đang hoạt động");
    }
}

public class DonDangKyCLB
{
    public int MaID { get; private set; }
    public int NguoiLapDonID { get; private set; }
    public string TenCLBMongMuon { get; private set; }
    public string TinhTrangDuyet { get; private set; }

    private DonDangKyCLB(int maId, int nguoiLapDonId, string tenClbMongMuon, string tinhTrangDuyet)
    {
        MaID = maId;
        NguoiLapDonID = nguoiLapDonId;
        TenCLBMongMuon = tenClbMongMuon;
        TinhTrangDuyet = tinhTrangDuyet;
    }

    public static DonDangKyCLB Create(int maId, int nguoiLapDonId, string tenClbMongMuon)
    {
        if (string.IsNullOrWhiteSpace(tenClbMongMuon))
            throw new ArgumentException("Chưa điền tên CLB muốn thành lập.");
        return new DonDangKyCLB(maId, nguoiLapDonId, tenClbMongMuon, "Chờ duyệt");
    }

    public void XacNhanDuyet()
    {
        TinhTrangDuyet = "Đã duyệt"; // Đổi trạng thái khi Admin duyệt
    }
}

public class ThongBaoCLB
{
    public int MaID { get; private set; }
    public int CauLacBoID { get; private set; }
    public string ChuDe { get; private set; }
    public string NoiDungTin { get; private set; }

    private ThongBaoCLB(int maId, int cauLacBoId, string chuDe, string noiDungTin)
    {
        MaID = maId;
        CauLacBoID = cauLacBoId;
        ChuDe = chuDe;
        NoiDungTin = noiDungTin;
    }

    public static ThongBaoCLB Create(int maId, int cauLacBoId, string chuDe, string noiDungTin)
    {
        if (string.IsNullOrWhiteSpace(chuDe) || string.IsNullOrWhiteSpace(noiDungTin))
            throw new ArgumentException("Chủ đề và nội dung thông báo phải được điền đầy đủ.");
        return new ThongBaoCLB(maId, cauLacBoId, chuDe, noiDungTin);
    }
}

public class DanhGiaHoatDong
{
    public int MaID { get; private set; }
    public int SinhVienID { get; private set; }
    public int CauLacBoID { get; private set; }
    public string ChiTietGopY { get; private set; }

    private DanhGiaHoatDong(int maId, int sinhVienId, int cauLacBoId, string chiTietGopY)
    {
        MaID = maId;
        SinhVienID = sinhVienId;
        CauLacBoID = cauLacBoId;
        ChiTietGopY = chiTietGopY;
    }

    public static DanhGiaHoatDong Create(int maId, int sinhVienId, int cauLacBoId, string chiTietGopY)
    {
        if (string.IsNullOrWhiteSpace(chiTietGopY))
            throw new ArgumentException("Nội dung đánh giá trống.");
        return new DanhGiaHoatDong(maId, sinhVienId, cauLacBoId, chiTietGopY);
    }
}