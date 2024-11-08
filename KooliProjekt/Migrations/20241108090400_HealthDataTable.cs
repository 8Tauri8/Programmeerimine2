using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Migrations
{
    /// <inheritdoc />
    public partial class HealthDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Blood_pressure = table.Column<float>(type: "real", nullable: false),
                    Blood_sugar = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthData", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Nutrients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sugar = table.Column<float>(type: "real", nullable: false),
                    Fat = table.Column<float>(type: "real", nullable: false),
                    Carbohydrates = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Quantity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nutrientsid = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quantity", x => x.id);
                    table.ForeignKey(
                        name: "FK_Quantity_Nutrients_Nutrientsid",
                        column: x => x.Nutrientsid,
                        principalTable: "Nutrients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nutrition",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eating_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nutrientsid = table.Column<int>(type: "int", nullable: false),
                    Quantityid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrition", x => x.id);
                    table.ForeignKey(
                        name: "FK_Nutrition_Nutrients_Nutrientsid",
                        column: x => x.Nutrientsid,
                        principalTable: "Nutrients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nutrition_Quantity_Quantityid",
                        column: x => x.Quantityid,
                        principalTable: "Quantity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction); // Changed to NoAction to avoid cascade delete cycle
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    HealthDataid = table.Column<int>(type: "int", nullable: false),
                    Nutritionid = table.Column<int>(type: "int", nullable: false),
                    Workerid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.id);
                    table.ForeignKey(
                        name: "FK_Patient_HealthData_HealthDataid",
                        column: x => x.HealthDataid,
                        principalTable: "HealthData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_Nutrition_Nutritionid",
                        column: x => x.Nutritionid,
                        principalTable: "Nutrition",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Patientid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.id);
                    table.ForeignKey(
                        name: "FK_Worker_Patient_Patientid",
                        column: x => x.Patientid,
                        principalTable: "Patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_Nutrientsid",
                table: "Nutrition",
                column: "Nutrientsid");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_Quantityid",
                table: "Nutrition",
                column: "Quantityid");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_HealthDataid",
                table: "Patient",
                column: "HealthDataid");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Nutritionid",
                table: "Patient",
                column: "Nutritionid");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Workerid",
                table: "Patient",
                column: "Workerid");

            migrationBuilder.CreateIndex(
                name: "IX_Quantity_Nutrientsid",
                table: "Quantity",
                column: "Nutrientsid");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Patientid",
                table: "Worker",
                column: "Patientid");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Worker_Workerid",
                table: "Patient",
                column: "Workerid",
                principalTable: "Worker",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_Nutrients_Nutrientsid",
                table: "Nutrition");

            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Nutrients_Nutrientsid",
                table: "Quantity");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_Quantity_Quantityid",
                table: "Nutrition");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_HealthData_HealthDataid",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Nutrition_Nutritionid",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Worker_Workerid",
                table: "Patient");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "Nutrition");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Quantity");

            migrationBuilder.DropTable(
                name: "Nutrients");

            migrationBuilder.DropTable(
                name: "HealthData");
        }
    }
}
