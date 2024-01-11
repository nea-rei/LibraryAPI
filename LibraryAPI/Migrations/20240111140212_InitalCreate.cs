using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Library");

            migrationBuilder.CreateTable(
                name: "Author",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorIdentity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<int>(type: "int", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    Available = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LibraryUser",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LibraryCard = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                schema: "Library",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Author_AuthorsId",
                        column: x => x.AuthorsId,
                        principalSchema: "Library",
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Book_BooksId",
                        column: x => x.BooksId,
                        principalSchema: "Library",
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    LibraryUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loan_LibraryUser_LibraryUserId",
                        column: x => x.LibraryUserId,
                        principalSchema: "Library",
                        principalTable: "LibraryUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Library",
                table: "Author",
                columns: new[] { "Id", "AuthorIdentity", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 456789, "Anna", "Burns" },
                    { 2, 456123, "Bo", "Tag" },
                    { 3, 789464, "Tom", "Pip" }
                });

            migrationBuilder.InsertData(
                schema: "Library",
                table: "Book",
                columns: new[] { "Id", "Available", "ISBN", "Rating", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, "False", 54612312, null, 2010, "Sea" },
                    { 2, "False", 54612342, null, 2000, "Midnight" },
                    { 3, "False", 54122312, null, 1993, "Happy" }
                });

            migrationBuilder.InsertData(
                schema: "Library",
                table: "LibraryUser",
                columns: new[] { "Id", "FirstName", "LastName", "LibraryCard" },
                values: new object[,]
                {
                    { 1, "Sandra", "Woo", 678345 },
                    { 2, "Carl", "Loos", 678346 }
                });

            migrationBuilder.InsertData(
                schema: "Library",
                table: "Loan",
                columns: new[] { "Id", "Available", "BookId", "LibraryUserId", "LoanDate", "Rating", "ReturnDate" },
                values: new object[,]
                {
                    { 1, false, 1, 1, new DateTime(2024, 1, 7, 14, 2, 12, 49, DateTimeKind.Utc).AddTicks(3318), 0.0, null },
                    { 2, false, 2, 1, new DateTime(2024, 1, 9, 14, 2, 12, 49, DateTimeKind.Utc).AddTicks(3342), 0.0, null },
                    { 3, false, 3, 2, new DateTime(2024, 1, 8, 14, 2, 12, 49, DateTimeKind.Utc).AddTicks(3354), 0.0, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_AuthorIdentity",
                schema: "Library",
                table: "Author",
                column: "AuthorIdentity",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                schema: "Library",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryUser_LibraryCard",
                schema: "Library",
                table: "LibraryUser",
                column: "LibraryCard",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LibraryUserId",
                schema: "Library",
                table: "Loan",
                column: "LibraryUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "Loan",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "Author",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "Book",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "LibraryUser",
                schema: "Library");
        }
    }
}
