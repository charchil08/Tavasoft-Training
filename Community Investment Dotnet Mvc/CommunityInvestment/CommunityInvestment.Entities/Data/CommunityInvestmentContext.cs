using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CommunityInvestment.Entities.DataModels;

namespace CommunityInvestment.Entities.Data
{
    public partial class CommunityInvestmentContext : DbContext
    {
        public CommunityInvestmentContext()
        {
        }

        public CommunityInvestmentContext(DbContextOptions<CommunityInvestmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<ApprovalStatus> ApprovalStatuses { get; set; } = null!;
        public virtual DbSet<Availability> Availabilities { get; set; } = null!;
        public virtual DbSet<Banner> Banners { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<CmsPage> CmsPages { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<FavouriteMission> FavouriteMissions { get; set; } = null!;
        public virtual DbSet<GoalMission> GoalMissions { get; set; } = null!;
        public virtual DbSet<Mission> Missions { get; set; } = null!;
        public virtual DbSet<MissionApplication> MissionApplications { get; set; } = null!;
        public virtual DbSet<MissionDocument> MissionDocuments { get; set; } = null!;
        public virtual DbSet<MissionInvite> MissionInvites { get; set; } = null!;
        public virtual DbSet<MissionMedium> MissionMedia { get; set; } = null!;
        public virtual DbSet<MissionRating> MissionRatings { get; set; } = null!;
        public virtual DbSet<MissionSkill> MissionSkills { get; set; } = null!;
        public virtual DbSet<MissionTheme> MissionThemes { get; set; } = null!;
        public virtual DbSet<MissionType> MissionTypes { get; set; } = null!;
        public virtual DbSet<PasswordReset> PasswordResets { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<Story> Stories { get; set; } = null!;
        public virtual DbSet<StoryInvite> StoryInvites { get; set; } = null!;
        public virtual DbSet<StoryMedium> StoryMedia { get; set; } = null!;
        public virtual DbSet<StoryStatus> StoryStatuses { get; set; } = null!;
        public virtual DbSet<Timesheet> Timesheets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserSkill> UserSkills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=CiConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.HasIndex(e => e.Email, "UQ__admin__AB6E6164CCB896ED")
                    .IsUnique();

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<ApprovalStatus>(entity =>
            {
                entity.ToTable("approval_status");

                entity.Property(e => e.ApprovalStatusId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("approval_status_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.ToTable("availability");

                entity.Property(e => e.AvailabilityId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("availability_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("banner");

                entity.Property(e => e.BannerId).HasColumnName("banner_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Image)
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("sort_order")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasColumnName("text");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.HasIndex(e => e.Name, "UQ__city__72E12F1B207DEE0A")
                    .IsUnique();

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("fk_country_countryId__city__countryId");
            });

            modelBuilder.Entity<CmsPage>(entity =>
            {
                entity.ToTable("cms_page");

                entity.Property(e => e.CmsPageId).HasColumnName("cms_page_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Desciption)
                    .HasColumnType("text")
                    .HasColumnName("desciption");

                entity.Property(e => e.Slug)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("slug");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.ApprovalStatusId)
                    .HasColumnName("approval_status_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ApprovalStatus)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ApprovalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_approvalStatus_approvalStatusId__comment__approvalStatusId");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__comment__missionId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__comment__userId");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.HasIndex(e => e.Name, "UQ__country__72E12F1B4C96A748")
                    .IsUnique();

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Iso)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("ISO");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<FavouriteMission>(entity =>
            {
                entity.ToTable("favourite_mission");

                entity.Property(e => e.FavouriteMissionId).HasColumnName("favourite_mission_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.FavouriteMissions)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__favouriteMission__missionId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FavouriteMissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__favouriteMission__userId");
            });

            modelBuilder.Entity<GoalMission>(entity =>
            {
                entity.ToTable("goal_mission");

                entity.Property(e => e.GoalMissionId).HasColumnName("goal_mission_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.GoalObjectiveText)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("goal_objective_text");

                entity.Property(e => e.GoalValue).HasColumnName("goal_value");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.GoalMissions)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__goalMission__missionId");
            });

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.ToTable("mission");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.MissionThemeId).HasColumnName("mission_theme_id");

                entity.Property(e => e.MissionTypeId).HasColumnName("mission_type_id");

                entity.Property(e => e.OrganizationDetail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("organization_detail");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("organization_name");

                entity.Property(e => e.ShortDescription)
                    .HasColumnType("text")
                    .HasColumnName("short_description");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Availability)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.AvailabilityId)
                    .HasConstraintName("fk_availability_availabilittyId__mission__availabilittyId");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_cityId__mission__cityId");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_country_countryId__mission__countryId");

                entity.HasOne(d => d.MissionTheme)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.MissionThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_missionTheme_missionThemeId__mission__missionThemeId");

                entity.HasOne(d => d.MissionType)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.MissionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_missionType_missionTypeId__mission__missionTypeId");
            });

            modelBuilder.Entity<MissionApplication>(entity =>
            {
                entity.ToTable("mission_application");

                entity.Property(e => e.MissionApplicationId).HasColumnName("mission_application_id");

                entity.Property(e => e.AppliedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("applied_at");

                entity.Property(e => e.ApprovalStatusId).HasColumnName("approval_status_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ApprovalStatus)
                    .WithMany(p => p.MissionApplications)
                    .HasForeignKey(d => d.ApprovalStatusId)
                    .HasConstraintName("fk_approvalStatus_approvalStatusId__missionApplication__approvalStatusId");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionApplications)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__missionApplication_missionId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MissionApplications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__missionApplication__userId");
            });

            modelBuilder.Entity<MissionDocument>(entity =>
            {
                entity.ToTable("mission_document");

                entity.Property(e => e.MissionDocumentId).HasColumnName("mission_document_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.DocumentName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("document_name");

                entity.Property(e => e.DocumentPath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("document_path");

                entity.Property(e => e.DoucmentType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("doucment_type");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionDocuments)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__missionDocument__missionId");
            });

            modelBuilder.Entity<MissionInvite>(entity =>
            {
                entity.ToTable("mission_invite");

                entity.Property(e => e.MissionInviteId).HasColumnName("mission_invite_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.FromUserId).HasColumnName("from_user_id");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.ToUserId).HasColumnName("to_user_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.MissionInviteFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__missionInvite__fromUserId");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionInvites)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__missionInvite__missionId");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.MissionInviteToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__missionInvite__toUserId");
            });

            modelBuilder.Entity<MissionMedium>(entity =>
            {
                entity.HasKey(e => e.MissionMediaId)
                    .HasName("PK__mission___848A78E81EB2948D");

                entity.ToTable("mission_media");

                entity.Property(e => e.MissionMediaId).HasColumnName("mission_media_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.Default).HasColumnName("default");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.MediaName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("media_name");

                entity.Property(e => e.MediaPath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("media_path");

                entity.Property(e => e.MediaType)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("media_type");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionMedia)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__missionMedia__missionId");
            });

            modelBuilder.Entity<MissionRating>(entity =>
            {
                entity.ToTable("mission_rating");

                entity.Property(e => e.MissionRatingId).HasColumnName("mission_rating_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.MediaName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("media_name");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionRatings)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__missionRating__missionId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MissionRatings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__missionRating__userId");
            });

            modelBuilder.Entity<MissionSkill>(entity =>
            {
                entity.ToTable("mission_skill");

                entity.Property(e => e.MissionSkillId).HasColumnName("mission_skill_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.MediaName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("media_name");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.SkillId).HasColumnName("skill_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.MissionSkills)
                    .HasForeignKey(d => d.MissionId)
                    .HasConstraintName("fk_mission_missionId__missionSkill__missionId");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.MissionSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_skill_skillId__missionSkill__skillId");
            });

            modelBuilder.Entity<MissionTheme>(entity =>
            {
                entity.ToTable("mission_theme");

                entity.Property(e => e.MissionThemeId).HasColumnName("mission_theme_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<MissionType>(entity =>
            {
                entity.ToTable("mission_type");

                entity.Property(e => e.MissionTypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("mission_type_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<PasswordReset>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("pk_password_reset");

                entity.ToTable("password_reset");

                entity.Property(e => e.Email)
                    .HasMaxLength(191)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.Token)
                    .HasMaxLength(191)
                    .IsUnicode(false)
                    .HasColumnName("token");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("skill");

                entity.Property(e => e.SkillId).HasColumnName("skill_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.SkillName)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("skill_name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Story>(entity =>
            {
                entity.ToTable("story");

                entity.Property(e => e.StoryId).HasColumnName("story_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.PublishedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("published_at");

                entity.Property(e => e.StoryStatusId)
                    .HasColumnName("story_status_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__story__missionId");

                entity.HasOne(d => d.StoryStatus)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.StoryStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_storyStatus_storyStatusId__story__storyStatusId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__story__userId");
            });

            modelBuilder.Entity<StoryInvite>(entity =>
            {
                entity.ToTable("story_invite");

                entity.Property(e => e.StoryInviteId).HasColumnName("story_invite_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.FromUserId).HasColumnName("from_user_id");

                entity.Property(e => e.StoryId).HasColumnName("story_id");

                entity.Property(e => e.ToUserId).HasColumnName("to_user_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.StoryInviteFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__storyInvite__fromUserId");

                entity.HasOne(d => d.Story)
                    .WithMany(p => p.StoryInvites)
                    .HasForeignKey(d => d.StoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_story_storyId__storyInvite__storyId");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.StoryInviteToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__storyInvite__toUserId");
            });

            modelBuilder.Entity<StoryMedium>(entity =>
            {
                entity.HasKey(e => e.StoryMediaId)
                    .HasName("PK__story_me__29BD053C6B1FEE9F");

                entity.ToTable("story_media");

                entity.Property(e => e.StoryMediaId).HasColumnName("story_media_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Path)
                    .HasColumnType("text")
                    .HasColumnName("path");

                entity.Property(e => e.StoryId).HasColumnName("story_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Story)
                    .WithMany(p => p.StoryMedia)
                    .HasForeignKey(d => d.StoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_story_storyId__storyMedia__storyId");
            });

            modelBuilder.Entity<StoryStatus>(entity =>
            {
                entity.ToTable("story_status");

                entity.Property(e => e.StoryStatusId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("story_status_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Timesheet>(entity =>
            {
                entity.ToTable("timesheet");

                entity.Property(e => e.TimesheetId).HasColumnName("timesheet_id");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ApprovalStatusId)
                    .HasColumnName("approval_status_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DateVolunteered)
                    .HasColumnType("datetime")
                    .HasColumnName("date_volunteered");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.Notes)
                    .HasColumnType("text")
                    .HasColumnName("notes");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ApprovalStatus)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.ApprovalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_approvalStatus_approvalStatusId__timesheet__approvalStatusId");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mission_missionId__timesheet__missionId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__timesheet__userId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Department)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("department");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("employee_id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.LinkedInUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("linked_in_url");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.ProfileText)
                    .HasColumnType("text")
                    .HasColumnName("profile_text");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.WhyIVolunteer)
                    .HasColumnType("text")
                    .HasColumnName("why_i_volunteer");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_cityId__user__cityId");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_country_countryId__user__countryId");
            });

            modelBuilder.Entity<UserSkill>(entity =>
            {
                entity.ToTable("user_skill");

                entity.Property(e => e.UserSkillId).HasColumnName("user_skill_id");

                entity.Property(e => e.CreatedAt)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.SkillId).HasColumnName("skill_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Skill)
                    .WithMany(p => p.UserSkills)
                    .HasForeignKey(d => d.SkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_skill_skillId__userSkill__skillId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSkills)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_userId__userSkill__userId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
