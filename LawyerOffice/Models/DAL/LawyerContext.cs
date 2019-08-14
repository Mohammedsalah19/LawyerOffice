using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LawyerOffice.Models.DAL
{
    public class LawyerContext:DbContext
    {
        public LawyerContext():base("DBLaywerOffice")
        {

        }


        public DbSet<Client> Client { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Cases> Cases { get; set; }
        public DbSet<Courts> Court { get; set; }
        public DbSet<attachements> attachements { get; set; }
        public DbSet<elmohdar> elmohdar { get; set; }
        public DbSet<eltawkeel> eltawkeel { get; set; }
        public DbSet<fees> feeses { get; set; }
        public DbSet<elgalasat> elgalasat { get; set; }
        public DbSet<City> city { get; set; }

        public DbSet<Employees> Employee { get; set; }
        public DbSet<Jobs> Jobs { get; set; }

       public DbSet<Offices> Offices { get; set; }
        public DbSet<HowCanAcess> HowCanAcess { get; set; }
        public DbSet<attachmentType> attachmentType { get; set; }
        public DbSet<CourtType> CourtType { get; set; }
        public DbSet<Salaries> Salaries { get; set; }
        public DbSet<Month> Month { get; set; }
        public DbSet<AllPayments> AllPayment { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<ExpensesModel> Expenses { get; set; }
        public DbSet<Expensestypes> Expensestypes { get; set; }
        public DbSet<Library> Library { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<Tables> Tables { get; set; }


        





        public System.Data.Entity.DbSet<LawyerOffice.Models.CasesAndGalasat> CasesAndGalasats { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.EmployeeExtinsions> EmployeeExtinsions { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.CasesAndClients> CasesAndClients { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.ClientWithCases> ClientWithCases { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.DAL.IEClinetsWithIECase> IEClinetsWithIECases { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.hcaAndEmp> hcaAndEmps { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.EmpOfficeJob> EmpOfficeJobs { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.CasesCourtsEmployees> CasesCourtsEmployees { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.CasesAndFeeses> CasesAndFeeses { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.SalaryWithEmpAndMonth> SalaryWithEmpAndMonths { get; set; }

        public System.Data.Entity.DbSet<LawyerOffice.Models.ExpensesAndTypes> ExpensesAndTypes { get; set; }
    }
}