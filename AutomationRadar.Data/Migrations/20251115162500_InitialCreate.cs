using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationRadar.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OCCUPATIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    Sector = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCCUPATIONS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AUTOMATION_RISKS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    OccupationId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RiskLevel = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    HorizonYears = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Justification = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    Source = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTOMATION_RISKS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTOMATIONRISK_OCCUPATION",
                        column: x => x.OccupationId,
                        principalTable: "OCCUPATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CAREER_TRANSITIONS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FromOccupationId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ToOccupationId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RecommendedActions = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    Priority = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAREER_TRANSITIONS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRANSITION_FROM",
                        column: x => x.FromOccupationId,
                        principalTable: "OCCUPATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TRANSITION_TO",
                        column: x => x.ToOccupationId,
                        principalTable: "OCCUPATIONS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AUTOMATION_RISKS_OccupationId",
                table: "AUTOMATION_RISKS",
                column: "OccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_CAREER_TRANSITIONS_FromOccupationId",
                table: "CAREER_TRANSITIONS",
                column: "FromOccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_CAREER_TRANSITIONS_ToOccupationId",
                table: "CAREER_TRANSITIONS",
                column: "ToOccupationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUTOMATION_RISKS");

            migrationBuilder.DropTable(
                name: "CAREER_TRANSITIONS");

            migrationBuilder.DropTable(
                name: "OCCUPATIONS");
        }
    }
}
