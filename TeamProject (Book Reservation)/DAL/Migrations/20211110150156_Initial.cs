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
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
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
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
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
                name: "Author_Books",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author_Books", x => new { x.BookID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_Author_Books_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Author_Books_BookTemplates_BookID",
                        column: x => x.BookID,
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
                    Conditon = table.Column<int>(type: "int", nullable: false),
                    BookOwnerId = table.Column<int>(type: "int", nullable: false),
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
                name: "Book_Genres",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Genres", x => new { x.GenreID, x.BookID });
                    table.ForeignKey(
                        name: "FK_Book_Genres_BookTemplates_BookID",
                        column: x => x.BookID,
                        principalTable: "BookTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Genres_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "Id",
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
                name: "EReaderInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    EreaderOwnerId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_EReaderInstances_Users_EreaderOwnerId",
                        column: x => x.EreaderOwnerId,
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
                name: "BookCollection_Books",
                columns: table => new
                {
                    BookCollectionID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCollection_Books", x => new { x.BookID, x.BookCollectionID });
                    table.ForeignKey(
                        name: "FK_BookCollection_Books_BookCollections_BookCollectionID",
                        column: x => x.BookCollectionID,
                        principalTable: "BookCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCollection_Books_BookTemplates_BookID",
                        column: x => x.BookID,
                        principalTable: "BookTemplates",
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
                    EReaderID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_EBookInstances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "Date", nullable: false),
                    DateTill = table.Column<DateTime>(type: "Date", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EReaderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_EReaderInstances_EReaderID",
                        column: x => x.EReaderID,
                        principalTable: "EReaderInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation_BookInstances",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false),
                    BookInstanceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation_BookInstances", x => new { x.ReservationID, x.BookInstanceID });
                    table.ForeignKey(
                        name: "FK_Reservation_BookInstances_BookInstances_BookInstanceID",
                        column: x => x.BookInstanceID,
                        principalTable: "BookInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_BookInstances_Reservations_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Description", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "William Shakespeare was an English playwright, poet, and actor, widely regarded as the greatest writer in the English language and the world's greatest dramatist.", "William", "Shakespeare" },
                    { 2, "Mary Wollstonecraft Shelley (30 August 1797 – 1 February 1851) was an English novelist who wrote the Gothic novel Frankenstein; or, The Modern Prometheus (1818), which is considered an early example of science fiction.", "Mary", "Shelley" },
                    { 3, "Charlotte Brontë (21 April 1816 – 31 March 1855) was an English novelist and poet, the eldest of the three Brontë sisters who survived into adulthood and whose novels became classics of English literature.", "Charlotte", "Brontë" },
                    { 4, "Emily Jane Brontë (30 July 1818 – 19 December 1848) was an English novelist and poet who is best known for her only novel, Wuthering Heights, now considered a classic of English literature.", "Emily", "Brontë" },
                    { 5, "Jules Gabriel Verne (8 February 1828 – 24 March 1905) was a French novelist, poet, and playwright. His collaboration with the publisher Pierre-Jules Hetzel led to the creation of the Voyages extraordinaires, a series of bestselling adventure novels.", "Jules", "Verne" },
                    { 6, "Jane Austen (16 December 1775 – 18 July 1817) was an English novelist known primarily for her six major novels, which interpret, critique and comment upon the British landed gentry at the end of the 18th century.", "Jane", "Austen" },
                    { 7, "Charles John Huffam Dickens (7 February 1812 – 9 June 1870) was an English writer and social critic. He created some of the world's best-known fictional characters and is regarded by many as the greatest novelist of the Victorian era.", "Charles", "Dickens" },
                    { 8, "Edgar Allan Poe (born Edgar Poe; January 19, 1809 – October 7, 1849) was an American writer, poet, editor, and literary critic. Poe is best known for his poetry and short stories, particularly his tales of mystery and the macabre. ", "Edgar Allan", "Poe" },
                    { 9, "Douglas Noël Adams was an English author, comic radio dramatist, and musician. He is best known as the author of the Hitchhiker's Guide to the Galaxy series. Hitchhiker's began on radio, and developed into a trilogy of five books (which sold more than fifteen million copies during his lifetime) as well as a television series, a comic book series, a computer game, and a feature film that was completed after Adams' death. The series has also been adapted for live theatre using various scripts; the earliest such productions used material newly written by Adams. He was known to some fans as Bop Ad (after his illegible signature), or by his initials \"DNA\".", "Douglas", "Adams" },
                    { 10, "Eric Arthur Blair, better known by his pen name George Orwell, was an English author and journalist. His work is marked by keen intelligence and wit, a profound awareness of social injustice, an intense opposition to totalitarianism, a passion for clarity in language, and a belief in democratic socialism.", "George", "Orwell" },
                    { 11, "Dan Brown is the author of numerous #1 bestselling novels, including The Da Vinci Code, which has become one of the best selling novels of all time as well as the subject of intellectual debate among readers and scholars. Brown’s novels are published in 52 languages around the world with 200 million copies in print.", "Dan", "Brown" },
                    { 12, "Antoine de Saint-Exupéry was born in Lyons on June 29, 1900. He flew for the first time at the age of twelve, at the Ambérieu airfield, and it was then that he became determined to be a pilot. He kept that ambition even after moving to a school in Switzerland and while spending summer vacations at the family's château at Saint-Maurice-de-Rémens, in eastern France. (The house at Saint-Maurice appears again and again in Saint-Exupéry's writing.)", "Antoine", "de Saint-Exupéry" }
                });

            migrationBuilder.InsertData(
                table: "BookTemplates",
                columns: new[] { "Id", "DateOfRelease", "Description", "Discriminator", "Format", "ISBN", "Language", "MemorySize", "PageCount", "Title" },
                values: new object[] { 9, new DateTime(1591, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shakespeare's immortal drama tells the story of star-crossed lovers, rival dynasties and bloody revenge. Romeo and Juliet is a hymn to youth and the thrill of forbidden love, charged with sexual passion and violence, but also a warning of death: a dazzling combination of bawdy comedy and high tragedy.", "EBook", 0, "9780141920252", 0, 1024, 320, "Romeo and Juliet" });

            migrationBuilder.InsertData(
                table: "BookTemplates",
                columns: new[] { "Id", "DateOfRelease", "Description", "Discriminator", "ISBN", "Language", "PageCount", "Title" },
                values: new object[,]
                {
                    { 8, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Since its immediate success in 1813, Pride and Prejudice has remained one of the most popular novels in the English language. Jane Austen called this brilliant work \"her own darling child\" and its vivacious heroine, Elizabeth Bennet, \" as delightful a creature as ever appeared in print.\" The romantic clash between the opinionated Elizabeth and her proud beau, Mr. Darcy, is a splendid performance of civilized sparring. And Jane Austen's radiant wit sparkles as her characters dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners of Regency England.", "Book", "9780679783268", 0, 279, "Pride and Prejudice" },
                    { 7, new DateTime(1945, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "A farm is taken over by its overworked, mistreated animals. With flaming idealism and stirring slogans, they set out to create a paradise of progress, justice, and equality. Thus the stage is set for one of the most telling satiric fables ever penned –a razor-edged fairy tale for grown-ups that records the evolution from revolution against tyranny to a totalitarianism just as terrible. When Animal Farm was first published, Stalinist Russia was seen as its target. Today it is devastatingly clear that wherever and whenever freedom is attacked,under whatever banner,the cutting clarity and savage comedy of George Orwell’s masterpiece have a meaning and message still ferociously fresh.", "Book", "9780451526342", 0, 141, "Animal Farm" },
                    { 5, new DateTime(1943, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "A pilot stranded in the desert awakes one morning to see, standing before him, the most extraordinary little fellow. \"Please,\" asks the stranger, \"draw me a sheep.\" And the pilot realizes that when life's events are too difficult to understand, there is no choice but to succumb to their mysteries. He pulls out pencil and paper... And thus begins this wise and enchanting fable that, in teaching the secret of what is really important in life, has changed forever the world for its readers.", "Book", "9783140464079", 0, 96, "The Little Prince" },
                    { 6, new DateTime(2006, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "While in Paris, Harvard symbologist Robert Langdon is awakened by a phone call in the dead of the night. The elderly curator of the Louvre has been murdered inside the museum, his body covered in baffling symbols. As Langdon and gifted French cryptologist Sophie Neveu sort through the bizarre riddles, they are stunned to discover a trail of clues hidden in the works of Leonardo da Vinci—clues visible for all to see and yet ingeniously disguised by the painter.", "Book", "9780307277671 ", 0, 489, "The Da Vinci Code" },
                    { 3, new DateTime(1591, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shakespeare's immortal drama tells the story of star-crossed lovers, rival dynasties and bloody revenge. Romeo and Juliet is a hymn to youth and the thrill of forbidden love, charged with sexual passion and violence, but also a warning of death: a dazzling combination of bawdy comedy and high tragedy.", "Book", "9780007902361", 0, 160, "Romeo and Juliet" },
                    { 2, new DateTime(1600, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Considered one of Shakespeare′s most rich and enduring plays, the depiction of its hero Hamlet as he vows to avenge the murder of his father by his brother Claudius is both powerful and complex. As Hamlet tries to find out the truth of the situation, his troubled relationship with his mother comes to the fore, as do the paradoxes in his personality. A play of carefully crafted conflict and tragedy, Shakespeare′s intricate dialogue continues to fascinate audiences to this day.", "Book", "9780007902347", 0, 192, "Hamlet" },
                    { 1, new DateTime(1818, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Victor Frankenstein, a brilliant but wayward scientist, builds a human from dead flesh. Horrified at what he has done, he abandons his creation. The hideous creature learns language and becomes civilized but society rejects him. Spurned, he seeks vengeance on his creator. So begins a cycle of destruction, with Frankenstein and his 'monster' pursuing each other to the extremes of nature until all vestiges of their humanity are lost.", "Book", "9781509827756", 0, 280, "Frankenstein" },
                    { 4, new DateTime(2007, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shakespeare's immortal drama tells the story of star-crossed lovers, rival dynasties and bloody revenge. Romeo and Juliet is a hymn to youth and the thrill of forbidden love, charged with sexual passion and violence, but also a warning of death: a dazzling combination of bawdy comedy and high tragedy.", "Book", "9780671461492", 0, 193, "The Hitchhiker's Guide to the Galaxy" }
                });

            migrationBuilder.InsertData(
                table: "EReaderTemplates",
                columns: new[] { "Id", "CompanyMake", "MemoryInMB", "Model" },
                values: new object[,]
                {
                    { 1, "PocketBook", 16000, "632 Touch HD 3" },
                    { 2, "ONYX BOOX", 32000, "Nova 3" },
                    { 3, "PocketBook", 32000, "1040 InkPad X" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                column: "Id",
                values: new object[]
                {
                    5,
                    3,
                    7,
                    2,
                    4,
                    1
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "HashedPassword", "IsAdmin", "Name", "Surname" },
                values: new object[,]
                {
                    { 3, "evee@gmail.com", "", false, "Eve", "Third" },
                    { 4, "jack@gmail.com", "", false, "Jack", "Fourth" },
                    { 1, "alice@gmail.com", "", true, "Alice", "First" },
                    { 2, "bob@gmail.com", "", false, "Bob", "Second" },
                    { 5, "malory@gmail.com", "", false, "Malory", "Fifth" }
                });

            migrationBuilder.InsertData(
                table: "Author_Books",
                columns: new[] { "AuthorID", "BookID" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 1, 2 },
                    { 1, 9 },
                    { 1, 3 },
                    { 6, 8 },
                    { 9, 4 },
                    { 12, 5 },
                    { 10, 7 },
                    { 11, 6 }
                });

            migrationBuilder.InsertData(
                table: "BookCollections",
                columns: new[] { "Id", "CreationDate", "Description", "Title", "UserId" },
                values: new object[] { 1, new DateTime(2021, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Just classics.", "Classics", 2 });

            migrationBuilder.InsertData(
                table: "BookInstances",
                columns: new[] { "Id", "BookOwnerId", "BookTemplateID", "Conditon" },
                values: new object[,]
                {
                    { 1, 1, 1, 4 },
                    { 8, 3, 8, 5 },
                    { 6, 5, 6, 2 },
                    { 5, 4, 5, 3 },
                    { 4, 3, 4, 0 },
                    { 3, 2, 3, 0 },
                    { 2, 1, 2, 4 },
                    { 7, 5, 7, 3 }
                });

            migrationBuilder.InsertData(
                table: "Book_Genres",
                columns: new[] { "BookID", "GenreID" },
                values: new object[,]
                {
                    { 5, 2 },
                    { 4, 1 },
                    { 7, 2 },
                    { 9, 7 },
                    { 3, 7 },
                    { 2, 7 },
                    { 8, 5 },
                    { 6, 3 },
                    { 1, 7 }
                });

            migrationBuilder.InsertData(
                table: "EReaderInstances",
                columns: new[] { "Id", "Description", "EReaderTemplateID", "EreaderOwnerId" },
                values: new object[,]
                {
                    { 1, null, 1, 1 },
                    { 3, null, 2, 3 },
                    { 2, null, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "DateFrom", "DateTill", "EReaderID", "UserID" },
                values: new object[,]
                {
                    { 7, new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 8, new DateTime(2021, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 5, new DateTime(2021, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 4, new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 9, new DateTime(2021, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 3, new DateTime(2021, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 2, new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 1, new DateTime(2021, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 6, new DateTime(2021, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookTemplateID", "Content", "CreationDate", "StarsAmmount", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Best", new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 2, 1, "Great!!", new DateTime(2021, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2 },
                    { 3, 1, "huh", new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookTemplateID", "Content", "CreationDate", "StarsAmmount", "UserID" },
                values: new object[] { 4, 8, "Changed my life", new DateTime(2021, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4 });

            migrationBuilder.InsertData(
                table: "BookCollection_Books",
                columns: new[] { "BookCollectionID", "BookID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "EBookInstances",
                columns: new[] { "Id", "EBookTemplateID", "EReaderID", "UserId" },
                values: new object[,]
                {
                    { 1, 9, 1, null },
                    { 2, 9, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Reservation_BookInstances",
                columns: new[] { "BookInstanceID", "ReservationID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 8, 9 },
                    { 1, 4 },
                    { 2, 5 },
                    { 7, 8 },
                    { 3, 7 },
                    { 5, 6 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "DateFrom", "DateTill", "EReaderID", "UserID" },
                values: new object[] { 10, new DateTime(2021, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Author_Books_AuthorID",
                table: "Author_Books",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Genres_BookID",
                table: "Book_Genres",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookCollection_Books_BookCollectionID",
                table: "BookCollection_Books",
                column: "BookCollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookCollections_UserId",
                table: "BookCollections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInstances_BookTemplateID",
                table: "BookInstances",
                column: "BookTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_EBookInstances_EBookTemplateID",
                table: "EBookInstances",
                column: "EBookTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_EBookInstances_EReaderID",
                table: "EBookInstances",
                column: "EReaderID");

            migrationBuilder.CreateIndex(
                name: "IX_EBookInstances_UserId",
                table: "EBookInstances",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EReaderInstances_EreaderOwnerId",
                table: "EReaderInstances",
                column: "EreaderOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_EReaderInstances_EReaderTemplateID",
                table: "EReaderInstances",
                column: "EReaderTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BookInstances_BookInstanceID",
                table: "Reservation_BookInstances",
                column: "BookInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EReaderID",
                table: "Reservations",
                column: "EReaderID");

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
                name: "Author_Books");

            migrationBuilder.DropTable(
                name: "Book_Genres");

            migrationBuilder.DropTable(
                name: "BookCollection_Books");

            migrationBuilder.DropTable(
                name: "EBookInstances");

            migrationBuilder.DropTable(
                name: "Reservation_BookInstances");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "BookCollections");

            migrationBuilder.DropTable(
                name: "BookInstances");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "BookTemplates");

            migrationBuilder.DropTable(
                name: "EReaderInstances");

            migrationBuilder.DropTable(
                name: "EReaderTemplates");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
