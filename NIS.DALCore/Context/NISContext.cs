namespace NIS.DALCore.Context
{
    using System;
    //using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using Microsoft.EntityFrameworkCore;
    using NIS.DALCore.Model;
    using DbContext = Microsoft.EntityFrameworkCore.DbContext;

    public partial class NISContext : DbContext
    {


        //public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<CustomerRequest> CustomerRequest { get; set; }
        public virtual DbSet<CustomerRequestType> CustomerRequestType { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Engineer> Engineer { get; set; }
        public virtual DbSet<RequestEmail> RequestEmail { get; set; }
        public virtual DbSet<InjuryType> InjuryType { get; set; }
        public virtual DbSet<MainTask> MainTask { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationUser> NotificationUser { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<Node> Node { get; set; }
        public virtual DbSet<LawProcess> LawProcess { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumber { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<RequestFile> RequestFile { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<RealTimeConnection> RealTimeConnection { get; set; }
        public virtual DbSet<RequestStatusHistory> RequestStatusHistory { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SourceType> SourceType { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<UserSettings> UserSettings { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<TaskStatus> TaskStatus { get; set; }
        public virtual DbSet<TaskStatusHistory> TaskStatusHistory { get; set; }
        public virtual DbSet<TaskHistory> TaskHistory { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }

        public virtual DbSet<WorkFlow> WorkFlow { get; set; }

        public virtual DbSet<CustomerRequestArchive> CustomerRequestArchive { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(DBConnection.Connection);
                // optionsBuilder.UseSqlServer(@"data source=91.135.244.25;initial catalog=Nis_Service_test2;persist security info=True;user id=ismayil;password=AzTeLekom#IsmayiL#2018;MultipleActiveResultSets=True;Connect Timeout=60;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Region)
                   .WithMany(p => p.Area)
                   .HasForeignKey(d => d.RegionId)
                   .HasConstraintName("FK_dbo.Area_Region");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FilePath).HasMaxLength(255);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User)
                 .WithMany(p => p.Comment)
                 .HasForeignKey(d => d.UserId)
                 .HasConstraintName("FK_dbo.comment_User");

                entity.HasOne(d => d.MainTask)
                .WithMany(p => p.Comment)
                .HasForeignKey(d => d.MainTaskId)
                .HasConstraintName("FK_dbo.comment_Maintask");
            });

            modelBuilder.Entity<CustomerRequest>(entity =>
            {
                entity.HasIndex(e => new { e.CustomerRequestTypeId, e.SourceTypeId, e.CreatedUserId })
                    .HasName("_dta_index_CustomerRequest_14_2053582354__K2_K12_K4");

                entity.HasIndex(e => new { e.Id, e.CustomerRequestTypeId, e.CreatedUserId, e.CustomerName, e.CustomerNumber, e.Text, e.StartDate, e.CreatedDate, e.Description, e.ContactNumber, e.SourceTypeId, e.RegionId, e.RequestStatusId, e.RatingId, e.Email, e.MailUniqueId, e.ContractNumber, e.AON, e.DepartmentId })
                    .HasName("_dta_index_CustomerRequest_14_2053582354__K3_1_2_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19");

                entity.HasIndex(e => new { e.Id, e.CustomerRequestTypeId, e.DepartmentId, e.CreatedUserId, e.CustomerName, e.CustomerNumber, e.Text, e.StartDate, e.CreatedDate, e.Description, e.ContactNumber, e.RegionId, e.RequestStatusId, e.RatingId, e.Email, e.MailUniqueId, e.ContractNumber, e.AON, e.SourceTypeId })
                    .HasName("_dta_index_CustomerRequest_14_2053582354__K12_1_2_3_4_5_6_7_8_9_10_11_13_14_15_16_17_18_19");

                entity.HasIndex(e => new { e.Id, e.CustomerRequestTypeId, e.DepartmentId, e.CreatedUserId, e.CustomerName, e.CustomerNumber, e.Text, e.StartDate, e.CreatedDate, e.Description, e.ContactNumber, e.SourceTypeId, e.RegionId, e.RatingId, e.Email, e.MailUniqueId, e.ContractNumber, e.AON, e.RequestStatusId })
                    .HasName("_dta_index_CustomerRequest_14_2053582354__K14_1_2_3_4_5_6_7_8_9_10_11_12_13_15_16_17_18_19");

                entity.HasIndex(e => new { e.Id, e.CustomerRequestTypeId, e.DepartmentId, e.CreatedUserId, e.CustomerName, e.CustomerNumber, e.Text, e.StartDate, e.CreatedDate, e.Description, e.ContactNumber, e.SourceTypeId, e.RegionId, e.RequestStatusId, e.Email, e.MailUniqueId, e.ContractNumber, e.AON, e.RatingId })
                    .HasName("_dta_index_CustomerRequest_14_2053582354__K15_1_2_3_4_5_6_7_8_9_10_11_12_13_14_16_17_18_19");

                entity.HasIndex(e => new { e.Id, e.CustomerRequestTypeId, e.DepartmentId, e.CreatedUserId, e.CustomerName, e.CustomerNumber, e.Text, e.StartDate, e.CreatedDate, e.Description, e.ContactNumber, e.SourceTypeId, e.RequestStatusId, e.RatingId, e.Email, e.MailUniqueId, e.ContractNumber, e.AON, e.RegionId })
                    .HasName("_dta_index_CustomerRequest_14_2053582354__K13_1_2_3_4_5_6_7_8_9_10_11_12_14_15_16_17_18_19");

                entity.HasIndex(e => new { e.Id, e.CustomerRequestTypeId, e.DepartmentId, e.CustomerName, e.CustomerNumber, e.Text, e.StartDate, e.CreatedDate, e.Description, e.ContactNumber, e.SourceTypeId, e.RegionId, e.RequestStatusId, e.RatingId, e.Email, e.MailUniqueId, e.ContractNumber, e.AON, e.CreatedUserId })
                    .HasName("_dta_index_CustomerRequest_14_2053582354__K4_1_2_3_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19");

                entity.HasIndex(e => new { e.Id, e.DepartmentId, e.CustomerName, e.CustomerNumber, e.Text, e.StartDate, e.CreatedDate, e.Description, e.ContactNumber, e.SourceTypeId, e.RegionId, e.RequestStatusId, e.RatingId, e.Email, e.MailUniqueId, e.ContractNumber, e.AON, e.CustomerRequestTypeId, e.CreatedUserId })
                    .HasName("_dta_index_CustomerRequest_14_2053582354__K2_K4_1_3_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19");

                entity.Property(e => e.AON)
                    .HasColumnName("AON")
                    .HasMaxLength(50);

                entity.Property(e => e.ContactNumber).HasMaxLength(50);

                entity.Property(e => e.ContractNumber).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Email).HasColumnType("nvarchar(max)");

                entity.Property(e => e.MailUniqueId).HasMaxLength(256);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Text).HasColumnType("nvarchar(max)");
            });


            modelBuilder.Entity<CustomerRequestType>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.ParentDepartmentId)
                    .HasName("IX_ParentDepartmentId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentType).HasMaxLength(1);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.HasOne(e => e.DefaultUser)
                .WithMany(e => e.DefaultDepartment)
                .HasForeignKey(d => d.DefaultUserId)
                .HasConstraintName("FK_dbo.department_USer");

            });

            modelBuilder.Entity<Engineer>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<InjuryType>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<MainTask>(entity =>
            {
                entity.HasIndex(e => e.UpdatedUserId)
                    .HasName("IX_UpdatedUserId");

                entity.HasIndex(e => new { e.DepartmentId, e.GeneratedUserId, e.MainTaskId, e.ExecutorUserId })
                    .HasName("_dta_index_MainTask_14_2099048__K9_K2_K12_K4");

                entity.HasIndex(e => new { e.ExecutorUserId, e.MainTaskId, e.GeneratedUserId, e.DepartmentId })
                    .HasName("_dta_index_MainTask_14_2099048__K4_K12_K2_K9");

                entity.HasIndex(e => new { e.Id, e.CustomerRequestId, e.ExecutorUserId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.DepartmentId, e.TaskStatusId, e.ProjectId, e.MainTaskId, e.RatingId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.GeneratedUserId })
                    .HasName("_dta_index_MainTask_14_2099048__K2_1_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.CustomerRequestId, e.ExecutorUserId, e.CreatedDate, e.EndDate, e.Description, e.DepartmentId, e.TaskStatusId, e.ProjectId, e.MainTaskId, e.RatingId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.InjuryTypeId })
                    .HasName("_dta_index_MainTask_14_2099048__K5_1_2_3_4_6_7_8_9_10_11_12_13_14_15_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.CustomerRequestId, e.ExecutorUserId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.DepartmentId, e.ProjectId, e.MainTaskId, e.RatingId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.TaskStatusId })
                    .HasName("_dta_index_MainTask_14_2099048__K10_1_2_3_4_5_6_7_8_9_11_12_13_14_15_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.CustomerRequestId, e.ExecutorUserId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.DepartmentId, e.TaskStatusId, e.MainTaskId, e.RatingId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.ProjectId })
                    .HasName("_dta_index_MainTask_14_2099048__K11_1_2_3_4_5_6_7_8_9_10_12_13_14_15_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.CustomerRequestId, e.ExecutorUserId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.DepartmentId, e.TaskStatusId, e.ProjectId, e.MainTaskId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.RatingId })
                    .HasName("_dta_index_MainTask_14_2099048__K13_1_2_3_4_5_6_7_8_9_10_11_12_14_15_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.CustomerRequestId, e.ExecutorUserId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.DepartmentId, e.TaskStatusId, e.ProjectId, e.MainTaskId, e.RatingId, e.Note, e.ProjectName, e.UpdatedUserId, e.RealInjuryTypeId })
                    .HasName("_dta_index_MainTask_14_2099048__K15_1_2_3_4_5_6_7_8_9_10_11_12_13_14_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.CustomerRequestId, e.ExecutorUserId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.DepartmentId, e.TaskStatusId, e.ProjectId, e.RatingId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.MainTaskId })
                    .HasName("_dta_index_MainTask_14_2099048__K12_1_2_3_4_5_6_7_8_9_10_11_13_14_15_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.CustomerRequestId, e.ExecutorUserId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.TaskStatusId, e.ProjectId, e.MainTaskId, e.RatingId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.DepartmentId })
                    .HasName("_dta_index_MainTask_14_2099048__K9_1_2_3_4_5_6_7_8_10_11_12_13_14_15_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.CustomerRequestId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.TaskStatusId, e.ProjectId, e.MainTaskId, e.RatingId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.ExecutorUserId, e.DepartmentId })
                    .HasName("_dta_index_MainTask_14_2099048__K4_K9_1_2_3_5_6_7_8_10_11_12_13_14_15_16_17_18");

                entity.HasIndex(e => new { e.Id, e.GeneratedUserId, e.ExecutorUserId, e.InjuryTypeId, e.CreatedDate, e.EndDate, e.Description, e.DepartmentId, e.TaskStatusId, e.ProjectId, e.MainTaskId, e.RatingId, e.Note, e.RealInjuryTypeId, e.ProjectName, e.UpdatedUserId, e.CustomerRequestId })
                    .HasName("_dta_index_MainTask_14_2099048__K3_1_2_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.EndDate).HasColumnType("datetime");


                entity.Property(e => e.Note).HasColumnType("nvarchar(max)");

                entity.Property(e => e.ProjectName).HasColumnType("nvarchar(max)");

                entity.HasOne(d => d.CustomerRequest)
                    .WithMany(p => p.MainTask)
                    .HasForeignKey(d => d.CustomerRequestId)
                    .HasConstraintName("FK_dbo.Maintask_Request");

                entity.HasOne(e => e.ExecutorUser)
                    .WithMany(e => e.ExecutedTask)
                    .HasForeignKey(d => d.ExecutorUserId)
                    .HasConstraintName("FK_dbo.maintask_ExecuterUSer");

                entity.HasOne(e => e.UpdateddUser)
                    .WithMany(e => e.UpdatedTask)
                    .HasForeignKey(d => d.UpdatedUserId)
                    .HasConstraintName("FK_dbo.maintask_USer");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.MainTask)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_dbo.MainTask_dbo.Project_ProjectId");


            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });


            modelBuilder.Entity<Node>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(50);
            });


            modelBuilder.Entity<LawProcess>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                //entity.HasOne(d => d.CustomerRequest);                 
                     
            });



            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Subject).HasMaxLength(250);
            });

            modelBuilder.Entity<NotificationUser>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.NotificationId, e.CreatedDate, e.Description, e.UserId, e.Status })
                    .HasName("_dta_index_NotificationUser_14_1013578649__K2_K6_1_3_4_5");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.Property(e => e.ADSL)
                    .HasColumnName("ADSL ")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProviderName).HasMaxLength(100);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);
            });

            modelBuilder.Entity<RealTimeConnection>(entity =>
            {
                entity.Property(e => e.ConnectionId).HasMaxLength(250);

                entity.Property(e => e.Description).HasMaxLength(250);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RequestEmail>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Message).HasColumnType("ntext");

                entity.Property(e => e.Subject).HasMaxLength(250);
            });

            modelBuilder.Entity<RequestFile>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.FilePath).HasMaxLength(250);
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<RequestStatusHistory>(entity =>
            {
                entity.HasIndex(e => new { e.RequestStatusId, e.UpdatedUserId, e.UpdatedDate, e.Description, e.Id, e.CustomerRequestId })
                    .HasName("_dta_index_RequestStatusHistory_14_1301579675__K1_2_3_4_5_6");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.HasOne(e => e.CustomerRequest)
                    .WithMany(e => e.RequestStatusHistory)
                    .HasForeignKey(e => e.CustomerRequestId)
                    .HasConstraintName("FK_Requeststatus_request");

                entity.HasOne(e => e.RequestStatus)
                   .WithMany(e => e.RequestStatusHistory)
                   .HasForeignKey(e => e.RequestStatusId)
                   .HasConstraintName("FK_RequeststatusHistory_requestStatus");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SourceType>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MobilePhone).HasMaxLength(30);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Organisation).HasMaxLength(100);

                entity.Property(e => e.PassportNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TaskHistory>(entity =>
            {
                entity.HasIndex(e => e.MainTaskId)
                    .HasName("IX_MainTaskId");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.MainTask)
                    .WithMany(p => p.TaskHistory)
                    .HasForeignKey(d => d.MainTaskId)
                    .HasConstraintName("FK_dbo.TaskHistory_dbo.MainTask_MainTaskId");
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<TaskStatusHistory>(entity =>
            {
                entity.HasIndex(e => new { e.MainTaskId, e.UpdatedUserId, e.UpdatedDate, e.Description, e.Id, e.TaskStatusId })
                    .HasName("_dta_index_TaskStatusHistory_14_1493580359__K1_2_3_4_5_6");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.ParentUserId })
                    .HasName("_dta_index_User_14_1525580473__K1_K10");

                entity.HasIndex(e => new { e.ParentUserId, e.Id })
                    .HasName("_dta_index_User_14_1525580473__K10_K1");

                entity.HasIndex(e => new { e.Id, e.Email, e.Password, e.MobileNumber, e.AccessFailedCount, e.PhoneNumber, e.LastLoginDate, e.ConnectedId, e.ParentUserId, e.FirstName, e.LastName, e.Salt, e.DepartmentId, e.UserTempId, e.IsDeleted, e.UserName })
                    .HasName("_dta_index_User_14_1525580473__K6_1_2_3_4_5_7_8_9_10_11_12_13_16_17_18");

                entity.HasIndex(e => new { e.Id, e.Email, e.Password, e.MobileNumber, e.AccessFailedCount, e.UserName, e.PhoneNumber, e.LastLoginDate, e.ConnectedId, e.FirstName, e.LastName, e.Salt, e.DepartmentId, e.UserTempId, e.IsDeleted, e.ParentUserId })
                    .HasName("_dta_index_User_14_1525580473__K10_1_2_3_4_5_6_7_8_9_11_12_13_16_17_18");

                entity.Property(e => e.ConnectedId).HasMaxLength(250);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Salt).HasMaxLength(32);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserTempId).HasColumnType("nvarchar(max)");
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.Property(e => e.ClaimType).HasMaxLength(250);

                entity.Property(e => e.ClaimValue).HasMaxLength(250);
            });

            modelBuilder.Entity<UserSettings>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_UserId");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Settings).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.UserSettings_dbo.User_UserId");
            });

        }
    }
}
