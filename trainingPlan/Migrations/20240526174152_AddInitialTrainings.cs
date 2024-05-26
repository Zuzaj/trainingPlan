using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trainingPlan.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialTrainings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanViews_Users_UserId",
                table: "PlanViews");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_PlanViews_PlanViewId",
                table: "Trainings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PlanViews",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanViews_Users_UserId",
                table: "PlanViews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_PlanViews_PlanViewId",
                table: "Trainings",
                column: "PlanViewId",
                principalTable: "PlanViews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanViews_Users_UserId",
                table: "PlanViews");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_PlanViews_PlanViewId",
                table: "Trainings");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PlanViews",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanViews_Users_UserId",
                table: "PlanViews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_PlanViews_PlanViewId",
                table: "Trainings",
                column: "PlanViewId",
                principalTable: "PlanViews",
                principalColumn: "Id");
        }
    }
}
