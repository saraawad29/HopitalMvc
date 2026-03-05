using HopitalMvcSqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace HopitalMvcSqlite.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Department> Departments => Set<Department>();

    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Consultation> Consultations => Set<Consultation>();
    public DbSet<DoctorPatient> DoctorPatients => Set<DoctorPatient>();
    public DbSet<Staff> Staff => Set<Staff>();
    public DbSet<Nurse> Nurses => Set<Nurse>();
    public DbSet<AdminStaff> AdminStaffs => Set<AdminStaff>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Patient: FileNumber unique
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.FileNumber)
            .IsUnique();

        // Patient: Email unique
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.Email)
            .IsUnique();

        // Champs obligatoires (au niveau DB)
        modelBuilder.Entity<Patient>()
            .Property(p => p.LastName)
            .IsRequired();

        modelBuilder.Entity<Patient>()
            .Property(p => p.FirstName)
            .IsRequired();

        modelBuilder.Entity<Department>()
            .Property(d => d.Name)
            .IsRequired();

        // LicenseNumber unique
        modelBuilder.Entity<Doctor>()
            .HasIndex(d => d.LicenseNumber)
            .IsUnique();

        // Le comportement choisi est : DeleteBehavior.Restrict
        // car Un médecin est une entité importante du système.
        // Supprimer un département ne doit pas supprimer les médecins.
        // Cela évite une perte de données accidentelle.
        // Relation Department 1..* Doctors (Restrict recommandé)
        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Department)
            .WithMany(dep => dep.Doctors)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Chef médical : Department -> Doctor (SetNull si le doctor est supprimé)
        modelBuilder.Entity<Department>()
            .HasOne(d => d.MedicalChief)
            .WithMany()
            .HasForeignKey(d => d.MedicalChiefId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relation Patient 1..* Consultations
        modelBuilder.Entity<Consultation>()
            .HasOne(c => c.Patient)
            .WithMany(p => p.Consultations)
            .HasForeignKey(c => c.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation Doctor 1..* Consultations
        modelBuilder.Entity<Consultation>()
            .HasOne(c => c.Doctor)
            .WithMany(d => d.Consultations)
            .HasForeignKey(c => c.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Empêcher doublon : même médecin même date
        modelBuilder.Entity<Consultation>()
            .HasIndex(c => new { c.DoctorId, c.Date })
            .IsUnique();

        modelBuilder.Entity<DoctorPatient>()
        .HasKey(dp => new { dp.DoctorId, dp.PatientId });

        modelBuilder.Entity<DoctorPatient>()
            .HasOne(dp => dp.Doctor)
            .WithMany(d => d.DoctorPatients)
            .HasForeignKey(dp => dp.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DoctorPatient>()
            .HasOne(dp => dp.Patient)
            .WithMany(p => p.DoctorPatients)
            .HasForeignKey(dp => dp.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Person>()
        //     .HasDiscriminator<string>("PersonType")
        //     .HasValue<Patient>("Patient")
        //     .HasValue<Doctor>("Doctor");

        modelBuilder.Entity<Patient>().OwnsOne(p => p.Address, a =>
        {
            a.Property(x => x.Street).HasColumnName("Address_Street");
            a.Property(x => x.City).HasColumnName("Address_City");
            a.Property(x => x.PostalCode).HasColumnName("Address_PostalCode");
            a.Property(x => x.Country).HasColumnName("Address_Country");
        });

        modelBuilder.Entity<Department>().OwnsOne(d => d.ContactAddress, a =>
        {
            a.Property(x => x.Street).HasColumnName("Contact_Street");
            a.Property(x => x.City).HasColumnName("Contact_City");
            a.Property(x => x.PostalCode).HasColumnName("Contact_PostalCode");
            a.Property(x => x.Country).HasColumnName("Contact_Country");
        });


        modelBuilder.Entity<Staff>()
        .HasDiscriminator<string>("StaffType")
        .HasValue<Doctor>("Doctor")
        .HasValue<Nurse>("Nurse")
        .HasValue<AdminStaff>("Admin");


        modelBuilder.Entity<Department>()
        .HasOne(d => d.ParentDepartment)
        .WithMany(d => d.SubDepartments)
        .HasForeignKey(d => d.ParentDepartmentId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}