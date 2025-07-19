﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLKS_WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddHinhAnhToLoaiPhong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "LoaiPhongs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "LoaiPhongs");
        }
    }
}
