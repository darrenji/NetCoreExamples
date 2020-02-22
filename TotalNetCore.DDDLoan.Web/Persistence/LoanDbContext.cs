using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Web.Persistence
{
    public class LoanDbContext : DbContext
    {
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<Operator> Operators { get; set; }

        public LoanDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanApplication>()
                .Property(t => t.Number);
            modelBuilder.Entity<LoanApplication>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<LoanApplication>()
                .Property(t => t.Id)
                .HasConversion(x => x.Value, x => new LoanApplicationId(x));//定义如何写入和读取

            //枚举转换成字符串
            var converter = new EnumToStringConverter<LoanApplicationStatus>();
            var converterForScore = new EnumToStringConverter<ApplicationScore>();

            modelBuilder.Entity<LoanApplication>().Property(t => t.Status).HasConversion(converter);

            //LoanApplication和Score的关系，但Score并不在数据库中
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Score, s => {
                    s.Property(x => x.Explanation);
                    s.Property(x => x.Score).HasConversion(converterForScore);
                });

            //LoanApplication与Customer以及Customer中的Address关系
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(t => t.Customer)
                .OwnsOne(t => t.Address, ca =>
                {
                    ca.Property(x => x.Country);
                    ca.Property(x => x.City);
                    ca.Property(x => x.ZipCode);
                    ca.Property(x => x.Street);
                });

            //LoanApplication与Customer以及Customer中的NatianalIdentifier中的关系
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(t => t.Customer)
                .OwnsOne(t => t.NationalIdentifier, ni => ni.Property(x => x.Value));

            //LocationApplication, Customer, Name
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Customer)
                .OwnsOne(c => c.Name, cn =>
                {
                    cn.Property(x => x.First);
                    cn.Property(x => x.Last);
                });

            //LoanApplication与Customer, Birthdate
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(A => A.Customer, c => c.Property(x => x.Birthdate));

            //LoanApplicaiton,Cutomer, MonthlyIncome, Amount
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Customer)
                .OwnsOne(a => a.MonthlyIncome, ma => ma.Property(x => x.Amount));

            //LoanApplication, Property, Address
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Property)
                .OwnsOne(p => p.Address, pa =>
                {
                    pa.Property(x => x.Country);
                    pa.Property(x => x.City);
                    pa.Property(x => x.ZipCode);
                    pa.Property(x => x.Street);
                });

            //LoanApplication, Property, Value
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Property)
                .OwnsOne(p => p.Value, pv => pv.Property(x => x.Amount));

            //LoanApplication, Loan,InterestRate
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Loan)
                .OwnsOne(l => l.InterestRate, ir => ir.Property(x => x.Value));

            //LoanApplicaiton, Loan, LoanAmount
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Loan)
                .OwnsOne(l => l.LoanAmount, la => { la.Property(x => x.Amount); });

            //LoanApplication, Loan, LoanNumberOfYears
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Loan, l => l.Property(x=>x.LoanNumberOfYears));

            //LoanApplication, Loan, DecisionBy + DecisionDate
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a=>a.Decision, d=> {
                    d.OwnsOne(x => x.DecisionBy, db => db.Property(y => y.Value));
                    d.Property(x => x.DecisionDate);
                });

            //LoanApplicaiton, Registration, ReigsteredBy+RegistrationDate
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Registration, r => {
                    r.OwnsOne(x => x.RegisteredBy, db => db.Property(y => y.Value));
                    r.Property(x => x.RegistrationDate);
                });

            modelBuilder.Entity<Operator>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Operator>()
                .Property(l => l.Id)
                .HasConversion(x => x.Value, x => new OperatorId(x));

            modelBuilder.Entity<Operator>()
                .OwnsOne(o => o.CompetenceLevel, cl => cl.Property(c => c.Amount));
        }
    }
}
