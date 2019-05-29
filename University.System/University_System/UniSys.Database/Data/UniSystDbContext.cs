namespace UniSys.Database.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models.Users;

    internal class UniSystDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureLecturerEntity(builder);

            ConfigureStudentEntity(builder);

            ConfigureSubjectEntity(builder);
        }

        private void ConfigureLecturerEntity(ModelBuilder builder)
        {
            builder
                .Entity<Lecturer>()
                .HasKey(l => l.LecturerId);
            builder
                .Entity<Lecturer>()
                .HasMany(l => l.SubjectCollection);
            builder
                .Entity<Lecturer>()
                .Property(l => l.FirstName)
                .HasMaxLength(50)
                .IsUnicode();
            builder
                .Entity<Lecturer>()
                .Property(l => l.LastName)
                .HasMaxLength(50)
                .IsUnicode();
            builder
                .Entity<Lecturer>()
                .Property(l => l.PersonalNumber)
                .HasMaxLength(10)
                .IsUnicode();
            builder
                .Entity<Lecturer>()
                .Property(l => l.BirthDate);
            builder
                .Entity<Lecturer>()
                .Property(l => l.Gender);
            builder
                .Entity<Lecturer>()
                .Property(l => l.Title)
                .IsUnicode();
            builder
                .Entity<Lecturer>()
                .Property(l => l.Image);
            builder
                .Entity<Lecturer>()
                .Property(l => l.Faculty)
                .IsUnicode();
            builder
                .Entity<Lecturer>()
                .Property(l => l.Region);
        }

        private void ConfigureStudentEntity(ModelBuilder builder)
        {
            builder
               .Entity<Student>()
               .HasKey(s => s.StudentId);
            builder
                .Entity<Student>()
                .Property(s => s.FirstName)
                .HasMaxLength(50)
                .IsUnicode();
            builder
                .Entity<Student>()
                .Property(s => s.LastName)
                .HasMaxLength(50)
                .IsUnicode();
            builder
                .Entity<Student>()
                .Property(s => s.PersonalNumber)
                .HasMaxLength(10);
            builder
                .Entity<Student>()
                .Property(s => s.BirthDate);
            builder
                .Entity<Student>()
                .Property(s => s.Gender);
            builder
                .Entity<Student>()
                .Property(s => s.Faculty)
                .IsUnicode();
            builder
                .Entity<Student>()
                .Property(s => s.FacultyNumber);
            builder
                .Entity<Student>()
                .Property(s => s.Specialty)
                .IsUnicode();
            builder
                .Entity<Student>()
                .Property(s => s.Region)
                .IsUnicode();
            builder
                .Entity<Student>()
                .Property(s => s.BirthDate);
            builder
                .Entity<Student>()
                .Property(s => s.Image);
        }

        private void ConfigureSubjectEntity(ModelBuilder builder)
        {
            builder
                .Entity<Subject>()
                .HasKey(s => s.SubjectId);
            builder
                .Entity<Subject>()
                .Property(s => s.Name)
                .HasMaxLength(50)
                .IsUnicode();
            builder
                .Entity<Subject>()
                .HasMany(s => s.Students);
        }
    }
}
