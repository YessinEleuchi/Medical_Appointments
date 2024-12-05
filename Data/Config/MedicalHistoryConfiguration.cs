using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Medical_Appointments_API.Data.Models;



namespace Medical_Appointments_API.Data.Config
{
    public class MedicalHistoryConfiguration : IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {
            builder.ToTable("MedicalHistories");

            builder.HasKey(mh => mh.MedicalHistoryID);

            // Foreign key relationship with ApplicationUser (Patient)
            builder.HasOne(mh => mh.Patient)
                .WithMany(u => u.MedicalHistories)
                .HasForeignKey(mh => mh.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the DateOfEntry property to be required with a default value
            builder.Property(mh => mh.DateOfEntry)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Index on UserId and DateOfEntry for performance
            builder.HasIndex(mh => mh.UserId).HasDatabaseName("IX_MedicalHistory_UserId");
            builder.HasIndex(mh => mh.DateOfEntry).HasDatabaseName("IX_MedicalHistory_DateOfEntry");
        }
    }
}
