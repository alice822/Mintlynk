using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MintLynk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConfirmationToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagicToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    ProfilePicPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsLetterSubscription = table.Column<bool>(type: "bit", nullable: true),
                    TermsAndConditionAccepted = table.Column<bool>(type: "bit", nullable: true),
                    Intro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink0",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink0", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink1",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink1", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink2",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink2", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink3",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink3", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink4",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink4", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink5",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink5", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink6",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink6", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink7",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink7", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink8",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink8", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLink9",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLink9", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkA",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkA", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkB",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkB", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkC",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkC", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkD",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkD", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkE",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkE", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkF",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkF", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkG",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkG", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkH",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkH", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkI",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkI", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkJ",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkJ", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkK",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkK", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkL",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkL", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkM",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkM", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkN",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkN", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkO",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkO", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkP",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkP", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkQ",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkQ", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkR",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkR", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkS",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkS", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkT",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkT", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkU",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkU", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkV",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkV", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkW",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkW", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkX",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkX", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkY",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkY", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkZ",
                columns: table => new
                {
                    EntityId = table.Column<string>(type: "varchar(8)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(40)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UtmParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    HasExpirationDate = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkZ", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartLinkHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Referrer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrowserVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartLinkHistory", x => x.Id);

                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink0EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink0EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink1EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink1EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink2EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink2EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink3EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink3EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink4EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink4EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink5EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink5EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink6EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink6EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink7EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink7EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink8EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink8EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLink9EntityId",
                table: "SmartLinkHistory",
                column: "SmartLink9EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkAEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkAEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkBEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkBEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkCEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkCEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkDEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkDEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkEEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkEEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkFEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkFEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkGEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkGEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkHEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkHEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkIEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkIEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkJEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkJEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkKEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkKEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkLEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkLEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkMEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkMEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkNEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkNEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkOEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkOEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkPEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkPEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkQEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkQEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkREntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkREntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkSEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkSEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkTEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkTEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkUEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkUEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkVEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkVEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkWEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkWEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkXEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkXEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkYEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkYEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartLinkHistory_SmartLinkZEntityId",
                table: "SmartLinkHistory",
                column: "SmartLinkZEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "SmartLinkHistory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SmartLink0");

            migrationBuilder.DropTable(
                name: "SmartLink1");

            migrationBuilder.DropTable(
                name: "SmartLink2");

            migrationBuilder.DropTable(
                name: "SmartLink3");

            migrationBuilder.DropTable(
                name: "SmartLink4");

            migrationBuilder.DropTable(
                name: "SmartLink5");

            migrationBuilder.DropTable(
                name: "SmartLink6");

            migrationBuilder.DropTable(
                name: "SmartLink7");

            migrationBuilder.DropTable(
                name: "SmartLink8");

            migrationBuilder.DropTable(
                name: "SmartLink9");

            migrationBuilder.DropTable(
                name: "SmartLinkA");

            migrationBuilder.DropTable(
                name: "SmartLinkB");

            migrationBuilder.DropTable(
                name: "SmartLinkC");

            migrationBuilder.DropTable(
                name: "SmartLinkD");

            migrationBuilder.DropTable(
                name: "SmartLinkE");

            migrationBuilder.DropTable(
                name: "SmartLinkF");

            migrationBuilder.DropTable(
                name: "SmartLinkG");

            migrationBuilder.DropTable(
                name: "SmartLinkH");

            migrationBuilder.DropTable(
                name: "SmartLinkI");

            migrationBuilder.DropTable(
                name: "SmartLinkJ");

            migrationBuilder.DropTable(
                name: "SmartLinkK");

            migrationBuilder.DropTable(
                name: "SmartLinkL");

            migrationBuilder.DropTable(
                name: "SmartLinkM");

            migrationBuilder.DropTable(
                name: "SmartLinkN");

            migrationBuilder.DropTable(
                name: "SmartLinkO");

            migrationBuilder.DropTable(
                name: "SmartLinkP");

            migrationBuilder.DropTable(
                name: "SmartLinkQ");

            migrationBuilder.DropTable(
                name: "SmartLinkR");

            migrationBuilder.DropTable(
                name: "SmartLinkS");

            migrationBuilder.DropTable(
                name: "SmartLinkT");

            migrationBuilder.DropTable(
                name: "SmartLinkU");

            migrationBuilder.DropTable(
                name: "SmartLinkV");

            migrationBuilder.DropTable(
                name: "SmartLinkW");

            migrationBuilder.DropTable(
                name: "SmartLinkX");

            migrationBuilder.DropTable(
                name: "SmartLinkY");

            migrationBuilder.DropTable(
                name: "SmartLinkZ");
        }
    }
}
