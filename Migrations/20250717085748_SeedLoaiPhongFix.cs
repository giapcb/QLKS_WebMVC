using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLKS_WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedLoaiPhongFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phongs_LoaiPhongs_Mã loại phòng",
                table: "Phongs");

            migrationBuilder.DropIndex(
                name: "IX_Phongs_Mã loại phòng",
                table: "Phongs");

            migrationBuilder.DropColumn(
                name: "Mã loại phòng",
                table: "Phongs");

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_LoaiPhongId",
                table: "Phongs",
                column: "LoaiPhongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phongs_LoaiPhongs_LoaiPhongId",
                table: "Phongs",
                column: "LoaiPhongId",
                principalTable: "LoaiPhongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phongs_LoaiPhongs_LoaiPhongId",
                table: "Phongs");

            migrationBuilder.DropIndex(
                name: "IX_Phongs_LoaiPhongId",
                table: "Phongs");

            migrationBuilder.AddColumn<int>(
                name: "Mã loại phòng",
                table: "Phongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_Mã loại phòng",
                table: "Phongs",
                column: "Mã loại phòng");

            migrationBuilder.AddForeignKey(
                name: "FK_Phongs_LoaiPhongs_Mã loại phòng",
                table: "Phongs",
                column: "Mã loại phòng",
                principalTable: "LoaiPhongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
