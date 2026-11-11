using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortPulse.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terminals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vessels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ImoNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GrossTonnage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vessels_ShippingLines_ShippingLineId",
                        column: x => x.ShippingLineId,
                        principalTable: "ShippingLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Berths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LengthMeters = table.Column<int>(type: "int", nullable: false),
                    MaxDraftMeters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Berths_Terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BerthBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BerthId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VesselId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DepartureTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BerthBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BerthBookings_Berths_BerthId",
                        column: x => x.BerthId,
                        principalTable: "Berths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BerthBookings_ShippingLines_ShippingLineId",
                        column: x => x.ShippingLineId,
                        principalTable: "ShippingLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BerthBookings_Vessels_VesselId",
                        column: x => x.VesselId,
                        principalTable: "Vessels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BerthBookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GateInTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    GateOutTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Containers_BerthBookings_BerthBookingId",
                        column: x => x.BerthBookingId,
                        principalTable: "BerthBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Containers_ShippingLines_ShippingLineId",
                        column: x => x.ShippingLineId,
                        principalTable: "ShippingLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomsInspections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InspectionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Outcome = table.Column<int>(type: "int", nullable: false),
                    Inspector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomsInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomsInspections_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemurrageCharges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalculatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FreeDays = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    DailyRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TotalCharge = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemurrageCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemurrageCharges_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BerthBookings_BerthId_ArrivalTime_DepartureTime",
                table: "BerthBookings",
                columns: new[] { "BerthId", "ArrivalTime", "DepartureTime" });

            migrationBuilder.CreateIndex(
                name: "IX_BerthBookings_ShippingLineId",
                table: "BerthBookings",
                column: "ShippingLineId");

            migrationBuilder.CreateIndex(
                name: "IX_BerthBookings_VesselId",
                table: "BerthBookings",
                column: "VesselId");

            migrationBuilder.CreateIndex(
                name: "IX_Berths_TerminalId_Code",
                table: "Berths",
                columns: new[] { "TerminalId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_BerthBookingId",
                table: "Containers",
                column: "BerthBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ContainerNumber",
                table: "Containers",
                column: "ContainerNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ShippingLineId",
                table: "Containers",
                column: "ShippingLineId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomsInspections_ContainerId_InspectionDate",
                table: "CustomsInspections",
                columns: new[] { "ContainerId", "InspectionDate" });

            migrationBuilder.CreateIndex(
                name: "IX_DemurrageCharges_ContainerId",
                table: "DemurrageCharges",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingLines_Code",
                table: "ShippingLines",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terminals_Code",
                table: "Terminals",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_ImoNumber",
                table: "Vessels",
                column: "ImoNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vessels_ShippingLineId",
                table: "Vessels",
                column: "ShippingLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomsInspections");

            migrationBuilder.DropTable(
                name: "DemurrageCharges");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "BerthBookings");

            migrationBuilder.DropTable(
                name: "Berths");

            migrationBuilder.DropTable(
                name: "Vessels");

            migrationBuilder.DropTable(
                name: "Terminals");

            migrationBuilder.DropTable(
                name: "ShippingLines");
        }
    }
}
