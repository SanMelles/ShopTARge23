using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge23.Data.Migrations
{
    /// <inheritdoc />
    public partial class KindergartenImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "KindergartenId",
                table: "FileToDatabases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KindergartenId",
                table: "FileToApis",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KindergartenId",
                table: "FileToDatabases");

            migrationBuilder.DropColumn(
                name: "KindergartenId",
                table: "FileToApis");
        }
    }
}
