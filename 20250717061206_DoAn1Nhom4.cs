using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLKS_WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class DoAn1Nhom4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "DatPhong",
                columns: table => new
                {
                    MaDat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatPhong", x => x.MaDat);
                    table.ForeignKey(
                        name: "FK_DatPhong_KhachHang_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "KhachHang",
                columns: new[] { "MaKH", "CCCD", "DiaChi", "HoTen", "SDT" },
                values: new object[] { 1, "12345", "Nam Dinh", "Do Hoang Minh", "123456789" });

            migrationBuilder.InsertData(
                table: "DatPhong",
                columns: new[] { "MaDat", "MaKH", "MaPhong", "NgayDat", "NgayNhan", "NgayTra", "TrangThai" },
                values: new object[] { 1, 1, 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.CreateIndex(
                name: "IX_DatPhong_MaKH",
                table: "DatPhong",
                column: "MaKH");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatPhong");

            migrationBuilder.DropTable(
                name: "KhachHang");
        }
    }
}
