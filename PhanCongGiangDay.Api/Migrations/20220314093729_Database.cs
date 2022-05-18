using Microsoft.EntityFrameworkCore.Migrations;

namespace PhanCongGiangDay.Api.Migrations
{
    public partial class Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiangVien",
                columns: table => new
                {
                    GiangVienId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiangVien = table.Column<string>(nullable: true),
                    HoTen = table.Column<string>(nullable: true),
                    GioiTinh = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    MatKhau = table.Column<string>(nullable: true),
                    Quyen = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVien", x => x.GiangVienId);
                });

            migrationBuilder.CreateTable(
                name: "Hocky",
                columns: table => new
                {
                    HocKyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamHoc = table.Column<int>(nullable: false),
                    HocKyThu = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hocky", x => x.HocKyId);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    KhoaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhoa = table.Column<string>(nullable: true),
                    TenKhoa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.KhoaId);
                });

            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    MonHocId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaMonHoc = table.Column<string>(nullable: true),
                    TenMonHoc = table.Column<string>(nullable: true),
                    SoTinChi = table.Column<int>(nullable: false),
                    SoTietLT = table.Column<int>(nullable: false),
                    SoTietTH = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.MonHocId);
                });

            migrationBuilder.CreateTable(
                name: "NhomLop",
                columns: table => new
                {
                    NhomLopId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhomLop = table.Column<string>(nullable: true),
                    KhoaId = table.Column<int>(nullable: false),
                    HocKyId = table.Column<int>(nullable: false),
                    MonHocId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomLop", x => x.NhomLopId);
                    table.ForeignKey(
                        name: "FK_NhomLop_Hocky_HocKyId",
                        column: x => x.HocKyId,
                        principalTable: "Hocky",
                        principalColumn: "HocKyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhomLop_Khoa_KhoaId",
                        column: x => x.KhoaId,
                        principalTable: "Khoa",
                        principalColumn: "KhoaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhomLop_MonHoc_MonHocId",
                        column: x => x.MonHocId,
                        principalTable: "MonHoc",
                        principalColumn: "MonHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanCong",
                columns: table => new
                {
                    PhanCongId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiangVienId = table.Column<int>(nullable: false),
                    NhomLopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCong", x => x.PhanCongId);
                    table.ForeignKey(
                        name: "FK_PhanCong_GiangVien_GiangVienId",
                        column: x => x.GiangVienId,
                        principalTable: "GiangVien",
                        principalColumn: "GiangVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanCong_NhomLop_NhomLopId",
                        column: x => x.NhomLopId,
                        principalTable: "NhomLop",
                        principalColumn: "NhomLopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GiangVien",
                columns: new[] { "GiangVienId", "DiaChi", "Email", "GioiTinh", "HoTen", "MaGiangVien", "MatKhau", "NgaySinh", "Quyen", "SoDienThoai" },
                values: new object[,]
                {
                    { 1, "Bình Dương", "cntt01@gmail.com", "Nam", "Nguyễn Văn A", "CNTT01", "123", "02/07/1990", 1, "0987654321" },
                    { 2, "Bình Dương", "cntt02@gmail.com", "Nam", "Nguyễn Văn B", "CNTT02", "123", "12/02/1991", 1, "0987654321" },
                    { 3, "Bình Dương", "cntt03@gmail.com", "Nữ", "Nguyễn Thị C", "CNTT03", "123", "10/05/1990", 1, "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Hocky",
                columns: new[] { "HocKyId", "HocKyThu", "NamHoc" },
                values: new object[,]
                {
                    { 1, 0, 2022 },
                    { 2, 1, 2022 }
                });

            migrationBuilder.InsertData(
                table: "Khoa",
                columns: new[] { "KhoaId", "MaKhoa", "TenKhoa" },
                values: new object[] { 1, "CNTT", "Công nghệ thông tin" });

            migrationBuilder.InsertData(
                table: "MonHoc",
                columns: new[] { "MonHocId", "MaMonHoc", "SoTietLT", "SoTietTH", "SoTinChi", "TenMonHoc" },
                values: new object[,]
                {
                    { 1, "LING123", 30, 15, 3, "Cơ sở lập trình" },
                    { 2, "LING124", 30, 30, 4, "Kỹ thuật lập trình" },
                    { 3, "LING125", 30, 30, 4, "Lập trình web" }
                });

            migrationBuilder.InsertData(
                table: "NhomLop",
                columns: new[] { "NhomLopId", "HocKyId", "KhoaId", "MaNhomLop", "MonHocId" },
                values: new object[,]
                {
                    { 1, 1, 1, "D19PM01", 1 },
                    { 2, 1, 1, "D19PM02", 2 },
                    { 3, 1, 1, "D19PM02", 2 },
                    { 4, 1, 1, "D19PM02", 2 }
                });

            migrationBuilder.InsertData(
                table: "PhanCong",
                columns: new[] { "PhanCongId", "GiangVienId", "NhomLopId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "PhanCong",
                columns: new[] { "PhanCongId", "GiangVienId", "NhomLopId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "PhanCong",
                columns: new[] { "PhanCongId", "GiangVienId", "NhomLopId" },
                values: new object[] { 3, 2, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_NhomLop_HocKyId",
                table: "NhomLop",
                column: "HocKyId");

            migrationBuilder.CreateIndex(
                name: "IX_NhomLop_KhoaId",
                table: "NhomLop",
                column: "KhoaId");

            migrationBuilder.CreateIndex(
                name: "IX_NhomLop_MonHocId",
                table: "NhomLop",
                column: "MonHocId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_GiangVienId",
                table: "PhanCong",
                column: "GiangVienId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_NhomLopId",
                table: "PhanCong",
                column: "NhomLopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhanCong");

            migrationBuilder.DropTable(
                name: "GiangVien");

            migrationBuilder.DropTable(
                name: "NhomLop");

            migrationBuilder.DropTable(
                name: "Hocky");

            migrationBuilder.DropTable(
                name: "Khoa");

            migrationBuilder.DropTable(
                name: "MonHoc");
        }
    }
}
