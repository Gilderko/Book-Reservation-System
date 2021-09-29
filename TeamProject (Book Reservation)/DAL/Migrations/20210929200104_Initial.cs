using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    DateOfRelease = table.Column<DateTime>(type: "Date", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemorySize = table.Column<int>(type: "int", nullable: true),
                    Format = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EReaderTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CompanyMake = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    MemoryInMB = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EReaderTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HashedPassword = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBookTemplate",
                columns: table => new
                {
                    AuthorsBooksId = table.Column<int>(type: "int", nullable: false),
                    AuthorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBookTemplate", x => new { x.AuthorsBooksId, x.AuthorsId });
                    table.ForeignKey(
                        name: "FK_AuthorBookTemplate_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBookTemplate_BookTemplates_AuthorsBooksId",
                        column: x => x.AuthorsBooksId,
                        principalTable: "BookTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTemplateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookInstances_BookTemplates_BookTemplateID",
                        column: x => x.BookTemplateID,
                        principalTable: "BookTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EReaderInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EReaderTemplateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EReaderInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EReaderInstances_EReaderTemplates_EReaderTemplateID",
                        column: x => x.EReaderTemplateID,
                        principalTable: "EReaderTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTemplateGenre",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    GenresGenreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTemplateGenre", x => new { x.BooksId, x.GenresGenreID });
                    table.ForeignKey(
                        name: "FK_BookTemplateGenre_BookTemplates_BooksId",
                        column: x => x.BooksId,
                        principalTable: "BookTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTemplateGenre_Genres_GenresGenreID",
                        column: x => x.GenresGenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCollections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StarsAmmount = table.Column<int>(type: "int", nullable: false),
                    BookTemplateID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_BookTemplates_BookTemplateID",
                        column: x => x.BookTemplateID,
                        principalTable: "BookTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EBookInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EBookTemplateID = table.Column<int>(type: "int", nullable: false),
                    EReaderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBookInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EBookInstances_BookTemplates_EBookTemplateID",
                        column: x => x.EBookTemplateID,
                        principalTable: "BookTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EBookInstances_EReaderInstances_EReaderID",
                        column: x => x.EReaderID,
                        principalTable: "EReaderInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "Date", nullable: false),
                    DateTill = table.Column<DateTime>(type: "Date", nullable: false),
                    BookInstanceID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ERedeaderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_BookInstances_BookInstanceID",
                        column: x => x.BookInstanceID,
                        principalTable: "BookInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_EReaderInstances_ERedeaderID",
                        column: x => x.ERedeaderID,
                        principalTable: "EReaderInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCollectionBookTemplate",
                columns: table => new
                {
                    BookCollectionId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCollectionBookTemplate", x => new { x.BookCollectionId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_BookCollectionBookTemplate_BookCollections_BookCollectionId",
                        column: x => x.BookCollectionId,
                        principalTable: "BookCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCollectionBookTemplate_BookTemplates_BooksId",
                        column: x => x.BooksId,
                        principalTable: "BookTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookTemplate_AuthorsId",
                table: "AuthorBookTemplate",
                column: "AuthorsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCollectionBookTemplate_BooksId",
                table: "BookCollectionBookTemplate",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCollections_UserId",
                table: "BookCollections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInstances_BookTemplateID",
                table: "BookInstances",
                column: "BookTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_BookTemplateGenre_GenresGenreID",
                table: "BookTemplateGenre",
                column: "GenresGenreID");

            migrationBuilder.CreateIndex(
                name: "IX_EBookInstances_EBookTemplateID",
                table: "EBookInstances",
                column: "EBookTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_EBookInstances_EReaderID",
                table: "EBookInstances",
                column: "EReaderID");

            migrationBuilder.CreateIndex(
                name: "IX_EReaderInstances_EReaderTemplateID",
                table: "EReaderInstances",
                column: "EReaderTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BookInstanceID",
                table: "Reservations",
                column: "BookInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ERedeaderID",
                table: "Reservations",
                column: "ERedeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserID",
                table: "Reservations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookTemplateID",
                table: "Reviews",
                column: "BookTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBookTemplate");

            migrationBuilder.DropTable(
                name: "BookCollectionBookTemplate");

            migrationBuilder.DropTable(
                name: "BookTemplateGenre");

            migrationBuilder.DropTable(
                name: "EBookInstances");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "BookCollections");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "BookInstances");

            migrationBuilder.DropTable(
                name: "EReaderInstances");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BookTemplates");

            migrationBuilder.DropTable(
                name: "EReaderTemplates");
        }
    }
}
