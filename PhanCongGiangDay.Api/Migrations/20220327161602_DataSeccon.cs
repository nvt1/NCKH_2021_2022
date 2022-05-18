using Microsoft.EntityFrameworkCore.Migrations;

namespace PhanCongGiangDay.Api.Migrations
{
    public partial class DataSeccon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HocKyThu",
                table: "Hocky",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Hocky",
                keyColumn: "HocKyId",
                keyValue: 1,
                column: "HocKyThu",
                value: "1");

            migrationBuilder.UpdateData(
                table: "Hocky",
                keyColumn: "HocKyId",
                keyValue: 2,
                column: "HocKyThu",
                value: "2");

            migrationBuilder.InsertData(
                table: "Hocky",
                columns: new[] { "HocKyId", "HocKyThu", "NamHoc" },
                values: new object[] { 3, "3", 2022 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hocky",
                keyColumn: "HocKyId",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "HocKyThu",
                table: "Hocky",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Hocky",
                keyColumn: "HocKyId",
                keyValue: 1,
                column: "HocKyThu",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Hocky",
                keyColumn: "HocKyId",
                keyValue: 2,
                column: "HocKyThu",
                value: 1);
        }
    }
}
