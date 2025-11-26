using System.Data.Entity;
using ProtechGroup.Infrastructure.Entities;

namespace ProtechGroup.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("FlightBookingConnection")
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<SearchInput> SearchInputs { get; set; }
        public DbSet<SearchWSHistory> SearchWSHistorys { get; set; }
        public DbSet<ServiceFee> ServiceFees { get; set; }
        public DbSet<PriceBaggage> PriceBaggages { get; set; }
        public DbSet<OrderBaggage> OrderBaggages { get; set; }
        public DbSet<OrderBlockFlight> OrderBlockFlights { get; set; }
        public DbSet<OrderBooking> OrderBookings { get; set; }
        public DbSet<OrderContactlInfo> OrderContactInfos { get; set; }
        public DbSet<OrderFlight> OrderFlights { get; set; }
        public DbSet<OrderFlightSegment> OrderFlightSegments { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<OrderRemark> OrderRemarks { get; set; }
        public DbSet<OrderTraveller> OrderTravellers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Nếu cần mapping chi tiết:
            modelBuilder.Entity<Airport>().ToTable("Airport");
            modelBuilder.Entity<SearchInput>().ToTable("SearchInput");
            modelBuilder.Entity<SearchWSHistory>().ToTable("SearchWSHistory");
            modelBuilder.Entity<ServiceFee>().ToTable("ServiceFee");
            modelBuilder.Entity<PriceBaggage>().ToTable("PriceBaggage");
            modelBuilder.Entity<OrderBaggage>().ToTable("OrderBaggage");
            modelBuilder.Entity<OrderBlockFlight>().ToTable("OrderBlockFlight");
            modelBuilder.Entity<OrderBooking>().ToTable("OrderBooking");
            modelBuilder.Entity<OrderContactlInfo>().ToTable("OrderContactInfo");
            modelBuilder.Entity<OrderFlight>().ToTable("OrderFlight");
            modelBuilder.Entity<OrderFlightSegment>().ToTable("OrderFlightSegment");
            modelBuilder.Entity<OrderPayment>().ToTable("OrderPayment");
            modelBuilder.Entity<OrderRemark>().ToTable("OrderRemark");
            modelBuilder.Entity<OrderTraveller>().ToTable("OrderTraveller");
            // cấu hình column length nếu bạn muốn
            base.OnModelCreating(modelBuilder);
        }

    }
}
