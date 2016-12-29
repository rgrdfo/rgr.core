using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RGR.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Organizer = table.Column<string>(nullable: true),
                    ReachDate = table.Column<DateTime>(nullable: false),
                    ScanUrl = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Block = table.Column<string>(nullable: true),
                    CityDistrictId = table.Column<long>(nullable: false),
                    CityId = table.Column<long>(nullable: false),
                    CountryId = table.Column<long>(nullable: false),
                    DistrictResidentialAreaId = table.Column<long>(nullable: false),
                    Flat = table.Column<string>(nullable: true),
                    House = table.Column<string>(nullable: true),
                    Land = table.Column<string>(nullable: true),
                    Latitude = table.Column<float>(nullable: false),
                    Logitude = table.Column<float>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    RegionDistrictId = table.Column<long>(nullable: false),
                    RegionId = table.Column<long>(nullable: false),
                    StreetId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleType = table.Column<short>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    FullContent = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    PreviewImage = table.Column<string>(nullable: true),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    ShortContent = table.Column<string>(nullable: true),
                    Title = table.Column<long>(nullable: false),
                    VideoLink = table.Column<string>(nullable: true),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditEvents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdditionalInformation = table.Column<string>(nullable: true),
                    BrowserInfo = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    EventType = table.Column<short>(nullable: false),
                    IP = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Clicks = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    LinkUrl = table.Column<string>(nullable: true),
                    Location = table.Column<short>(nullable: false),
                    ObjectUrl = table.Column<string>(nullable: true),
                    ShowProbability = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<short>(nullable: false),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Authorusing = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Publisher = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    AgencyPayment = table.Column<bool>(nullable: false),
                    AgreementDate = table.Column<DateTime>(nullable: false),
                    AgreementEndDate = table.Column<DateTime>(nullable: false),
                    AgreementNumber = table.Column<string>(nullable: true),
                    AgreementType = table.Column<short>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Blacklisted = table.Column<bool>(nullable: false),
                    ClientType = table.Column<short>(nullable: false),
                    Commision = table.Column<string>(nullable: true),
                    CompanyId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    ICQ = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PassportId = table.Column<long>(nullable: false),
                    PaymentConditions = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientReviews",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ObjectId = table.Column<long>(nullable: false),
                    Operation = table.Column<short>(nullable: false),
                    ReviewDate = table.Column<DateTime>(nullable: false),
                    ScanUrl = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientReviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthiorEmail = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    EntityId = table.Column<long>(nullable: false),
                    EntityType = table.Column<short>(nullable: false),
                    RequestData = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    CompanyType = table.Column<long>(nullable: false),
                    ContactPerson = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DirectorId = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Inactive = table.Column<bool>(nullable: false),
                    IsServiceProvider = table.Column<bool>(nullable: false),
                    LocationSchemeUrl = table.Column<string>(nullable: true),
                    LogoImageUrl = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    NDSPayer = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    Phone3 = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dictionaries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    SystemName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryValues",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DictionaryId = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: false),
                    ShortValue = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstateObjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Filled = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: false),
                    ObjectType = table.Column<short>(nullable: false),
                    Operation = table.Column<short>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstateObjectMatchedSearchRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateMoved = table.Column<DateTime>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    RequestDateCreated = table.Column<DateTime>(nullable: false),
                    RequestDateDeleted = table.Column<DateTime>(nullable: false),
                    RequestId = table.Column<long>(nullable: false),
                    RequestTitle = table.Column<string>(nullable: true),
                    RequestUserId = table.Column<long>(nullable: false),
                    Status = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateObjectMatchedSearchRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstateObjectMatchedSearchRequestComments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    MatchedRequestId = table.Column<long>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateObjectMatchedSearchRequestComments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoCities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RegionDistrictId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoCities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoCountries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoDistricts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bounds = table.Column<string>(nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Population = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoDistricts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoLandmarks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaId = table.Column<long>(nullable: false),
                    CityId = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DistrictId = table.Column<long>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StreetId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoLandmarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoObjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StreetId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoObjectInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuildYear = table.Column<int>(nullable: false),
                    Builder = table.Column<string>(nullable: true),
                    BuildingMaterial = table.Column<string>(nullable: true),
                    CelingMaterial = table.Column<string>(nullable: true),
                    Community = table.Column<bool>(nullable: false),
                    CommunityName = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EntranceCount = table.Column<int>(nullable: false),
                    FloorsCount = table.Column<int>(nullable: false),
                    Gas = table.Column<bool>(nullable: false),
                    GeoObjectId = table.Column<long>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Liter = table.Column<string>(nullable: true),
                    Locked = table.Column<bool>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Planning = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoObjectInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoRegions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoRegionDistricts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RegionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoRegionDistricts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoResidentialAreas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bounds = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DistrictId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoResidentialAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoStreets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaId = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoStreets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailNotificationMessages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    DateEnqued = table.Column<DateTime>(nullable: false),
                    DateSended = table.Column<DateTime>(nullable: false),
                    Recipient = table.Column<string>(nullable: true),
                    Sended = table.Column<bool>(nullable: false),
                    Subject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailNotificationMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Href = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonRdvAgents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonRdvAgents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectAdditionalProperties",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdditionalBuildings = table.Column<string>(nullable: true),
                    AgencyPayment = table.Column<bool>(nullable: false),
                    AgreementEndDate = table.Column<DateTime>(nullable: false),
                    AgreementNumber = table.Column<string>(nullable: true),
                    AgreementStartDate = table.Column<DateTime>(nullable: false),
                    AgreementType = table.Column<long>(nullable: false),
                    Auction = table.Column<bool>(nullable: false),
                    BalconiesCount = table.Column<int>(nullable: false),
                    Basement = table.Column<string>(nullable: true),
                    BaywindowsCount = table.Column<int>(nullable: false),
                    BedroomsCount = table.Column<int>(nullable: false),
                    Builder = table.Column<long>(nullable: false),
                    BuildingYear = table.Column<int>(nullable: false),
                    Burdens = table.Column<long>(nullable: false),
                    Comission = table.Column<string>(nullable: true),
                    CorrectPlanning = table.Column<bool>(nullable: false),
                    Court = table.Column<long>(nullable: false),
                    Environment = table.Column<long>(nullable: false),
                    ErkersCount = table.Column<int>(nullable: false),
                    ExtensionsLegality = table.Column<bool>(nullable: false),
                    Fence = table.Column<long>(nullable: false),
                    Fencing = table.Column<long>(nullable: false),
                    FlatLocation = table.Column<long>(nullable: false),
                    FlatRoomsCount = table.Column<int>(nullable: false),
                    Loading = table.Column<string>(nullable: true),
                    LoggiasCount = table.Column<int>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    ObjectName = table.Column<string>(nullable: true),
                    OwnerPart = table.Column<long>(nullable: false),
                    PaymentCondition = table.Column<string>(nullable: true),
                    Placement = table.Column<long>(nullable: false),
                    PlotForm = table.Column<long>(nullable: false),
                    Redesign = table.Column<bool>(nullable: false),
                    RedesignLegality = table.Column<bool>(nullable: false),
                    RegistrationPosibility = table.Column<bool>(nullable: false),
                    RentDate = table.Column<DateTime>(nullable: false),
                    RentWithServices = table.Column<bool>(nullable: false),
                    Roof = table.Column<long>(nullable: false),
                    RoomPlanning = table.Column<long>(nullable: false),
                    RoomsCount = table.Column<int>(nullable: false),
                    ViewFromWindows = table.Column<string>(nullable: true),
                    WindowsLocation = table.Column<string>(nullable: true),
                    WindowsMaterial = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectAdditionalProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectChangementProperties",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvanceDate = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateMoved = table.Column<DateTime>(nullable: false),
                    DateRegisted = table.Column<DateTime>(nullable: false),
                    DealDate = table.Column<DateTime>(nullable: false),
                    DelayToDate = table.Column<DateTime>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    PriceChanged = table.Column<DateTime>(nullable: false),
                    PriceChanging = table.Column<double>(nullable: false),
                    StatusChangedBy = table.Column<long>(nullable: false),
                    ViewDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectChangementProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectClients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectCommunications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Electricy = table.Column<string>(nullable: true),
                    Gas = table.Column<string>(nullable: true),
                    HasColdWaterMeter = table.Column<bool>(nullable: false),
                    HasElectricyMeter = table.Column<bool>(nullable: false),
                    HasGasMeter = table.Column<bool>(nullable: false),
                    HasHotWaterMeter = table.Column<bool>(nullable: false),
                    HasInternet = table.Column<bool>(nullable: false),
                    Heating = table.Column<string>(nullable: true),
                    ObjectId = table.Column<long>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    SanFurniture = table.Column<string>(nullable: true),
                    Sewer = table.Column<long>(nullable: false),
                    Tubes = table.Column<string>(nullable: true),
                    Water = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectCommunications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvanceEndDate = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<long>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DelayDate = table.Column<DateTime>(nullable: false),
                    DelayReason = table.Column<long>(nullable: false),
                    HistoryStatus = table.Column<short>(nullable: false),
                    NonRDVAgentId = table.Column<long>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    RDVAgentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectMainProperties",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbilityForMachineryEntrance = table.Column<bool>(nullable: false),
                    ActualUsableFloorArea = table.Column<double>(nullable: false),
                    AddCommercialBuildings = table.Column<string>(nullable: true),
                    Advertising1 = table.Column<string>(nullable: true),
                    Advertising2 = table.Column<string>(nullable: true),
                    Advertising3 = table.Column<string>(nullable: true),
                    Advertising4 = table.Column<string>(nullable: true),
                    Advertising5 = table.Column<string>(nullable: true),
                    AtticHeight = table.Column<double>(nullable: false),
                    BigRoomFloorArea = table.Column<double>(nullable: false),
                    BuildingClass = table.Column<long>(nullable: false),
                    BuildingFloor = table.Column<double>(nullable: false),
                    BuildingMaterial = table.Column<string>(nullable: true),
                    BuildingPeriod = table.Column<long>(nullable: false),
                    BuildingReadyPercent = table.Column<double>(nullable: false),
                    BuildingType = table.Column<long>(nullable: false),
                    CelingHeight = table.Column<double>(nullable: false),
                    ContactCompanyId = table.Column<long>(nullable: false),
                    ContactPersonId = table.Column<long>(nullable: false),
                    ContactPhone = table.Column<short>(nullable: false),
                    ContractorCompany = table.Column<long>(nullable: false),
                    Currency = table.Column<long>(nullable: false),
                    DistanceToCity = table.Column<double>(nullable: false),
                    DistanceToSea = table.Column<double>(nullable: false),
                    Documents = table.Column<string>(nullable: true),
                    ElectricPower = table.Column<double>(nullable: false),
                    EntranceToObject = table.Column<long>(nullable: false),
                    EntryLocation = table.Column<long>(nullable: false),
                    Exchange = table.Column<bool>(nullable: false),
                    ExchangeConditions = table.Column<string>(nullable: true),
                    Exclusive = table.Column<bool>(nullable: false),
                    FacadeWindowsCount = table.Column<int>(nullable: false),
                    FamiliesCount = table.Column<int>(nullable: false),
                    FirstFloorDownSet = table.Column<double>(nullable: false),
                    FlatType = table.Column<long>(nullable: false),
                    FloorMaterial = table.Column<string>(nullable: true),
                    FloorNumber = table.Column<int>(nullable: false),
                    FootageExplanation = table.Column<string>(nullable: true),
                    Foundation = table.Column<long>(nullable: false),
                    FullDescription = table.Column<string>(nullable: true),
                    HasParking = table.Column<bool>(nullable: false),
                    HasPhotos = table.Column<bool>(nullable: false),
                    HasWeights = table.Column<bool>(nullable: false),
                    HouseType = table.Column<long>(nullable: false),
                    HousingStock = table.Column<bool>(nullable: false),
                    HowToReach = table.Column<long>(nullable: false),
                    IsSetNumberAgency = table.Column<bool>(nullable: false),
                    KitchenFloorArea = table.Column<double>(nullable: false),
                    LandArea = table.Column<double>(nullable: false),
                    LandAssignment = table.Column<string>(nullable: true),
                    LandFloorFactical = table.Column<double>(nullable: false),
                    Landmark = table.Column<long>(nullable: false),
                    LeaseTime = table.Column<DateTime>(nullable: false),
                    LevelsCount = table.Column<int>(nullable: false),
                    LivingPeolplesDescription = table.Column<string>(nullable: true),
                    LivingPeoples = table.Column<string>(nullable: true),
                    Loading = table.Column<long>(nullable: false),
                    MetricDescription = table.Column<string>(nullable: true),
                    MortgageBank = table.Column<long>(nullable: false),
                    MortgagePossibility = table.Column<bool>(nullable: false),
                    MultilistingBonus = table.Column<double>(nullable: false),
                    MultilistingBonusType = table.Column<long>(nullable: false),
                    Negotiable = table.Column<bool>(nullable: false),
                    NewBuilding = table.Column<bool>(nullable: false),
                    NonResidenceUsage = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    ObjectAssignment = table.Column<string>(nullable: true),
                    ObjectId = table.Column<long>(nullable: false),
                    ObjectUsage = table.Column<string>(nullable: true),
                    OwnerPrice = table.Column<double>(nullable: false),
                    OwnerShare = table.Column<long>(nullable: false),
                    OwnersCount = table.Column<int>(nullable: false),
                    PhoneLinesCount = table.Column<int>(nullable: false),
                    Prepayment = table.Column<bool>(nullable: false),
                    PrescriptionsCount = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    PricePerHundred = table.Column<double>(nullable: false),
                    PricePerUnit = table.Column<double>(nullable: false),
                    PricingZone = table.Column<int>(nullable: false),
                    PropertyType = table.Column<long>(nullable: false),
                    RealPrice = table.Column<double>(nullable: false),
                    ReleaseInfo = table.Column<string>(nullable: true),
                    Relief = table.Column<long>(nullable: false),
                    RemovalReason = table.Column<long>(nullable: false),
                    RentOverpayment = table.Column<string>(nullable: true),
                    RentPerDay = table.Column<double>(nullable: false),
                    RentPerMonth = table.Column<double>(nullable: false),
                    ResidencePermit = table.Column<bool>(nullable: false),
                    Security = table.Column<string>(nullable: true),
                    SellConditions = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    SpecialOffer = table.Column<bool>(nullable: false),
                    SpecialOfferDescription = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    TotalArea = table.Column<double>(nullable: false),
                    TotalFloors = table.Column<int>(nullable: false),
                    Urgently = table.Column<bool>(nullable: false),
                    UtilitiesRentExspensive = table.Column<bool>(nullable: false),
                    WindowsCount = table.Column<int>(nullable: false),
                    Yard = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectMainProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectManagerNotifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    NotificationType = table.Column<short>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectManagerNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectMedias",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    MediaType = table.Column<short>(nullable: false),
                    MediaUrl = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    PreviewUrl = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectPriceChangements",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChangedBy = table.Column<long>(nullable: false),
                    Currency = table.Column<long>(nullable: false),
                    DateChanged = table.Column<DateTime>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectPriceChangements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectRatingProperties",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Balcony = table.Column<string>(nullable: true),
                    BuildingClass = table.Column<long>(nullable: false),
                    Carpentry = table.Column<string>(nullable: true),
                    Ceiling = table.Column<string>(nullable: true),
                    CommonState = table.Column<long>(nullable: false),
                    EntranceDoor = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    Furniture = table.Column<string>(nullable: true),
                    Kitchen = table.Column<string>(nullable: true),
                    KitchenDescription = table.Column<string>(nullable: true),
                    Ladder = table.Column<string>(nullable: true),
                    Loggia = table.Column<string>(nullable: true),
                    Multilisting = table.Column<bool>(nullable: false),
                    ObjectId = table.Column<long>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    UtilityRooms = table.Column<string>(nullable: true),
                    Vestibule = table.Column<string>(nullable: true),
                    WC = table.Column<string>(nullable: true),
                    WCDescription = table.Column<string>(nullable: true),
                    Walls = table.Column<string>(nullable: true),
                    WindowsDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectRatingProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActiveImageUrl = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    InactiveImageUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    IssuesBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    RegistrationPlace = table.Column<string>(nullable: true),
                    Series = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paymenst",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DatePayed = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Direction = table.Column<short>(nullable: false),
                    Payed = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paymenst", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    OperationContext = table.Column<bool>(nullable: false),
                    PermissionGroup = table.Column<string>(nullable: true),
                    SystemName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: false),
                    PermissionId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionOptions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ObjectOperation = table.Column<short>(nullable: false),
                    ObjectType = table.Column<short>(nullable: false),
                    RolePermissionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    SearchUrl = table.Column<string>(nullable: true),
                    TimesUsed = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchRequestObjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateMoved = table.Column<DateTime>(nullable: false),
                    DeclineReason = table.Column<string>(nullable: true),
                    DeclineReasonPrice = table.Column<bool>(nullable: false),
                    EstateObjectId = table.Column<long>(nullable: false),
                    New = table.Column<bool>(nullable: false),
                    OldPrice = table.Column<double>(nullable: false),
                    SearchRequestId = table.Column<long>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    TriggerEvent = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequestObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchRequestObjectComments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    RequestObjectId = table.Column<long>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequestObjectComments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLogItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    RDVSummary = table.Column<decimal>(nullable: false),
                    ServiceId = table.Column<long>(nullable: false),
                    Summary = table.Column<decimal>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLogItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractDate = table.Column<DateTime>(nullable: false),
                    ContractNumber = table.Column<string>(nullable: true),
                    ContractScan = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Examples = table.Column<string>(nullable: true),
                    Geo = table.Column<string>(nullable: true),
                    Measure = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: false),
                    ProvidedId = table.Column<long>(nullable: false),
                    RDVShare = table.Column<decimal>(nullable: false),
                    Rules = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    ServiceStatus = table.Column<short>(nullable: false),
                    Subject = table.Column<long>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMSNotificationMessages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateEnqueued = table.Column<DateTime>(nullable: false),
                    DateSended = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Recipient = table.Column<string>(nullable: true),
                    Sended = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSNotificationMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticPages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: false),
                    Route = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Views = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoredFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentSize = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    MimeType = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    OriginalFilename = table.Column<string>(nullable: true),
                    ServerFilename = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemStats",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatDateTime = table.Column<DateTime>(nullable: false),
                    StatType = table.Column<short>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CertificateFile = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: false),
                    Organizer = table.Column<string>(nullable: true),
                    ProgramName = table.Column<string>(nullable: true),
                    TrainingDate = table.Column<DateTime>(nullable: false),
                    TrainingPlace = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activated = table.Column<bool>(nullable: false),
                    AdditionalInformation = table.Column<string>(nullable: true),
                    Appointment = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Blocked = table.Column<bool>(nullable: false),
                    CertificateEndDate = table.Column<DateTime>(nullable: false),
                    CertificateNumber = table.Column<string>(nullable: true),
                    CertificationDate = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    ICQ = table.Column<string>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: false),
                    Notifications = table.Column<short>(nullable: false),
                    PassportId = table.Column<long>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    PublicLoading = table.Column<string>(nullable: true),
                    RoleId = table.Column<long>(nullable: false),
                    SeniorityStartDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SurName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievments");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AuditEvents");

            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ClientReviews");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Dictionaries");

            migrationBuilder.DropTable(
                name: "DictionaryValues");

            migrationBuilder.DropTable(
                name: "EstateObjects");

            migrationBuilder.DropTable(
                name: "EstateObjectMatchedSearchRequests");

            migrationBuilder.DropTable(
                name: "EstateObjectMatchedSearchRequestComments");

            migrationBuilder.DropTable(
                name: "GeoCities");

            migrationBuilder.DropTable(
                name: "GeoCountries");

            migrationBuilder.DropTable(
                name: "GeoDistricts");

            migrationBuilder.DropTable(
                name: "GeoLandmarks");

            migrationBuilder.DropTable(
                name: "GeoObjects");

            migrationBuilder.DropTable(
                name: "GeoObjectInfos");

            migrationBuilder.DropTable(
                name: "GeoRegions");

            migrationBuilder.DropTable(
                name: "GeoRegionDistricts");

            migrationBuilder.DropTable(
                name: "GeoResidentialAreas");

            migrationBuilder.DropTable(
                name: "GeoStreets");

            migrationBuilder.DropTable(
                name: "MailNotificationMessages");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "NonRdvAgents");

            migrationBuilder.DropTable(
                name: "ObjectAdditionalProperties");

            migrationBuilder.DropTable(
                name: "ObjectChangementProperties");

            migrationBuilder.DropTable(
                name: "ObjectClients");

            migrationBuilder.DropTable(
                name: "ObjectCommunications");

            migrationBuilder.DropTable(
                name: "ObjectHistory");

            migrationBuilder.DropTable(
                name: "ObjectMainProperties");

            migrationBuilder.DropTable(
                name: "ObjectManagerNotifications");

            migrationBuilder.DropTable(
                name: "ObjectMedias");

            migrationBuilder.DropTable(
                name: "ObjectPriceChangements");

            migrationBuilder.DropTable(
                name: "ObjectRatingProperties");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Passports");

            migrationBuilder.DropTable(
                name: "Paymenst");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "RolePermissionOptions");

            migrationBuilder.DropTable(
                name: "SearchRequests");

            migrationBuilder.DropTable(
                name: "SearchRequestObjects");

            migrationBuilder.DropTable(
                name: "SearchRequestObjectComments");

            migrationBuilder.DropTable(
                name: "ServiceLogItems");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "SMSNotificationMessages");

            migrationBuilder.DropTable(
                name: "StaticPages");

            migrationBuilder.DropTable(
                name: "StoredFiles");

            migrationBuilder.DropTable(
                name: "SystemStats");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
