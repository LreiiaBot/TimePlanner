using Microsoft.EntityFrameworkCore;

namespace TimePlannerUpdated.Default
{
    class TimeplannerContext : DbContext
    {
        private const string ConnectionString = "server=127.0.0.1;database=timeplanner;user=root;password=;";

        public DbSet<UserTask> UserTask { get; set; }
        public DbSet<TaskList> TaskList { get; set; }
        public DbSet<Reminder> Reminder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.ToTable("UserTask");
                //entity.HasKey(e => e.Id);
                entity.Ignore(e => e.AutoOffsetReminders);
                entity.Ignore(e => e.AutoReminders);
            });

            modelBuilder.Entity<TaskList>(entity =>
            {
                entity.ToTable("TaskList");
                //entity.HasKey(e => e.Id);
                entity.Ignore(e => e.AutoOffsetReminders);
                entity.Ignore(e => e.AutoReminders);
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.ToTable("Reminder");
                entity.HasKey(e => e.Id);
                entity.Ignore(e => e.Parent);
            });
        }
    }
}
