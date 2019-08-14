namespace LawyerOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agenda",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        numberCase = c.Int(nullable: false),
                        FileNumber = c.Int(nullable: false),
                        DateOfGalas = c.DateTime(nullable: false),
                        ClientName = c.String(),
                        TypeOfClinet = c.String(),
                        GuilityName = c.String(),
                        TypeOfGuility = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AllPayments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.attachements",
                c => new
                    {
                        Atachment_id = c.Int(nullable: false, identity: true),
                        titlte = c.String(),
                        ImagePath = c.String(),
                        AttType = c.Int(nullable: false),
                        CaseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Atachment_id);
            
            CreateTable(
                "dbo.attachmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AttType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        numberOfCase = c.Int(nullable: false),
                        numberOfFile = c.Int(nullable: false),
                        AddressofClient = c.String(nullable: false),
                        TypeofClient = c.String(),
                        NumberoOfTawkeel = c.Int(nullable: false),
                        TypeoOfTawkeel = c.String(),
                        degreeOfCase = c.String(),
                        dateofElkaeed = c.DateTime(nullable: false),
                        nameofGuillty = c.String(nullable: false),
                        AddressoFGuility = c.String(nullable: false),
                        TypeofGuility = c.String(),
                        CourtName = c.String(nullable: false),
                        Eldarea = c.String(),
                        subject = c.String(nullable: false),
                        clientID = c.Int(nullable: false),
                        employeeName = c.String(nullable: false),
                        IsEnabled = c.String(),
                        money = c.Int(nullable: false),
                        CourtType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CasesAndClients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Client_ID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clients", t => t.Client_ID)
                .Index(t => t.Client_ID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Nationality = c.String(),
                        Address = c.String(nullable: false),
                        eldiana = c.String(nullable: false),
                        Mobile = c.String(nullable: false),
                        Email = c.String(),
                        Notes = c.String(),
                        employeeName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CasesAndFeeses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CasesAndGalasats",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CasesCourtsEmployees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ClientWithCases",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ClientX_ID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Clients", t => t.ClientX_ID)
                .Index(t => t.ClientX_ID);
            
            CreateTable(
                "dbo.Courts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CourtName = c.String(),
                        CourtType = c.String(),
                        cityid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.CourtTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.elgalasats",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        NumberOfGalsa = c.Int(nullable: false),
                        NumberOfCase = c.Int(nullable: false),
                        dateOfGalsa = c.DateTime(nullable: false),
                        RollNumber = c.String(nullable: false),
                        CourtName = c.String(nullable: false),
                        eldarea = c.String(),
                        desicion = c.String(nullable: false),
                        orders = c.String(nullable: false),
                        EmpName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.elmohdars",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MohdarPen = c.String(),
                        GalsaDate = c.DateTime(nullable: false),
                        lawyer = c.String(),
                        note = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.eltawkeels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                        place = c.String(),
                        date = c.DateTime(nullable: false),
                        notes = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        employeeName = c.String(),
                        Address = c.String(),
                        phone = c.String(),
                        jobs = c.String(),
                        Salary = c.Int(nullable: false),
                        OfficeName = c.String(),
                        Paswword = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EmployeeExtinsions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EmpOfficeJobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmpY_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Employees", t => t.EmpY_id)
                .Index(t => t.EmpY_id);
            
            CreateTable(
                "dbo.ExpensesModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Expensestype = c.String(),
                        values = c.Int(nullable: false),
                        Comment = c.String(),
                        office = c.String(),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ExpensesAndTypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Expensestypes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        catogery = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.fees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumberOfCase = c.Int(nullable: false),
                        recived = c.Int(nullable: false),
                        DateofRevice = c.DateTime(nullable: false),
                        notes = c.String(),
                        EmployeeName = c.String(),
                        clientID_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.clientID_ID)
                .Index(t => t.clientID_ID);
            
            CreateTable(
                "dbo.hcaAndEmps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmpX_id = c.Int(),
                        hcaX_ID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Employees", t => t.EmpX_id)
                .ForeignKey("dbo.HowCanAcesses", t => t.hcaX_ID)
                .Index(t => t.EmpX_id)
                .Index(t => t.hcaX_ID);
            
            CreateTable(
                "dbo.HowCanAcesses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        employeeID = c.Int(nullable: false),
                        Agenda = c.Boolean(nullable: false),
                        Cases = c.Boolean(nullable: false),
                        Employee = c.Boolean(nullable: false),
                        Clients = c.Boolean(nullable: false),
                        Staticis = c.Boolean(nullable: false),
                        Setting = c.Boolean(nullable: false),
                        library = c.Boolean(nullable: false),
                        AccessAll = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IEClinetsWithIECases",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Infoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PlaceName = c.String(),
                        description = c.String(),
                        Number = c.String(),
                        Img = c.String(),
                        DocumentPath = c.String(),
                        PhyscialPath = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        jobs = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Libraries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BookName = c.String(),
                        BookPath = c.String(),
                        Publisher = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        date = c.DateTime(nullable: false),
                        action = c.String(),
                        ipaddress = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        months = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfficeName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Employee = c.String(),
                        empSupplyer = c.String(),
                        salary = c.Int(nullable: false),
                        month = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SalaryWithEmpAndMonths",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        values = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.hcaAndEmps", "hcaX_ID", "dbo.HowCanAcesses");
            DropForeignKey("dbo.hcaAndEmps", "EmpX_id", "dbo.Employees");
            DropForeignKey("dbo.fees", "clientID_ID", "dbo.Clients");
            DropForeignKey("dbo.EmpOfficeJobs", "EmpY_id", "dbo.Employees");
            DropForeignKey("dbo.ClientWithCases", "ClientX_ID", "dbo.Clients");
            DropForeignKey("dbo.CasesAndClients", "Client_ID", "dbo.Clients");
            DropIndex("dbo.hcaAndEmps", new[] { "hcaX_ID" });
            DropIndex("dbo.hcaAndEmps", new[] { "EmpX_id" });
            DropIndex("dbo.fees", new[] { "clientID_ID" });
            DropIndex("dbo.EmpOfficeJobs", new[] { "EmpY_id" });
            DropIndex("dbo.ClientWithCases", new[] { "ClientX_ID" });
            DropIndex("dbo.CasesAndClients", new[] { "Client_ID" });
            DropTable("dbo.Tables");
            DropTable("dbo.SalaryWithEmpAndMonths");
            DropTable("dbo.Salaries");
            DropTable("dbo.Offices");
            DropTable("dbo.Months");
            DropTable("dbo.Logs");
            DropTable("dbo.Libraries");
            DropTable("dbo.Jobs");
            DropTable("dbo.Infoes");
            DropTable("dbo.IEClinetsWithIECases");
            DropTable("dbo.HowCanAcesses");
            DropTable("dbo.hcaAndEmps");
            DropTable("dbo.fees");
            DropTable("dbo.Expensestypes");
            DropTable("dbo.ExpensesAndTypes");
            DropTable("dbo.ExpensesModels");
            DropTable("dbo.EmpOfficeJobs");
            DropTable("dbo.EmployeeExtinsions");
            DropTable("dbo.Employees");
            DropTable("dbo.eltawkeels");
            DropTable("dbo.elmohdars");
            DropTable("dbo.elgalasats");
            DropTable("dbo.CourtTypes");
            DropTable("dbo.Courts");
            DropTable("dbo.ClientWithCases");
            DropTable("dbo.Cities");
            DropTable("dbo.CasesCourtsEmployees");
            DropTable("dbo.CasesAndGalasats");
            DropTable("dbo.CasesAndFeeses");
            DropTable("dbo.Clients");
            DropTable("dbo.CasesAndClients");
            DropTable("dbo.Cases");
            DropTable("dbo.attachmentTypes");
            DropTable("dbo.attachements");
            DropTable("dbo.AllPayments");
            DropTable("dbo.Agenda");
        }
    }
}
