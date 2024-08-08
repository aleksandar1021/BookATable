using BookATable.Domain;
using BookATable.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.DataAccess
{
    public class Context : DbContext
    {
        private readonly string _connectionString;
        public Context(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string conn { get => _connectionString; }

        public Context()
        {
            _connectionString = "Data Source=LAPTOP-GI6JHEKS;Initial Catalog=BookTable;TrustServerCertificate=true;Integrated security = true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.IsActive = true;
                        e.CreatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChanges();
        }


        public DbSet<MealCategory> MealCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RestaurantType> RestaurantTypes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MealCategoryRestaurant> MealCategoryRestaurants { get; set; }
        public DbSet<RestaurantImage> RestaurantImages { get; set; }
        public DbSet<Dish> Dishs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Appendice> Appendices { get; set; }
        public DbSet<AppendiceRestaurant> AppendiceRestaurants { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationAppendice> ReservationAppendices { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Saved> Saved { get; set; }
        public DbSet<RegularClosedDays> RegularClosedDays { get; set; }

        public DbSet<SpecificClosedDays> SpecificClosedDays { get; set; }
    }
}
