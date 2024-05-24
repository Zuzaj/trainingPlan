using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainingPlan.Migrations
{
    /// <inheritdoc />
    public partial class AddPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Plans_PlanId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_PlanId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Trainings");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Plans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlanTrainings",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTrainings", x => new { x.PlanId, x.TrainingsId });
                    table.ForeignKey(
                        name: "FK_PlanTrainings_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanTrainings_Trainings_TrainingsId",
                        column: x => x.TrainingsId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanTrainings_TrainingsId",
                table: "PlanTrainings",
                column: "TrainingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanTrainings");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Trainings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_PlanId",
                table: "Trainings",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Plans_PlanId",
                table: "Trainings",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
