using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseProjectLiteMK5.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "Title", "UserId" },
                values: new object[] { "46d44d6d-7716-4aa6-a190-3c8a8aa43110", "OBS! Detta kommer jag ej göra och jag uppmanar ingen annan att göra det heller!!OBS!\r\n\r\nSatt och funderade lite bara, skulle denna plan fungera?\r\n\r\nMan är 4st. Alla har pistol, svarta kläder, rånaluva, skyddsväst, mobil, nightvison (Stavning?) och handklovar.\r\n\r\nMobilen: Alla har en mobil med öronsnacka. Ljudlös och man har samtal mellan alla 4. Självklart så har man kontantkort och en billig mobil som man kan dumpa efter rånet.\r\n\r\nDetta sker efter mörkrets inbrott, aldeles innan stängning och när det är folk tomt.\r\nMan ställer bilen ca 1km från kiosken. En lägger sig utanför kisoken i en buske så att man har uppsikt fall om det skulle komma någon. Den andre ställer sig så att han siktar mot dörren. En tar kasörskan och den sista kollar på lagret.\r\nMan slänger ner kasörskan på marken så att hon inte syns ifrån fönstren. Sätter på henne/han handklovar både på fötterna och händerna. Sätter på munkavel + ögonbindel med. Knipsar av alla telefon sladdar. Snor hennes/hans mobil. Man tömmer kassan på pengar. Slår sönder videobanden om det finns video/ljud övervakning. Släcker lamporna låser dörren så att det ser ut som att kiosken har stängt. Sedan så springer man allt var man har till bilen. Kör till den andra flyktbilen som står ca 5mil från den första. Den andra bilen står vid en sjö så att man kan dumpa den första bilen i sjön. Kör ca 10 mil och där står en tredje bil. Man eldar upp den andra. Sedan så åker man till en båt. Åker skit långt ut till havs och slänger ner alla kläder m.m i en sopsäck med sten i och dumpar det i havet. Åker tillbacka till flyktbil nr 3 och åker hem. OBS! Alla snackar engelska under rånet så att kasörskan tror att man är från något annat land.\r\n\r\nSkulle detta funka eller tror ni att man skulle åka dit?\r\n\r\nOBS! Detta kommer jag ej göra och jag uppmanar ingen annan att göra det heller!!OBS!", new DateTime(2004, 9, 14, 22, 54, 0, 0, DateTimeKind.Unspecified), "Tror ni att man kommer åka fast?", "c732c21e-3d82-487d-bd47-0790f732983c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cc7e8ce-0da8-40e4-ae7d-e4d49fe44ffc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "41c2f21b-c8c0-4e7d-ad69-680ae24d6e37");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "AnonUsers",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "varchar(255)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonUsers", x => x.Identifier);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreationDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "41c2f21b-c8c0-4e7d-ad69-680ae24d6e37", 0, "5c90d772-7d86-472d-8689-3bad0267d801", new DateTime(2004, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "nibbler@nibblerflashback.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEJahMAS9wxRd1QbA3+32n+lqLL04N6lOYOhzomu+GU8bsXCoZ5evTnFmfTNXol/X3A==", null, false, "cb0f6207-d804-441b-9617-6c277ce95795", false, "Nibbler" },
                    { "6cc7e8ce-0da8-40e4-ae7d-e4d49fe44ffc", 0, "bbe93e22-5723-4e6b-99d3-51cf31f7437f", new DateTime(2023, 4, 30, 17, 54, 36, 953, DateTimeKind.Local).AddTicks(4301), "admin@administrator.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEDeMzYRGWt6yosW6yP6jOdGJMT2CEUmzyr6YH41cj1zfPY6+J2mHwaINzs9MjVw4cw==", null, false, "34994a99-83a6-4123-ab70-ceba3c43bb0f", false, "admin" }
                });
        }
    }
}
