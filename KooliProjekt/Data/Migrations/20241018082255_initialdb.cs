using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_Nutrients_NutrientsID",
                table: "Nutrition");

            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_Quantity_QuantityID",
                table: "Nutrition");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_HealthData_HealthDataID",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Nutrition_NutritionID",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Worker_WorkerID",
                table: "Patient");

            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Nutrients_NutrientsID",
                table: "Quantity");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Patient_PatientID",
                table: "Worker");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Worker",
                newName: "Patientid");

            migrationBuilder.RenameColumn(
                name: "WorkerID",
                table: "Worker",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_PatientID",
                table: "Worker",
                newName: "IX_Worker_Patientid");

            migrationBuilder.RenameColumn(
                name: "NutrientsID",
                table: "Quantity",
                newName: "Nutrientsid");

            migrationBuilder.RenameColumn(
                name: "QuantityID",
                table: "Quantity",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Quantity_NutrientsID",
                table: "Quantity",
                newName: "IX_Quantity_Nutrientsid");

            migrationBuilder.RenameColumn(
                name: "WorkerID",
                table: "Patient",
                newName: "Workerid");

            migrationBuilder.RenameColumn(
                name: "NutritionID",
                table: "Patient",
                newName: "Nutritionid");

            migrationBuilder.RenameColumn(
                name: "HealthDataID",
                table: "Patient",
                newName: "HealthDataid");

            migrationBuilder.RenameColumn(
                name: "PatientID",
                table: "Patient",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_WorkerID",
                table: "Patient",
                newName: "IX_Patient_Workerid");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_NutritionID",
                table: "Patient",
                newName: "IX_Patient_Nutritionid");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_HealthDataID",
                table: "Patient",
                newName: "IX_Patient_HealthDataid");

            migrationBuilder.RenameColumn(
                name: "QuantityID",
                table: "Nutrition",
                newName: "Quantityid");

            migrationBuilder.RenameColumn(
                name: "NutrientsID",
                table: "Nutrition",
                newName: "Nutrientsid");

            migrationBuilder.RenameColumn(
                name: "NutritionID",
                table: "Nutrition",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Nutrition_QuantityID",
                table: "Nutrition",
                newName: "IX_Nutrition_Quantityid");

            migrationBuilder.RenameIndex(
                name: "IX_Nutrition_NutrientsID",
                table: "Nutrition",
                newName: "IX_Nutrition_Nutrientsid");

            migrationBuilder.RenameColumn(
                name: "NutrientsID",
                table: "Nutrients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "HealthDataID",
                table: "HealthData",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrition_Nutrients_Nutrientsid",
                table: "Nutrition",
                column: "Nutrientsid",
                principalTable: "Nutrients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrition_Quantity_Quantityid",
                table: "Nutrition",
                column: "Quantityid",
                principalTable: "Quantity",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Patient_Worker_Workerid",
                table: "Patient",
                column: "Workerid",
                principalTable: "Worker",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Nutrients_Nutrientsid",
                table: "Quantity",
                column: "Nutrientsid",
                principalTable: "Nutrients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Patient_Patientid",
                table: "Worker",
                column: "Patientid",
                principalTable: "Patient",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_Nutrients_Nutrientsid",
                table: "Nutrition");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Quantity_Nutrients_Nutrientsid",
                table: "Quantity");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Patient_Patientid",
                table: "Worker");

            migrationBuilder.RenameColumn(
                name: "Patientid",
                table: "Worker",
                newName: "PatientID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Worker",
                newName: "WorkerID");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_Patientid",
                table: "Worker",
                newName: "IX_Worker_PatientID");

            migrationBuilder.RenameColumn(
                name: "Nutrientsid",
                table: "Quantity",
                newName: "NutrientsID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Quantity",
                newName: "QuantityID");

            migrationBuilder.RenameIndex(
                name: "IX_Quantity_Nutrientsid",
                table: "Quantity",
                newName: "IX_Quantity_NutrientsID");

            migrationBuilder.RenameColumn(
                name: "Workerid",
                table: "Patient",
                newName: "WorkerID");

            migrationBuilder.RenameColumn(
                name: "Nutritionid",
                table: "Patient",
                newName: "NutritionID");

            migrationBuilder.RenameColumn(
                name: "HealthDataid",
                table: "Patient",
                newName: "HealthDataID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Patient",
                newName: "PatientID");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_Workerid",
                table: "Patient",
                newName: "IX_Patient_WorkerID");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_Nutritionid",
                table: "Patient",
                newName: "IX_Patient_NutritionID");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_HealthDataid",
                table: "Patient",
                newName: "IX_Patient_HealthDataID");

            migrationBuilder.RenameColumn(
                name: "Quantityid",
                table: "Nutrition",
                newName: "QuantityID");

            migrationBuilder.RenameColumn(
                name: "Nutrientsid",
                table: "Nutrition",
                newName: "NutrientsID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Nutrition",
                newName: "NutritionID");

            migrationBuilder.RenameIndex(
                name: "IX_Nutrition_Quantityid",
                table: "Nutrition",
                newName: "IX_Nutrition_QuantityID");

            migrationBuilder.RenameIndex(
                name: "IX_Nutrition_Nutrientsid",
                table: "Nutrition",
                newName: "IX_Nutrition_NutrientsID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Nutrients",
                newName: "NutrientsID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "HealthData",
                newName: "HealthDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrition_Nutrients_NutrientsID",
                table: "Nutrition",
                column: "NutrientsID",
                principalTable: "Nutrients",
                principalColumn: "NutrientsID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrition_Quantity_QuantityID",
                table: "Nutrition",
                column: "QuantityID",
                principalTable: "Quantity",
                principalColumn: "QuantityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_HealthData_HealthDataID",
                table: "Patient",
                column: "HealthDataID",
                principalTable: "HealthData",
                principalColumn: "HealthDataID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Nutrition_NutritionID",
                table: "Patient",
                column: "NutritionID",
                principalTable: "Nutrition",
                principalColumn: "NutritionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Worker_WorkerID",
                table: "Patient",
                column: "WorkerID",
                principalTable: "Worker",
                principalColumn: "WorkerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantity_Nutrients_NutrientsID",
                table: "Quantity",
                column: "NutrientsID",
                principalTable: "Nutrients",
                principalColumn: "NutrientsID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Patient_PatientID",
                table: "Worker",
                column: "PatientID",
                principalTable: "Patient",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
