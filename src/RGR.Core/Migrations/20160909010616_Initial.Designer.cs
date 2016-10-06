using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RGR.Core.Models;

namespace RGR.Core.Migrations
{
    [DbContext(typeof(rgrContext))]
    [Migration("20160909010616_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RGR.Core.Models.DataObject+Achievment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Organizer");

                    b.Property<DateTime>("ReachDate");

                    b.Property<string>("ScanUrl");

                    b.Property<string>("Title");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Achievments");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Block");

                    b.Property<long>("CityDistrictId");

                    b.Property<long>("CityId");

                    b.Property<long>("CountryId");

                    b.Property<long>("DistrictResidentialAreaId");

                    b.Property<string>("Flat");

                    b.Property<string>("House");

                    b.Property<string>("Land");

                    b.Property<float>("Latitude");

                    b.Property<float>("Logitude");

                    b.Property<long>("ObjectId");

                    b.Property<long>("RegionDistrictId");

                    b.Property<long>("RegionId");

                    b.Property<long>("StreetId");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Article", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("ArticleType");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("FullContent");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("PreviewImage");

                    b.Property<DateTime>("PublicationDate");

                    b.Property<string>("ShortContent");

                    b.Property<long>("Title");

                    b.Property<string>("VideoLink");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+AuditEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalInformation");

                    b.Property<string>("BrowserInfo");

                    b.Property<DateTime>("EventDate");

                    b.Property<short>("EventType");

                    b.Property<string>("IP");

                    b.Property<string>("Message");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("AuditEvents");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Banner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Clicks");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("LinkUrl");

                    b.Property<short>("Location");

                    b.Property<string>("ObjectUrl");

                    b.Property<int>("ShowProbability");

                    b.Property<string>("Title");

                    b.Property<short>("Type");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Authorusing");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<string>("Picture");

                    b.Property<decimal>("Price");

                    b.Property<string>("Publisher");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<bool>("AgencyPayment");

                    b.Property<DateTime>("AgreementDate");

                    b.Property<DateTime>("AgreementEndDate");

                    b.Property<string>("AgreementNumber");

                    b.Property<short>("AgreementType");

                    b.Property<DateTime>("Birthday");

                    b.Property<bool>("Blacklisted");

                    b.Property<short>("ClientType");

                    b.Property<string>("Commision");

                    b.Property<long>("CompanyId");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("ICQ");

                    b.Property<string>("LastName");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("Notes");

                    b.Property<long>("PassportId");

                    b.Property<string>("PaymentConditions");

                    b.Property<string>("Phone");

                    b.Property<string>("SurName");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ClientReview", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<long>("ObjectId");

                    b.Property<short>("Operation");

                    b.Property<DateTime>("ReviewDate");

                    b.Property<string>("ScanUrl");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ClientReviews");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthiorEmail");

                    b.Property<string>("AuthorName");

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<long>("EntityId");

                    b.Property<short>("EntityType");

                    b.Property<string>("RequestData");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Branch");

                    b.Property<long>("CityId");

                    b.Property<long>("CompanyType");

                    b.Property<string>("ContactPerson");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<long>("DirectorId");

                    b.Property<string>("Email");

                    b.Property<bool>("Inactive");

                    b.Property<bool>("IsServiceProvider");

                    b.Property<string>("LocationSchemeUrl");

                    b.Property<string>("LogoImageUrl");

                    b.Property<long>("ModifiedBy");

                    b.Property<bool>("NDSPayer");

                    b.Property<string>("Name");

                    b.Property<string>("Phone1");

                    b.Property<string>("Phone2");

                    b.Property<string>("Phone3");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Dictionary", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("SystemName");

                    b.HasKey("Id");

                    b.ToTable("Dictionaries");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+DictionaryValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<long>("DictionaryId");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("ShortValue");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("DictionaryValues");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+EstateObject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ClientId");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("Filled");

                    b.Property<long>("ModifiedBy");

                    b.Property<short>("ObjectType");

                    b.Property<short>("Operation");

                    b.Property<short>("Status");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("EstateObjects");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+EstateObjectMatchedSearchRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateMoved");

                    b.Property<long>("ObjectId");

                    b.Property<DateTime>("RequestDateCreated");

                    b.Property<DateTime>("RequestDateDeleted");

                    b.Property<long>("RequestId");

                    b.Property<string>("RequestTitle");

                    b.Property<long>("RequestUserId");

                    b.Property<short>("Status");

                    b.HasKey("Id");

                    b.ToTable("EstateObjectMatchedSearchRequests");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+EstateObjectMatchedSearchRequestComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<long>("MatchedRequestId");

                    b.Property<string>("Text");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("EstateObjectMatchedSearchRequestComments");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoCity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name");

                    b.Property<long>("RegionDistrictId");

                    b.HasKey("Id");

                    b.ToTable("GeoCities");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoCountry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GeoCountries");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoDistrict", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bounds");

                    b.Property<long>("CityId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Population");

                    b.HasKey("Id");

                    b.ToTable("GeoDistricts");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoLandmark", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AreaId");

                    b.Property<long>("CityId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<long>("DistrictId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<long>("StreetId");

                    b.HasKey("Id");

                    b.ToTable("GeoLandmarks");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoObject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<long>("StreetId");

                    b.HasKey("Id");

                    b.ToTable("GeoObjects");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoObjectInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BuildYear");

                    b.Property<string>("Builder");

                    b.Property<string>("BuildingMaterial");

                    b.Property<string>("CelingMaterial");

                    b.Property<bool>("Community");

                    b.Property<string>("CommunityName");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<int>("EntranceCount");

                    b.Property<int>("FloorsCount");

                    b.Property<bool>("Gas");

                    b.Property<long>("GeoObjectId");

                    b.Property<double>("Latitude");

                    b.Property<string>("Liter");

                    b.Property<bool>("Locked");

                    b.Property<double>("Longitude");

                    b.Property<string>("Number");

                    b.Property<string>("Photo");

                    b.Property<string>("Planning");

                    b.HasKey("Id");

                    b.ToTable("GeoObjectInfos");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoRegion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CountryId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GeoRegions");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoRegionDistrict", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name");

                    b.Property<long>("RegionId");

                    b.HasKey("Id");

                    b.ToTable("GeoRegionDistricts");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoResidentialArea", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bounds");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<long>("DistrictId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GeoResidentialAreas");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+GeoStreet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AreaId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GeoStreets");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+MailNotificationMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateEnqued");

                    b.Property<DateTime>("DateSended");

                    b.Property<string>("Recipient");

                    b.Property<bool>("Sended");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.ToTable("MailNotificationMessages");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+MenuItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Href");

                    b.Property<long>("ModifiedBy");

                    b.Property<int>("Position");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+NonRdvAgent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.Property<string>("SurName");

                    b.HasKey("Id");

                    b.ToTable("NonRdvAgents");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectAdditionalProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalBuildings");

                    b.Property<bool>("AgencyPayment");

                    b.Property<DateTime>("AgreementEndDate");

                    b.Property<string>("AgreementNumber");

                    b.Property<DateTime>("AgreementStartDate");

                    b.Property<long>("AgreementType");

                    b.Property<bool>("Auction");

                    b.Property<int>("BalconiesCount");

                    b.Property<string>("Basement");

                    b.Property<int>("BaywindowsCount");

                    b.Property<int>("BedroomsCount");

                    b.Property<long>("Builder");

                    b.Property<int>("BuildingYear");

                    b.Property<long>("Burdens");

                    b.Property<string>("Comission");

                    b.Property<bool>("CorrectPlanning");

                    b.Property<long>("Court");

                    b.Property<long>("Environment");

                    b.Property<int>("ErkersCount");

                    b.Property<bool>("ExtensionsLegality");

                    b.Property<long>("Fence");

                    b.Property<long>("Fencing");

                    b.Property<long>("FlatLocation");

                    b.Property<int>("FlatRoomsCount");

                    b.Property<string>("Loading");

                    b.Property<int>("LoggiasCount");

                    b.Property<long>("ObjectId");

                    b.Property<string>("ObjectName");

                    b.Property<long>("OwnerPart");

                    b.Property<string>("PaymentCondition");

                    b.Property<long>("Placement");

                    b.Property<long>("PlotForm");

                    b.Property<bool>("Redesign");

                    b.Property<bool>("RedesignLegality");

                    b.Property<bool>("RegistrationPosibility");

                    b.Property<DateTime>("RentDate");

                    b.Property<bool>("RentWithServices");

                    b.Property<long>("Roof");

                    b.Property<long>("RoomPlanning");

                    b.Property<int>("RoomsCount");

                    b.Property<string>("ViewFromWindows");

                    b.Property<string>("WindowsLocation");

                    b.Property<string>("WindowsMaterial");

                    b.HasKey("Id");

                    b.ToTable("ObjectAdditionalProperties");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectChangementProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AdvanceDate");

                    b.Property<long>("ChangedBy");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("DateMoved");

                    b.Property<DateTime>("DateRegisted");

                    b.Property<DateTime>("DealDate");

                    b.Property<DateTime>("DelayToDate");

                    b.Property<long>("ObjectId");

                    b.Property<DateTime>("PriceChanged");

                    b.Property<double>("PriceChanging");

                    b.Property<long>("StatusChangedBy");

                    b.Property<DateTime>("ViewDate");

                    b.HasKey("Id");

                    b.ToTable("ObjectChangementProperties");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectClient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ClientId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<long>("ObjectId");

                    b.HasKey("Id");

                    b.ToTable("ObjectClients");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectCommunication", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Electricy");

                    b.Property<string>("Gas");

                    b.Property<bool>("HasColdWaterMeter");

                    b.Property<bool>("HasElectricyMeter");

                    b.Property<bool>("HasGasMeter");

                    b.Property<bool>("HasHotWaterMeter");

                    b.Property<bool>("HasInternet");

                    b.Property<string>("Heating");

                    b.Property<long>("ObjectId");

                    b.Property<string>("Phone");

                    b.Property<string>("SanFurniture");

                    b.Property<long>("Sewer");

                    b.Property<string>("Tubes");

                    b.Property<string>("Water");

                    b.HasKey("Id");

                    b.ToTable("ObjectCommunications");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectHistoryEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AdvanceEndDate");

                    b.Property<long>("ClientId");

                    b.Property<long>("CompanyId");

                    b.Property<long>("CreatedBy");

                    b.Property<string>("CustomerName");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DelayDate");

                    b.Property<long>("DelayReason");

                    b.Property<short>("HistoryStatus");

                    b.Property<long>("NonRDVAgentId");

                    b.Property<long>("ObjectId");

                    b.Property<long>("RDVAgentId");

                    b.HasKey("Id");

                    b.ToTable("ObjectHistory");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectMainProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AbilityForMachineryEntrance");

                    b.Property<double>("ActualUsableFloorArea");

                    b.Property<string>("AddCommercialBuildings");

                    b.Property<string>("Advertising1");

                    b.Property<string>("Advertising2");

                    b.Property<string>("Advertising3");

                    b.Property<string>("Advertising4");

                    b.Property<string>("Advertising5");

                    b.Property<double>("AtticHeight");

                    b.Property<double>("BigRoomFloorArea");

                    b.Property<long>("BuildingClass");

                    b.Property<double>("BuildingFloor");

                    b.Property<string>("BuildingMaterial");

                    b.Property<long>("BuildingPeriod");

                    b.Property<double>("BuildingReadyPercent");

                    b.Property<long>("BuildingType");

                    b.Property<double>("CelingHeight");

                    b.Property<long>("ContactCompanyId");

                    b.Property<long>("ContactPersonId");

                    b.Property<short>("ContactPhone");

                    b.Property<long>("ContractorCompany");

                    b.Property<long>("Currency");

                    b.Property<double>("DistanceToCity");

                    b.Property<double>("DistanceToSea");

                    b.Property<string>("Documents");

                    b.Property<double>("ElectricPower");

                    b.Property<long>("EntranceToObject");

                    b.Property<long>("EntryLocation");

                    b.Property<bool>("Exchange");

                    b.Property<string>("ExchangeConditions");

                    b.Property<bool>("Exclusive");

                    b.Property<int>("FacadeWindowsCount");

                    b.Property<int>("FamiliesCount");

                    b.Property<double>("FirstFloorDownSet");

                    b.Property<long>("FlatType");

                    b.Property<string>("FloorMaterial");

                    b.Property<int>("FloorNumber");

                    b.Property<string>("FootageExplanation");

                    b.Property<long>("Foundation");

                    b.Property<string>("FullDescription");

                    b.Property<bool>("HasParking");

                    b.Property<bool>("HasPhotos");

                    b.Property<bool>("HasWeights");

                    b.Property<long>("HouseType");

                    b.Property<bool>("HousingStock");

                    b.Property<long>("HowToReach");

                    b.Property<bool>("IsSetNumberAgency");

                    b.Property<double>("KitchenFloorArea");

                    b.Property<double>("LandArea");

                    b.Property<string>("LandAssignment");

                    b.Property<double>("LandFloorFactical");

                    b.Property<long>("Landmark");

                    b.Property<DateTime>("LeaseTime");

                    b.Property<int>("LevelsCount");

                    b.Property<string>("LivingPeolplesDescription");

                    b.Property<string>("LivingPeoples");

                    b.Property<long>("Loading");

                    b.Property<string>("MetricDescription");

                    b.Property<long>("MortgageBank");

                    b.Property<bool>("MortgagePossibility");

                    b.Property<double>("MultilistingBonus");

                    b.Property<long>("MultilistingBonusType");

                    b.Property<bool>("Negotiable");

                    b.Property<bool>("NewBuilding");

                    b.Property<bool>("NonResidenceUsage");

                    b.Property<string>("Notes");

                    b.Property<string>("ObjectAssignment");

                    b.Property<long>("ObjectId");

                    b.Property<string>("ObjectUsage");

                    b.Property<double>("OwnerPrice");

                    b.Property<long>("OwnerShare");

                    b.Property<int>("OwnersCount");

                    b.Property<int>("PhoneLinesCount");

                    b.Property<bool>("Prepayment");

                    b.Property<int>("PrescriptionsCount");

                    b.Property<double>("Price");

                    b.Property<double>("PricePerHundred");

                    b.Property<double>("PricePerUnit");

                    b.Property<int>("PricingZone");

                    b.Property<long>("PropertyType");

                    b.Property<double>("RealPrice");

                    b.Property<string>("ReleaseInfo");

                    b.Property<long>("Relief");

                    b.Property<long>("RemovalReason");

                    b.Property<string>("RentOverpayment");

                    b.Property<double>("RentPerDay");

                    b.Property<double>("RentPerMonth");

                    b.Property<bool>("ResidencePermit");

                    b.Property<string>("Security");

                    b.Property<string>("SellConditions");

                    b.Property<string>("ShortDescription");

                    b.Property<bool>("SpecialOffer");

                    b.Property<string>("SpecialOfferDescription");

                    b.Property<string>("Title");

                    b.Property<double>("TotalArea");

                    b.Property<int>("TotalFloors");

                    b.Property<bool>("Urgently");

                    b.Property<bool>("UtilitiesRentExspensive");

                    b.Property<int>("WindowsCount");

                    b.Property<long>("Yard");

                    b.HasKey("Id");

                    b.ToTable("ObjectMainProperties");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectManagerNotification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<short>("NotificationType");

                    b.Property<long>("ObjectId");

                    b.HasKey("Id");

                    b.ToTable("ObjectManagerNotifications");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectMedia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("IsMain");

                    b.Property<short>("MediaType");

                    b.Property<string>("MediaUrl");

                    b.Property<long>("ModifiedBy");

                    b.Property<long>("ObjectId");

                    b.Property<int>("Position");

                    b.Property<string>("PreviewUrl");

                    b.Property<string>("Title");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.ToTable("ObjectMedias");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectPriceChangement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ChangedBy");

                    b.Property<long>("Currency");

                    b.Property<DateTime>("DateChanged");

                    b.Property<long>("ObjectId");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.ToTable("ObjectPriceChangements");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ObjectRatingProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Balcony");

                    b.Property<long>("BuildingClass");

                    b.Property<string>("Carpentry");

                    b.Property<string>("Ceiling");

                    b.Property<long>("CommonState");

                    b.Property<string>("EntranceDoor");

                    b.Property<string>("Floor");

                    b.Property<string>("Furniture");

                    b.Property<string>("Kitchen");

                    b.Property<string>("KitchenDescription");

                    b.Property<string>("Ladder");

                    b.Property<string>("Loggia");

                    b.Property<bool>("Multilisting");

                    b.Property<long>("ObjectId");

                    b.Property<int>("Rating");

                    b.Property<string>("UtilityRooms");

                    b.Property<string>("Vestibule");

                    b.Property<string>("WC");

                    b.Property<string>("WCDescription");

                    b.Property<string>("Walls");

                    b.Property<string>("WindowsDescription");

                    b.HasKey("Id");

                    b.ToTable("ObjectRatingProperties");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Partner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActiveImageUrl");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("InactiveImageUrl");

                    b.Property<string>("Name");

                    b.Property<int>("Position");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Passport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("IssueDate");

                    b.Property<string>("IssuesBy");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("Number");

                    b.Property<string>("RegistrationPlace");

                    b.Property<string>("Series");

                    b.HasKey("Id");

                    b.ToTable("Passports");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<long>("CompanyId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DatePayed");

                    b.Property<string>("Description");

                    b.Property<short>("Direction");

                    b.Property<bool>("Payed");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Paymenst");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("DisplayName");

                    b.Property<long>("ModifiedBy");

                    b.Property<bool>("OperationContext");

                    b.Property<string>("PermissionGroup");

                    b.Property<string>("SystemName");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+RolePermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<long>("ModifiedBy");

                    b.Property<long>("PermissionId");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+RolePermissionOption", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<short>("ObjectOperation");

                    b.Property<short>("ObjectType");

                    b.Property<long>("RolePermissionId");

                    b.HasKey("Id");

                    b.ToTable("RolePermissionOptions");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+SearchRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("SearchUrl");

                    b.Property<int>("TimesUsed");

                    b.Property<string>("Title");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("SearchRequests");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+SearchRequestObject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateMoved");

                    b.Property<string>("DeclineReason");

                    b.Property<bool>("DeclineReasonPrice");

                    b.Property<long>("EstateObjectId");

                    b.Property<bool>("New");

                    b.Property<double>("OldPrice");

                    b.Property<long>("SearchRequestId");

                    b.Property<short>("Status");

                    b.Property<short>("TriggerEvent");

                    b.HasKey("Id");

                    b.ToTable("SearchRequestObjects");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+SearchRequestObjectComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<long>("RequestObjectId");

                    b.Property<string>("Text");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("SearchRequestObjectComments");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ServiceLogItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CompanyId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<decimal>("RDVSummary");

                    b.Property<long>("ServiceId");

                    b.Property<decimal>("Summary");

                    b.Property<long>("UserId");

                    b.Property<decimal>("Volume");

                    b.HasKey("Id");

                    b.ToTable("ServiceLogItems");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+ServiceType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ContractDate");

                    b.Property<string>("ContractNumber");

                    b.Property<string>("ContractScan");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<string>("Examples");

                    b.Property<string>("Geo");

                    b.Property<long>("Measure");

                    b.Property<long>("ModifiedBy");

                    b.Property<long>("ProvidedId");

                    b.Property<decimal>("RDVShare");

                    b.Property<string>("Rules");

                    b.Property<string>("ServiceName");

                    b.Property<short>("ServiceStatus");

                    b.Property<long>("Subject");

                    b.Property<decimal>("Tax");

                    b.HasKey("Id");

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+SMSNotificationMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateEnqueued");

                    b.Property<DateTime>("DateSended");

                    b.Property<string>("Message");

                    b.Property<string>("Recipient");

                    b.Property<bool>("Sended");

                    b.HasKey("Id");

                    b.ToTable("SMSNotificationMessages");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+StaticPage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("Route");

                    b.Property<string>("Title");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.ToTable("StaticPages");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+StoredFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ContentSize");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("MimeType");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("OriginalFilename");

                    b.Property<string>("ServerFilename");

                    b.HasKey("Id");

                    b.ToTable("StoredFiles");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+SystemStatsEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("StatDateTime");

                    b.Property<short>("StatType");

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.ToTable("SystemStats");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+TrainingProgram", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CertificateFile");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<long>("ModifiedBy");

                    b.Property<string>("Organizer");

                    b.Property<string>("ProgramName");

                    b.Property<DateTime>("TrainingDate");

                    b.Property<string>("TrainingPlace");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.ToTable("TrainingPrograms");
                });

            modelBuilder.Entity("RGR.Core.Models.DataObject+User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activated");

                    b.Property<string>("AdditionalInformation");

                    b.Property<string>("Appointment");

                    b.Property<DateTime>("Birthdate");

                    b.Property<bool>("Blocked");

                    b.Property<DateTime>("CertificateEndDate");

                    b.Property<string>("CertificateNumber");

                    b.Property<DateTime>("CertificationDate");

                    b.Property<long>("CompanyId");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("ICQ");

                    b.Property<DateTime>("LastLogin");

                    b.Property<string>("LastName");

                    b.Property<string>("Login");

                    b.Property<long>("ModifiedBy");

                    b.Property<short>("Notifications");

                    b.Property<long>("PassportId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Phone1");

                    b.Property<string>("Phone2");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("PublicLoading");

                    b.Property<long>("RoleId");

                    b.Property<DateTime>("SeniorityStartDate");

                    b.Property<int>("Status");

                    b.Property<string>("SurName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
