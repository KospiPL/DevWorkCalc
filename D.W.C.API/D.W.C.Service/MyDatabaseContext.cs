using D.W.C.Lib.D.W.C.Models;

using DevWorkCalc.D.W.C.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace D.W.C.API.D.W.C.Service
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }
        public DbSet<WorkItemHistory> WorkItemsHistory { get; set; }
        public DbSet<Iteration> Iterations { get; set; }
        public DbSet<WorkItemsList> WorkItem {  get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<WorkItemDetails> workItemDetails { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            var dateTimeConverterForHistory = new ValueConverter<DateTime, DateTime>(
                v => v.AddHours(2),
                v => v); // Convert to local time on retrieval
            
            modelBuilder.Entity<WorkItemDetails>(entity =>
            {
                entity.ToTable("ITEM_DET");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.ApiId).HasColumnName("AP_IID");
                entity.Property(e => e.Rev).HasColumnName("Rev");
                entity.Property(e => e.AreaPath).HasColumnName("AreaPath").HasMaxLength(255);
                entity.Property(e => e.TeamProject).HasColumnName("TeamProject").HasMaxLength(255);
                entity.Property(e => e.IterationPath).HasColumnName("IterationPath").HasMaxLength(255);
                entity.Property(e => e.WorkItemType).HasColumnName("WorkItemType").HasMaxLength(255);
                entity.Property(e => e.State).HasColumnName("State").HasMaxLength(255);
                entity.Property(e => e.DisplayName).HasColumnName("AssignedTo_DisplayName").HasMaxLength(255);
                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate").HasColumnType("DATETIME");
                entity.Property(e => e.Title).HasColumnName("Title").HasMaxLength(255);
                entity.Property(e => e.BoardColumn).HasColumnName("BoardColumn").HasMaxLength(255);
                entity.Property(e => e.ActivatedDate).HasColumnName("ActivatedDate").HasColumnType("DATETIME");
                entity.Property(e => e.ResolvedDate).HasColumnName("ResolvedDate").HasColumnType("DATETIME");
                entity.Property(e => e.WorkTime).HasColumnName("WorkTime").HasColumnType("decimal(18, 2)"); 


            });

            modelBuilder.Entity<WorkItemHistory>(entity =>
            {
                entity.ToTable("ITEM_HIS");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.ApiId).HasColumnName("AP_IID");
                entity.Property(e => e.Rev).HasColumnName("Rev");
                entity.Property(e => e.OldValueDate)
                      .HasColumnName("System_ChangedDate_OldValue")
                      .HasColumnType("DATETIME")
                      .HasConversion(dateTimeConverterForHistory);
                entity.Property(e => e.NewValueDate)
                      .HasColumnName("System_ChangedDate_NewValue")
                      .HasColumnType("DATETIME")
                      .HasConversion(dateTimeConverterForHistory);
                entity.Property(e => e.OldValueColumn).HasColumnName("System_BoardColumn_OldValue");
                entity.Property(e => e.NewValueColumn).HasColumnName("System_BoardColumn_NewValue");
            });

            modelBuilder.Entity<Iteration>(entity =>
            {
                entity.ToTable("SPR");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.ApiId).HasColumnName("Api_Id");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.Path).HasColumnName("Path");
                entity.Property(e => e.StartDate).HasColumnName("StartDate");
                entity.Property(e => e.FinishDate).HasColumnName("FinishDate");
            });

            modelBuilder.Entity<WorkItemsList>(entity =>
            {
                entity.ToTable("ITEM_LIST");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.ApiId).HasColumnName("Api_Id");
                entity.Property(e => e.SprintId).HasColumnName("Spr_Id");
                entity.Property(e => e.Url).HasColumnName("url");
            });
        }

    }
}