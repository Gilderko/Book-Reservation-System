using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ReviewsAddedToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_BookInstances_BookInstanceID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_EReaderInstances_ERedeaderID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ERedeaderID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ERedeaderID",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "BookInstanceID",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EReaderID",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BookTemplates",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Authors",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

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
                    { 8, "Edgar Allan Poe (born Edgar Poe; January 19, 1809 – October 7, 1849) was an American writer, poet, editor, and literary critic. Poe is best known for his poetry and short stories, particularly his tales of mystery and the macabre. ", "Edgar Allan", "Poe" }
                });

            migrationBuilder.InsertData(
                table: "BookTemplates",
                columns: new[] { "Id", "DateOfRelease", "Description", "Discriminator", "Format", "ISBN", "Language", "MemorySize", "PageCount", "Title" },
                values: new object[] { 4, new DateTime(1591, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shakespeare's immortal drama tells the story of star-crossed lovers, rival dynasties and bloody revenge. Romeo and Juliet is a hymn to youth and the thrill of forbidden love, charged with sexual passion and violence, but also a warning of death: a dazzling combination of bawdy comedy and high tragedy.", "EBookTemplate", 0, "9780141920252", 0, 1024, 320, "Romeo and Juliet" });

            migrationBuilder.InsertData(
                table: "BookTemplates",
                columns: new[] { "Id", "DateOfRelease", "Description", "Discriminator", "ISBN", "Language", "PageCount", "Title" },
                values: new object[,]
                {
                    { 3, new DateTime(1591, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shakespeare's immortal drama tells the story of star-crossed lovers, rival dynasties and bloody revenge. Romeo and Juliet is a hymn to youth and the thrill of forbidden love, charged with sexual passion and violence, but also a warning of death: a dazzling combination of bawdy comedy and high tragedy.", "BookTemplate", "9780007902361", 0, 160, "Romeo and Juliet" },
                    { 2, new DateTime(1600, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Considered one of Shakespeare′s most rich and enduring plays, the depiction of its hero Hamlet as he vows to avenge the murder of his father by his brother Claudius is both powerful and complex. As Hamlet tries to find out the truth of the situation, his troubled relationship with his mother comes to the fore, as do the paradoxes in his personality. A play of carefully crafted conflict and tragedy, Shakespeare′s intricate dialogue continues to fascinate audiences to this day.", "BookTemplate", "9780007902347", 0, 192, "Hamlet" },
                    { 1, new DateTime(1818, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Victor Frankenstein, a brilliant but wayward scientist, builds a human from dead flesh. Horrified at what he has done, he abandons his creation. The hideous creature learns language and becomes civilized but society rejects him. Spurned, he seeks vengeance on his creator. So begins a cycle of destruction, with Frankenstein and his 'monster' pursuing each other to the extremes of nature until all vestiges of their humanity are lost.", "BookTemplate", "9781509827756", 0, 280, "Frankenstein" }
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
                column: "GenreID",
                values: new object[]
                {
                    2,
                    4
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "HashedPassword", "IsAdmin", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "alice@gmail.com", "", true, "Alice", "First" },
                    { 2, "bob@gmail.com", "", false, "Bob", "Second" },
                    { 3, "evee@gmail.com", "", false, "Eve", "Third" }
                });

            migrationBuilder.InsertData(
                table: "BookCollections",
                columns: new[] { "Id", "CreationDate", "Description", "Title", "UserId" },
                values: new object[] { 1, new DateTime(2021, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Just classics.", "Classics", 2 });

            migrationBuilder.InsertData(
                table: "BookInstances",
                columns: new[] { "Id", "BookTemplateID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "EReaderInstances",
                columns: new[] { "Id", "EReaderTemplateID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookTemplateID", "Content", "CreationDate", "StarsAmmount", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Best", new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 2, 1, "Great!!", new DateTime(2021, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 3, 1, "huh", new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "EBookInstances",
                columns: new[] { "Id", "EBookTemplateID", "EReaderID" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "BookInstanceID", "DateFrom", "DateTill", "EReaderID", "UserID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 4, 1, new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 2, 2, new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 5, 2, new DateTime(2021, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 3, 3, new DateTime(2021, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EReaderID",
                table: "Reservations",
                column: "EReaderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_BookInstances_BookInstanceID",
                table: "Reservations",
                column: "BookInstanceID",
                principalTable: "BookInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_EReaderInstances_EReaderID",
                table: "Reservations",
                column: "EReaderID",
                principalTable: "EReaderInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_BookInstances_BookInstanceID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_EReaderInstances_EReaderID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_EReaderID",
                table: "Reservations");

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BookCollections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EBookInstances",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EBookInstances",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EReaderInstances",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EReaderInstances",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EReaderTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookInstances",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookInstances",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookInstances",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookTemplates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EReaderInstances",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EReaderTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EReaderTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "EReaderID",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "BookInstanceID",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ERedeaderID",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BookTemplates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Authors",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ERedeaderID",
                table: "Reservations",
                column: "ERedeaderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_BookInstances_BookInstanceID",
                table: "Reservations",
                column: "BookInstanceID",
                principalTable: "BookInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_EReaderInstances_ERedeaderID",
                table: "Reservations",
                column: "ERedeaderID",
                principalTable: "EReaderInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
