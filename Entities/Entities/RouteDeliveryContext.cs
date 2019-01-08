using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RouterDelivery.Entities.Entities
{
    public partial class RouteDeliveryContext : DbContext
    {
        public RouteDeliveryContext()
        {
        }

        public RouteDeliveryContext(DbContextOptions<RouteDeliveryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<DeliverySchedule> DeliverySchedule { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<OptimizationRequest> OptimizationRequest { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<TransportType> TransportType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=RouteDelivery;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerLocation).HasMaxLength(255);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.TransportTypeId).HasColumnName("TransportTypeID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Delivery)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Delivery__Custom__1ED998B2");

                entity.HasOne(d => d.TransportType)
                    .WithMany(p => p.Delivery)
                    .HasForeignKey(d => d.TransportTypeId)
                    .HasConstraintName("FK__Delivery__Transp__1FCDBCEB");
            });

            modelBuilder.Entity<DeliverySchedule>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DriverName).HasMaxLength(255);

                entity.Property(e => e.EstimatedTime).HasColumnType("datetime");

                entity.Property(e => e.OptimizationRequestId).HasColumnName("OptimizationRequestID");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.TransportTypeId).HasColumnName("TransportTypeID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.DeliverySchedule)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__DeliveryS__Custo__2B3F6F97");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.DeliverySchedule)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__DeliveryS__Custo__2C3393D0");

                entity.HasOne(d => d.OptimizationRequest)
                    .WithMany(p => p.DeliverySchedule)
                    .HasForeignKey(d => d.OptimizationRequestId)
                    .HasConstraintName("FK__DeliveryS__Optim__2A4B4B5E");

                entity.HasOne(d => d.TransportType)
                    .WithMany(p => p.DeliverySchedule)
                    .HasForeignKey(d => d.TransportTypeId)
                    .HasConstraintName("FK__DeliveryS__Trans__2D27B809");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DriverName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartLocation)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TransportTypeId).HasColumnName("TransportTypeID");

                entity.HasOne(d => d.TransportType)
                    .WithMany(p => p.Driver)
                    .HasForeignKey(d => d.TransportTypeId)
                    .HasConstraintName("FK__Driver__Transpor__276EDEB3");
            });

            modelBuilder.Entity<OptimizationRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OptimizeDateTime).HasColumnType("datetime");

                entity.Property(e => e.RecurringSchedule)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.ScheduleDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.OptimizationRequest)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Optimizat__Statu__24927208");
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransportType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
