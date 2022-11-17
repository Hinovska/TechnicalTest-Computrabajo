using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Redarbor.DataRepository.DbModels.SqlServer
{
    public partial class RedarborContext : DbContext
    {
        private string _connectionString;
        public RedarborContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("SQLServerRedarbor");
        }

        public RedarborContext(DbContextOptions<RedarborContext> options) : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Portal> Portal { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(70);

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime2(6)");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.Email)
                    .HasName("ix_employee_email");

                entity.HasIndex(e => e.Username)
                    .HasName("ix_employee_username");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(60);

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(25);

                entity.Property(e => e.PortalId).HasColumnName("portal_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(12);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(25);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_company");

                entity.HasOne(d => d.Portal)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PortalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_portal");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_role");
            });

            modelBuilder.Entity<Portal>(entity =>
            {
                entity.ToTable("portal");

                entity.Property(e => e.PortalId).HasColumnName("portal_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(70);

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime2(6)");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime2(6)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(70);

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasColumnType("datetime2(6)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
