using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsManager.Console.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Company = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastContactedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTags",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTags", x => new { x.ContactId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ContactTags_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Company", "CreatedAt", "Email", "FirstName", "LastContactedAt", "LastName", "Notes", "Phone" },
                values: new object[,]
                {
                    { 1, "Tech Corp", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.smith@example.com", "John", new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", "CEO of Tech Corp", "(555) 123-4567" },
                    { 2, "Design Studio", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarah.j@email.com", "Sarah", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Johnson", "Freelance designer", "(555) 234-5678" },
                    { 3, "Global Industries", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mbrown@company.com", "Michael", new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brown", "Project manager", "(555) 345-6789" },
                    { 4, "Marketing Plus", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.davis@mail.com", "Emily", new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Davis", "Marketing specialist", "(555) 456-7890" },
                    { 5, "Wilson & Associates", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.w@example.org", "David", null, "Wilson", "Legal consultant", "(555) 567-8901" },
                    { 6, "Health First", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jen.martinez@email.com", "Jennifer", new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Martinez", "Healthcare provider", "(555) 678-9012" },
                    { 7, "Tech Solutions", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rtaylor@tech.com", "Robert", new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taylor", "Software engineer", "(555) 789-0123" },
                    { 8, "Creative Agency", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lisa.a@example.com", "Lisa", new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anderson", "Creative director", "(555) 890-1234" },
                    { 9, "Business Ventures", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wthomas@business.com", "William", new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thomas", "Entrepreneur", "(555) 901-2345" },
                    { 10, "Education Group", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "amanda.garcia@mail.com", "Amanda", new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Garcia", "Education coordinator", "(555) 012-3456" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Family" },
                    { 2, "Work" },
                    { 3, "Friends" },
                    { 4, "VIP" },
                    { 5, "Newsletter" }
                });

            migrationBuilder.InsertData(
                table: "ContactTags",
                columns: new[] { "ContactId", "TagId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 4 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 1 },
                    { 7, 3 },
                    { 8, 2 },
                    { 8, 4 },
                    { 9, 2 },
                    { 10, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactTags_TagId",
                table: "ContactTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactTags");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
