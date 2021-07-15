using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace TimePlannerUpdated.Default.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartingTime = table.Column<DateTimeOffset>(nullable: false),
                    AutoOffset = table.Column<TimeSpan>(nullable: false),
                    MinimalAutoRemindersCount = table.Column<int>(nullable: false),
                    AutoAddMinutes = table.Column<int>(nullable: false),
                    AutoAddHours = table.Column<int>(nullable: false),
                    AutoAddDays = table.Column<int>(nullable: false),
                    AutoAddMonths = table.Column<int>(nullable: false),
                    AutoAddYears = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartingTime = table.Column<DateTimeOffset>(nullable: false),
                    AutoOffset = table.Column<TimeSpan>(nullable: false),
                    MinimalAutoRemindersCount = table.Column<int>(nullable: false),
                    AutoAddMinutes = table.Column<int>(nullable: false),
                    AutoAddHours = table.Column<int>(nullable: false),
                    AutoAddDays = table.Column<int>(nullable: false),
                    AutoAddMonths = table.Column<int>(nullable: false),
                    AutoAddYears = table.Column<int>(nullable: false),
                    TaskListId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTask_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reminder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Done = table.Column<bool>(nullable: false),
                    Deadline = table.Column<DateTimeOffset>(nullable: false),
                    IsOffsetReminder = table.Column<bool>(nullable: false),
                    TaskListId = table.Column<int>(nullable: true),
                    TaskListId1 = table.Column<int>(nullable: true),
                    UserTaskId = table.Column<int>(nullable: true),
                    UserTaskId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminder_TaskList_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reminder_TaskList_TaskListId1",
                        column: x => x.TaskListId1,
                        principalTable: "TaskList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reminder_UserTask_UserTaskId",
                        column: x => x.UserTaskId,
                        principalTable: "UserTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reminder_UserTask_UserTaskId1",
                        column: x => x.UserTaskId1,
                        principalTable: "UserTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_TaskListId",
                table: "Reminder",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_TaskListId1",
                table: "Reminder",
                column: "TaskListId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_UserTaskId",
                table: "Reminder",
                column: "UserTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_UserTaskId1",
                table: "Reminder",
                column: "UserTaskId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_TaskListId",
                table: "UserTask",
                column: "TaskListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminder");

            migrationBuilder.DropTable(
                name: "UserTask");

            migrationBuilder.DropTable(
                name: "TaskList");
        }
    }
}
