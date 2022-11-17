using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Redarbor.DataRepository.DBModel
{
    public partial class RedarborContext : DbContext
    {
        private string _connectionString;
        public RedarborContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("PostgresRedarbor");
        }

        public RedarborContext(DbContextOptions<RedarborContext> options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasComment("Holds data for employees");

                entity.Property(e => e.EmployeeId)
                     .HasColumnName("employee_id")
                     .HasDefaultValueSql("uuid_generate_v4()")
                     .HasComment("Unique value that identifies the employee locally on the table");

                entity.HasIndex(e => e.Email)
                    .HasName("ix_employee_email");

                entity.HasIndex(e => e.Username)
                    .HasName("ix_employee_username");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("company_id")
                    .HasComment("Current portal for a employee.");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasComment("Record to keep track of the creation");

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasComment("Record to keep track of the delete");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("character varying")
                    .HasComment("Employee´s email address.");

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasColumnType("character varying")
                    .HasComment("Employee fax number.");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasComment("Record to keep track of the last login");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying")
                    .HasComment("Name of the product");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("character varying")
                    .HasComment("Employee´s password.");

                entity.Property(e => e.PortalId)
                    .HasColumnName("portal_id")
                    .HasComment("Current portal for a employee.");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasComment("Current role for a employee.");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasComment("Current status for a employee.");

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasColumnType("character varying")
                    .HasComment("Employee´s telephone number.");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updated_on")
                    .HasComment("Record to keep track of the last edit");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("character varying")
                    .HasComment("Employee´s username.");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
