using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Migrations
{
    /// <inheritdoc />
    public partial class _1000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_HealthData_HealthDataid",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Nutrition_Nutritionid",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Nutrients_Nutrientsid",
                table: "Quantity");

            migrationBuilder.DropIndex(
                name: "IX_Quantity_Nutrientsid",
                table: "Quantity");

            migrationBuilder.DropIndex(
                name: "IX_Patient_HealthDataid",
                table: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Patient_Nutritionid",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Nutrientsid",
                table: "Quantity");

            migrationBuilder.DropColumn(
                name: "HealthDataid",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Nutritionid",
                table: "Patient");

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "Quantity",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<float>(
                name: "Nutrients",
                table: "Quantity",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "HealthData",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nutrition",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nutrients",
                table: "Quantity");

            migrationBuilder.DropColumn(
                name: "HealthData",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Nutrition",
                table: "Patient");

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                table: "Quantity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "Nutrientsid",
                table: "Quantity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealthDataid",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Nutritionid",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_Nutrientsid",
                table: "Quantity",
                column: "Nutrientsid");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_HealthDataid",
                table: "Patient",
                column: "HealthDataid");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Nutritionid",
                table: "Patient",
                column: "Nutritionid");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_HealthData_HealthDataid",
                table: "Patient",
                column: "HealthDataid",
                principalTable: "HealthData",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Nutrition_Nutritionid",
                table: "Patient",
                column: "Nutritionid",
                principalTable: "Nutrition",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Nutrients_Nutrientsid",
                table: "Quantity",
                column: "Nutrientsid",
                principalTable: "Nutrients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
