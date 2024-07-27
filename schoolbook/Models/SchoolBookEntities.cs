using Microsoft.EntityFrameworkCore;

namespace schoolbook.Models
{
    public class SchoolBookEntities : DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=SchoolBook;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
            modelBuilder.Entity<Operation>()
                .HasIndex(x => x.Id)
                .IsUnique();

            // Relation Between BooK and User
            modelBuilder.Entity<Book>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.UserId);

            // Relation Between BooK and Operation
            modelBuilder.Entity<Operation>()
                .HasOne<Book>()
                .WithOne();
                
               
            // Relation Between User and Operation
            modelBuilder.Entity<Operation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f =>f.buyerrId );

            // Relation Between Book and BookImage
            modelBuilder.Entity<BookImage>()
                .HasOne<Book>()
                .WithMany()
                .HasForeignKey(f => f.BookId);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books  { get; set; }
        public DbSet<Operation> Operations  { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
    } 

}
