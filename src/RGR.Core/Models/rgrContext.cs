using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RGR.Core.Models
{
    public partial class rgrContext : DbContext, IDisposable
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=RAVENHOG-3\SQLEXPRESS;Initial Catalog=RDV_test;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }*/

        public rgrContext(DbContextOptions<rgrContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievments>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Organizer).HasMaxLength(255);

                entity.Property(e => e.ReachDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Achievments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Achievments_Users");
            });

            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.Property(e => e.Block).HasMaxLength(50);

                entity.Property(e => e.CityDistrictId).HasDefaultValueSql("-1");

                entity.Property(e => e.CityId).HasDefaultValueSql("-1");

                entity.Property(e => e.CountryId).HasDefaultValueSql("-1");

                entity.Property(e => e.DistrictResidentialAreaId).HasDefaultValueSql("-1");

                entity.Property(e => e.Flat).HasMaxLength(50);

                entity.Property(e => e.House).HasMaxLength(50);

                entity.Property(e => e.Land).HasMaxLength(50);

                entity.Property(e => e.RegionDistrictId).HasDefaultValueSql("-1");

                entity.Property(e => e.RegionId).HasDefaultValueSql("-1");

                entity.Property(e => e.StreetId).HasDefaultValueSql("-1");

                entity.HasOne(d => d.CityDistrict)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityDistrictId)
                    .HasConstraintName("FK_Addresses_GeoDistricts");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Addresses_GeoCities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Addresses_GeoCountries");

                entity.HasOne(d => d.DistrictResidentialArea)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.DistrictResidentialAreaId)
                    .HasConstraintName("FK_Addresses_GeoResidentialAreas");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_Addresses_EstateObjects");

                entity.HasOne(d => d.RegionDistrict)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.RegionDistrictId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Addresses_GeoRegionDistricts");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Addresses_GeoRegions");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.StreetId)
                    .HasConstraintName("FK_Addresses_GeoStreets");
            });

            modelBuilder.Entity<Articles>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.PublicationDate).HasColumnType("datetime");

                entity.Property(e => e.ShortContent).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Views).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<AuditEvents>(entity =>
            {
                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.EventType).HasDefaultValueSql("0");

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuditEvents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AuditEvents_Users");
            });

            modelBuilder.Entity<Banners>(entity =>
            {
                entity.Property(e => e.Clicks).HasDefaultValueSql("0");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Location).HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Views).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(255);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Picture).HasMaxLength(255);

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ClientReviews>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Operation).HasDefaultValueSql("-1");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ClientReviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ClientReviews_Users");
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.Property(e => e.AgencyPayment).HasDefaultValueSql("0");

                entity.Property(e => e.AgreementDate).HasColumnType("datetime");

                entity.Property(e => e.AgreementEndDate).HasColumnType("datetime");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Blacklisted).HasDefaultValueSql("0");

                entity.Property(e => e.Commision).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.Icq)
                    .HasColumnName("ICQ")
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Clients_Companies");

                entity.HasOne(d => d.Passport)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.PassportId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Clients_Passports");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.AuthiorEmail).HasMaxLength(255);

                entity.Property(e => e.AuthorName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasDefaultValueSql("-1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Comments_Users");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.Property(e => e.CityId).HasDefaultValueSql("1");

                entity.Property(e => e.CompanyType).HasDefaultValueSql("-1");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DirectorId).HasDefaultValueSql("-1");

                entity.Property(e => e.Inactive).HasDefaultValueSql("0");

                entity.Property(e => e.IsServiceProvider).HasDefaultValueSql("0");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Ndspayer)
                    .HasColumnName("NDSPayer")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Phone1).HasMaxLength(100);

                entity.Property(e => e.Phone2).HasMaxLength(100);

                entity.Property(e => e.Phone3).HasMaxLength(100);

                entity.Property(e => e.ShortName).HasMaxLength(255);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Companies_GeoCities");

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.DirectorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Companies_Users");
            });

            modelBuilder.Entity<Dictionaries>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(255);

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DictionaryValues>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.HasOne(d => d.Dictionary)
                    .WithMany(p => p.DictionaryValues)
                    .HasForeignKey(d => d.DictionaryId)
                    .HasConstraintName("FK_DictionaryValues_Dictionaries");
            });

            modelBuilder.Entity<EstateObjectMatchedSearchRequestComments>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.HasOne(d => d.MatchedRequest)
                    .WithMany(p => p.EstateObjectMatchedSearchRequestComments)
                    .HasForeignKey(d => d.MatchedRequestId)
                    .HasConstraintName("FK_EstateObjectMatchedSearchRequestComments_EstateObjectMatchedSearchRequests");
            });

            modelBuilder.Entity<EstateObjectMatchedSearchRequests>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateMoved).HasColumnType("datetime");

                entity.Property(e => e.RequestDateCreated).HasColumnType("datetime");

                entity.Property(e => e.RequestDateDeleted).HasColumnType("datetime");

                entity.Property(e => e.RequestTitle).HasMaxLength(255);

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.EstateObjectMatchedSearchRequests)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_EstateObjectMatchedSearchRequests_EstateObjects");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.EstateObjectMatchedSearchRequests)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EstateObjectMatchedSearchRequests_SearchRequests");

                entity.HasOne(d => d.RequestUser)
                    .WithMany(p => p.EstateObjectMatchedSearchRequests)
                    .HasForeignKey(d => d.RequestUserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EstateObjectMatchedSearchRequests_Users");
            });

            modelBuilder.Entity<EstateObjects>(entity =>
            {
                entity.Property(e => e.ClientId).HasDefaultValueSql("-1");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Filled).HasDefaultValueSql("0");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.ObjectType).HasDefaultValueSql("0");

                entity.Property(e => e.Operation).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.EstateObjects)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EstateObjects_Clients");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.EstateObjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EstateObjects_Users");
            });

            modelBuilder.Entity<GeoCities>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.RegionDistrict)
                    .WithMany(p => p.GeoCities)
                    .HasForeignKey(d => d.RegionDistrictId)
                    .HasConstraintName("FK_GeoCities_GeoRegionDistricts");
            });

            modelBuilder.Entity<GeoCountries>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<GeoDistricts>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Population).HasDefaultValueSql("0");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.GeoDistricts)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GeoDistricts_GeoCities");
            });

            modelBuilder.Entity<GeoLandmarks>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AreaId).HasDefaultValueSql("-1");

                entity.Property(e => e.CityId).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DistrictId).HasDefaultValueSql("-1");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StreetId).HasDefaultValueSql("-1");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.GeoLandmarks)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GeoLandmarks_GeoResidentialAreas");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.GeoLandmarks)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GeoLandmarks_GeoCities");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.GeoLandmarks)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GeoLandmarks_GeoDistricts");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.GeoLandmarks)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GeoLandmarks_GeoStreets");
            });

            modelBuilder.Entity<GeoObjectInfos>(entity =>
            {
                entity.Property(e => e.Builder).HasMaxLength(255);

                entity.Property(e => e.Community).HasDefaultValueSql("0");

                entity.Property(e => e.CommunityName).HasMaxLength(255);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Liter).HasMaxLength(255);

                entity.Property(e => e.Locked).HasDefaultValueSql("0");

                entity.Property(e => e.Number).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.HasOne(d => d.GeoObject)
                    .WithMany(p => p.GeoObjectInfos)
                    .HasForeignKey(d => d.GeoObjectId)
                    .HasConstraintName("FK_GeoObjectInfos_GeoObjects");
            });

            modelBuilder.Entity<GeoObjects>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.GeoObjects)
                    .HasForeignKey(d => d.StreetId)
                    .HasConstraintName("FK_GeoObjects_GeoStreets");
            });

            modelBuilder.Entity<GeoRegionDistricts>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.GeoRegionDistricts)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_GeoRegionDistricts_GeoRegionDistricts");
            });

            modelBuilder.Entity<GeoRegions>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.GeoRegions)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_GeoRegions_GeoCountries");
            });

            modelBuilder.Entity<GeoResidentialAreas>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.GeoResidentialAreas)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_GeoResidentialAreas_GeoDistricts");
            });

            modelBuilder.Entity<GeoStreets>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.GeoStreets)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_GeoStreets_GeoResidentialAreas");
            });

            modelBuilder.Entity<MailNotificationMessages>(entity =>
            {
                entity.Property(e => e.DateEnqued).HasColumnType("datetime");

                entity.Property(e => e.DateSended).HasColumnType("datetime");

                entity.Property(e => e.Recipient)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Sended).HasDefaultValueSql("0");

                entity.Property(e => e.Subject).HasMaxLength(255);
            });

            modelBuilder.Entity<MenuItems>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Position).HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<NonRdvAgents>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.SurName).HasMaxLength(255);
            });

            modelBuilder.Entity<ObjectAdditionalProperties>(entity =>
            {
                entity.Property(e => e.AdditionalBuildings).HasMaxLength(255);

                entity.Property(e => e.AgreementEndDate).HasColumnType("datetime");

                entity.Property(e => e.AgreementStartDate).HasColumnType("datetime");

                entity.Property(e => e.Basement).HasMaxLength(255);

                entity.Property(e => e.Loading).HasMaxLength(255);

                entity.Property(e => e.RentDate).HasColumnType("date");

                entity.Property(e => e.ViewFromWindows).HasMaxLength(255);

                entity.Property(e => e.WindowsLocation).HasMaxLength(255);

                entity.Property(e => e.WindowsMaterial).HasMaxLength(255);

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectAdditionalProperties)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectAdditionalProperties_ObjectAdditionalProperties");
            });

            modelBuilder.Entity<ObjectChangementProperties>(entity =>
            {
                entity.Property(e => e.AdvanceDate).HasColumnType("datetime");

                entity.Property(e => e.ChangedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DateMoved).HasColumnType("datetime");

                entity.Property(e => e.DateRegisted).HasColumnType("datetime");

                entity.Property(e => e.DealDate).HasColumnType("datetime");

                entity.Property(e => e.DelayToDate).HasColumnType("datetime");

                entity.Property(e => e.PriceChanged).HasColumnType("datetime");

                entity.Property(e => e.StatusChangedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.ViewDate).HasColumnType("datetime");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectChangementProperties)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectChangementProperties_EstateObjects");
            });

            modelBuilder.Entity<ObjectClients>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ObjectClients)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ObjectClients_Clients");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectClients)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ObjectClients_EstateObjects");
            });

            modelBuilder.Entity<ObjectCommunications>(entity =>
            {
                entity.Property(e => e.Electricy).HasMaxLength(255);

                entity.Property(e => e.Gas).HasMaxLength(255);

                entity.Property(e => e.Heating).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.SanFurniture).HasMaxLength(255);

                entity.Property(e => e.Tubes).HasMaxLength(255);

                entity.Property(e => e.Water).HasMaxLength(255);

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectCommunications)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectCommunications_EstateObjects");
            });

            modelBuilder.Entity<ObjectHistory>(entity =>
            {
                entity.Property(e => e.AdvanceEndDate).HasColumnType("date");

                entity.Property(e => e.ClientId).HasDefaultValueSql("-1");

                entity.Property(e => e.CompanyId).HasDefaultValueSql("-1");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DelayDate).HasColumnType("datetime");

                entity.Property(e => e.HistoryStatus).HasDefaultValueSql("-1");

                entity.Property(e => e.NonRdvagentId)
                    .HasColumnName("NonRDVAgentId")
                    .HasDefaultValueSql("-1");

                entity.Property(e => e.RdvagentId)
                    .HasColumnName("RDVAgentId")
                    .HasDefaultValueSql("-1");

                entity.HasOne(d => d.NonRdvagent)
                    .WithMany(p => p.ObjectHistory)
                    .HasForeignKey(d => d.NonRdvagentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ObjectHistory_NonRdvAgents");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectHistory)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectHistory_EstateObjects");

                entity.HasOne(d => d.Rdvagent)
                    .WithMany(p => p.ObjectHistory)
                    .HasForeignKey(d => d.RdvagentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ObjectHistory_Users");
            });

            modelBuilder.Entity<ObjectMainProperties>(entity =>
            {
                entity.Property(e => e.AddCommercialBuildings).HasMaxLength(255);

                entity.Property(e => e.BuildingMaterial).HasMaxLength(255);

                entity.Property(e => e.FloorMaterial).HasMaxLength(255);

                entity.Property(e => e.LandAssignment).HasMaxLength(255);

                entity.Property(e => e.LeaseTime).HasColumnType("datetime");

                entity.Property(e => e.LivingPeoples).HasMaxLength(255);

                entity.Property(e => e.ObjectAssignment).HasMaxLength(255);

                entity.Property(e => e.ObjectUsage).HasMaxLength(255);

                entity.Property(e => e.Security).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.ContactCompany)
                    .WithMany(p => p.ObjectMainProperties)
                    .HasForeignKey(d => d.ContactCompanyId)
                    .HasConstraintName("FK__ObjectMai__Conta__0697FACD");

                entity.HasOne(d => d.ContactPerson)
                    .WithMany(p => p.ObjectMainProperties)
                    .HasForeignKey(d => d.ContactPersonId)
                    .HasConstraintName("FK_ObjectMainProperties_User");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectMainProperties)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectMainProperties_EstateObjects");
            });

            modelBuilder.Entity<ObjectManagerNotifications>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.NotificationType).HasDefaultValueSql("0");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectManagerNotifications)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectManagerNotifications_EstateObjects");
            });

            modelBuilder.Entity<ObjectMedias>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IsMain).HasDefaultValueSql("0");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Position).HasDefaultValueSql("0");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Views).HasDefaultValueSql("0");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectMedias)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectMedias_EstateObjects");
            });

            modelBuilder.Entity<ObjectPriceChangements>(entity =>
            {
                entity.Property(e => e.DateChanged).HasColumnType("datetime");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectPriceChangements)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectPriceChangements_EstateObjects");
            });

            modelBuilder.Entity<ObjectRatingProperties>(entity =>
            {
                entity.Property(e => e.Balcony).HasMaxLength(255);

                entity.Property(e => e.Carpentry).HasMaxLength(255);

                entity.Property(e => e.Ceiling).HasMaxLength(255);

                entity.Property(e => e.EntranceDoor).HasMaxLength(255);

                entity.Property(e => e.Floor).HasMaxLength(255);

                entity.Property(e => e.Furniture).HasMaxLength(255);

                entity.Property(e => e.Kitchen).HasMaxLength(255);

                entity.Property(e => e.Ladder).HasMaxLength(255);

                entity.Property(e => e.Loggia).HasMaxLength(255);

                entity.Property(e => e.Vestibule).HasMaxLength(255);

                entity.Property(e => e.Walls).HasMaxLength(255);

                entity.Property(e => e.Wc)
                    .HasColumnName("WC")
                    .HasMaxLength(255);

                entity.Property(e => e.Wcdescription).HasColumnName("WCDescription");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectRatingProperties)
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("FK_ObjectRatingProperties_EstateObjects");
            });

            modelBuilder.Entity<Partners>(entity =>
            {
                entity.Property(e => e.ActiveImageUrl).HasMaxLength(255);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.InactiveImageUrl).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<Passports>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Number).HasMaxLength(6);

                entity.Property(e => e.Series).HasMaxLength(4);
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CompanyId).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DatePayed).HasColumnType("datetime");

                entity.Property(e => e.Payed).HasDefaultValueSql("0");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Payments_Companies");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Payments_Users");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(255);

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.OperationContext).HasDefaultValueSql("0");

                entity.Property(e => e.PermissionGroup).HasMaxLength(255);

                entity.Property(e => e.SystemName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<RolePermissionOptions>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.HasOne(d => d.RolePermission)
                    .WithMany(p => p.RolePermissionOptions)
                    .HasForeignKey(d => d.RolePermissionId)
                    .HasConstraintName("FK_RolePermissionOptions_RolePermissions");
            });

            modelBuilder.Entity<RolePermissions>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RolePermissions_Permissions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RolePermissions_Roles");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SearchRequestObjectComments>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.HasOne(d => d.RequestObject)
                    .WithMany(p => p.SearchRequestObjectComments)
                    .HasForeignKey(d => d.RequestObjectId)
                    .HasConstraintName("FK_SearchRequestObjectComments_SearchRequestObjects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SearchRequestObjectComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SearchRequestObjectComments_Users");
            });

            modelBuilder.Entity<SearchRequestObjects>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateMoved).HasColumnType("datetime");

                entity.Property(e => e.DeclineReason).HasMaxLength(255);

                entity.Property(e => e.DeclineReasonPrice).HasDefaultValueSql("0");

                entity.Property(e => e.New).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("1");

                entity.HasOne(d => d.EstateObject)
                    .WithMany(p => p.SearchRequestObjects)
                    .HasForeignKey(d => d.EstateObjectId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_SearchRequestObjects_EstateObjects");

                entity.HasOne(d => d.SearchRequest)
                    .WithMany(p => p.SearchRequestObjects)
                    .HasForeignKey(d => d.SearchRequestId)
                    .HasConstraintName("FK_SearchRequestObjects_SearchRequestObjects");
            });

            modelBuilder.Entity<SearchRequests>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.TimesUsed).HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SearchRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SearchRequests_Users");
            });

            modelBuilder.Entity<ServiceLogItems>(entity =>
            {
                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.Rdvsummary)
                    .HasColumnName("RDVSummary")
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Summary)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Volume)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ServiceLogItems)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ServiceLogItems_Companies");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceLogItems)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ServiceLogItems_ServiceTypes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ServiceLogItems)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ServiceLogItems_Users");
            });

            modelBuilder.Entity<ServiceTypes>(entity =>
            {
                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.Property(e => e.ContractNumber).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Rdvshare)
                    .HasColumnName("RDVShare")
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ServiceStatus).HasDefaultValueSql("0");

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.HasOne(d => d.Provided)
                    .WithMany(p => p.ServiceTypes)
                    .HasForeignKey(d => d.ProvidedId)
                    .HasConstraintName("FK_ServiceTypes_Companies");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<SmsnotificationMessages>(entity =>
            {
                entity.ToTable("SMSNotificationMessages");

                entity.Property(e => e.DateEnqueued).HasColumnType("datetime");

                entity.Property(e => e.DateSended).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Recipient)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Sended).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<StaticPages>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Route).HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Views).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<StoredFiles>(entity =>
            {
                entity.Property(e => e.ContentSize).HasDefaultValueSql("0");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.OriginalFilename)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ServerFilename)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SystemStats>(entity =>
            {
                entity.Property(e => e.StatDateTime).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("money");
            });

            modelBuilder.Entity<TrainingPrograms>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.TrainingDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TrainingPrograms)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TrainingPrograms_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Activated).HasDefaultValueSql("0");

                entity.Property(e => e.Appointment).HasMaxLength(255);

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.Blocked).HasDefaultValueSql("0");

                entity.Property(e => e.CertificateEndDate).HasColumnType("date");

                entity.Property(e => e.CertificationDate).HasColumnType("date");

                entity.Property(e => e.CompanyId).HasDefaultValueSql("-1");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Icq).HasColumnName("ICQ");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasDefaultValueSql("-1");

                entity.Property(e => e.Notifications).HasDefaultValueSql("0");

                entity.Property(e => e.PassportId).HasDefaultValueSql("-1");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone2).HasMaxLength(100);

                entity.Property(e => e.RoleId).HasDefaultValueSql("1");

                entity.Property(e => e.SeniorityStartDate).HasColumnType("date");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Users_Companies");

                entity.HasOne(d => d.Passport)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PassportId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Users_Passports");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Roles");
            });
        }

        public virtual DbSet<Achievments> Achievments { get; set; }
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<AuditEvents> AuditEvents { get; set; }
        public virtual DbSet<Banners> Banners { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<ClientReviews> ClientReviews { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Dictionaries> Dictionaries { get; set; }
        public virtual DbSet<DictionaryValues> DictionaryValues { get; set; }
        public virtual DbSet<EstateObjectMatchedSearchRequestComments> EstateObjectMatchedSearchRequestComments { get; set; }
        public virtual DbSet<EstateObjectMatchedSearchRequests> EstateObjectMatchedSearchRequests { get; set; }
        public virtual DbSet<EstateObjects> EstateObjects { get; set; }
        public virtual DbSet<GeoCities> GeoCities { get; set; }
        public virtual DbSet<GeoCountries> GeoCountries { get; set; }
        public virtual DbSet<GeoDistricts> GeoDistricts { get; set; }
        public virtual DbSet<GeoLandmarks> GeoLandmarks { get; set; }
        public virtual DbSet<GeoObjectInfos> GeoObjectInfos { get; set; }
        public virtual DbSet<GeoObjects> GeoObjects { get; set; }
        public virtual DbSet<GeoRegionDistricts> GeoRegionDistricts { get; set; }
        public virtual DbSet<GeoRegions> GeoRegions { get; set; }
        public virtual DbSet<GeoResidentialAreas> GeoResidentialAreas { get; set; }
        public virtual DbSet<GeoStreets> GeoStreets { get; set; }
        public virtual DbSet<MailNotificationMessages> MailNotificationMessages { get; set; }
        public virtual DbSet<MenuItems> MenuItems { get; set; }
        public virtual DbSet<NonRdvAgents> NonRdvAgents { get; set; }
        public virtual DbSet<ObjectAdditionalProperties> ObjectAdditionalProperties { get; set; }
        public virtual DbSet<ObjectChangementProperties> ObjectChangementProperties { get; set; }
        public virtual DbSet<ObjectClients> ObjectClients { get; set; }
        public virtual DbSet<ObjectCommunications> ObjectCommunications { get; set; }
        public virtual DbSet<ObjectHistory> ObjectHistory { get; set; }
        public virtual DbSet<ObjectMainProperties> ObjectMainProperties { get; set; }
        public virtual DbSet<ObjectManagerNotifications> ObjectManagerNotifications { get; set; }
        public virtual DbSet<ObjectMedias> ObjectMedias { get; set; }
        public virtual DbSet<ObjectPriceChangements> ObjectPriceChangements { get; set; }
        public virtual DbSet<ObjectRatingProperties> ObjectRatingProperties { get; set; }
        public virtual DbSet<Partners> Partners { get; set; }
        public virtual DbSet<Passports> Passports { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<RolePermissionOptions> RolePermissionOptions { get; set; }
        public virtual DbSet<RolePermissions> RolePermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SearchRequestObjectComments> SearchRequestObjectComments { get; set; }
        public virtual DbSet<SearchRequestObjects> SearchRequestObjects { get; set; }
        public virtual DbSet<SearchRequests> SearchRequests { get; set; }
        public virtual DbSet<ServiceLogItems> ServiceLogItems { get; set; }
        public virtual DbSet<ServiceTypes> ServiceTypes { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<SmsnotificationMessages> SmsnotificationMessages { get; set; }
        public virtual DbSet<StaticPages> StaticPages { get; set; }
        public virtual DbSet<StoredFiles> StoredFiles { get; set; }
        public virtual DbSet<SystemStats> SystemStats { get; set; }
        public virtual DbSet<TrainingPrograms> TrainingPrograms { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}