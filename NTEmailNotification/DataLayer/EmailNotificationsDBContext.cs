
using EmailNotifications.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailNotifications.DataLayer
{
    public class EmailNotificationsDBContext : DbContext
    {
        public EmailNotificationsDBContext(DbContextOptions<EmailNotificationsDBContext> options)
            : base(options)

        {
        }


            // DbSet properties for each entity
            public DbSet<Tenders> Tenders { get; set; }
            public DbSet<Awards> Awards { get; set; }
            public DbSet<Bidders> Bidders { get; set; }
            public DbSet<Closed> Closed { get; set; }
            public DbSet<Cancelled> Cancelled { get; set; }
            public DbSet<Provinces> Provinces { get; set; }
            public DbSet<Categories> Categories { get; set; }
            public DbSet<Departments> Departments { get; set; }
            public DbSet<Clusters> Clusters { get; set; }
            public DbSet<SupportDocument> SupportDocuments { get; set; }
            public DbSet<EsubmissionDocument> EsubmissionDocuments { get; set; }
            public DbSet<EsubmissionRequirements> EsubmissionRequirements { get; set; }
            public DbSet<EsubmissionRequirementsChecklist> EsubmissionCompletedRequirements { get; set; }
            public DbSet<QuestionsAndAnswer> QuestionsAndAnswers { get; set; }
            public DbSet<Bidder> EsubmissionBidder { get; set; }
            public DbSet<Esubmission> Esubmission { get; set; }
            public DbSet<DeclarationOfInterest> EsubmissionDeclaration { get; set; }
            public DbSet<EsubmissionSupplierDirectors> EsubmissionSupplierDirectors { get; set; }
            public DbSet<Discussion> Discussions { get; set; }
            public DbSet<Email_Notifications> Email_Notifications { get; set; }
            public DbSet<Contract> Contract { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Configure decimal properties with precision and scale
                modelBuilder.Entity<Contract>()
                    .Property(c => c.PercentageEscalation)
                    .HasColumnType("decimal(18, 2)"); // Precision: 18, Scale: 2

                modelBuilder.Entity<Contract>()
                    .Property(c => c.ContractAmountPaid)
                    .HasColumnType("decimal(18, 2)");

                modelBuilder.Entity<Contract>()
                    .Property(c => c.ContractBalance)
                    .HasColumnType("decimal(18, 2)");

                modelBuilder.Entity<Contract>()
                    .Property(c => c.ContractValue)
                    .HasColumnType("decimal(18, 2)");
            }
        }
    }

