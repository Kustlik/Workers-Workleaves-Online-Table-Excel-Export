namespace AstraRekrutacja.Migrations
{
    using AstraRekrutacja.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AstraRekrutacja.DB.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AstraRekrutacja.DB.MyDbContext context)
        {

            List<Manager> managers = new List<Manager>
            {
                new Manager {ManagerId = 1, FirstName = "Antonio", LastName="Banderas"},
                new Manager {ManagerId = 2, FirstName = "Jessica", LastName="Brown"},
                new Manager {ManagerId = 3, FirstName = "Sancho", LastName="Pansa"}
            };

            managers.ForEach(a => context.Managers.AddOrUpdate(b => b.ManagerId, a));
            context.SaveChanges();

            List<Worker> workers = new List<Worker>
            {
                new Worker {WorkerId = 1, ManagerId=1, FirstName = "John", LastName="Kowalski"},
                new Worker {WorkerId = 2, ManagerId=2, FirstName = "John", LastName="Krasinski"},
                new Worker {WorkerId = 3, ManagerId=3, FirstName = "Leonardo", LastName="Dicaprio"},
                new Worker {WorkerId = 4, ManagerId=1, FirstName = "Penelope", LastName="Cruz"},
                new Worker {WorkerId = 5, ManagerId=2, FirstName = "Josh", LastName="Brolin"},
                new Worker {WorkerId = 6, ManagerId=3, FirstName = "Nicole", LastName="Kidman"},
                new Worker {WorkerId = 7, ManagerId=1, FirstName = "Johnny", LastName="Depp"},
                new Worker {WorkerId = 8, ManagerId=2, FirstName = "Ben", LastName="Stiller"},
                new Worker {WorkerId = 9, ManagerId=3, FirstName = "Gregory", LastName="Onion"}
            };

            workers.ForEach(w => context.Workers.AddOrUpdate(p => p.WorkerId, w));
            context.SaveChanges();

            List<WorkleaveType> wltypes = new List<WorkleaveType>
            {
                new WorkleaveType {WorkleaveTypeId = 1, WorkleaveName = "Urlop na żądanie", Active= true, WorkleaveTemplate= "KOD_XML1"},
                new WorkleaveType {WorkleaveTypeId = 2, WorkleaveName = "Urlop wypoczynkowy", Active= true, WorkleaveTemplate= "KOD_XML555"},
                new WorkleaveType {WorkleaveTypeId = 3, WorkleaveName = "Urlop wypoczynkowyOLD", Active= false, WorkleaveTemplate= "KOD_XML1_NEW"},
                new WorkleaveType {WorkleaveTypeId = 4, WorkleaveName = "Urlop ojcowski", Active= true, WorkleaveTemplate= "KOD_XML421"},
            };
            wltypes.ForEach(a => context.WorkleaveTypes.AddOrUpdate(b => b.WorkleaveTypeId, a));
            context.SaveChanges();

            List<Workleave> workleaves = new List<Workleave>
            {
                new Workleave {WorkleaveId =1,WorkerId = 1, Days = 4, StartOfWorkleave = new DateTime(2022,01,26), Created = new DateTime(2022,01,03), Status = (WorkleaveStatus)10, WorkleaveTypeId= 1},
                new Workleave {WorkleaveId =2,WorkerId = 2, Days = 1, StartOfWorkleave = new DateTime(2022,02,03), Created = new DateTime(2022,01,02), Status = (WorkleaveStatus)20, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =3,WorkerId = 3, Days = 3, StartOfWorkleave = new DateTime(2022,01,31), Created = new DateTime(2021,12,30), Status = (WorkleaveStatus)10, WorkleaveTypeId= 2 },
                new Workleave {WorkleaveId =4,WorkerId = 4, Days = 6, StartOfWorkleave = new DateTime(2022,01,25), Created = new DateTime(2022,01,04), Status = (WorkleaveStatus)10, WorkleaveTypeId= 2 },
                new Workleave {WorkleaveId =5,WorkerId = 5, Days = 7, StartOfWorkleave = new DateTime(2022,01,22), Created =new DateTime(2022,01 ,01), Status = (WorkleaveStatus)0, WorkleaveTypeId= 3 },
                new Workleave {WorkleaveId =6,WorkerId = 6, Days = 5, StartOfWorkleave = new DateTime(2022,01,27), Created = new DateTime(2022,01,02), Status = (WorkleaveStatus)10, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =7,WorkerId = 7, Days = 10, StartOfWorkleave = new DateTime(2022,01,28 ), Created = new DateTime(2022,01,05), Status = (WorkleaveStatus)0, WorkleaveTypeId= 3 },
                new Workleave {WorkleaveId =8,WorkerId = 8, Days = 3, StartOfWorkleave = new DateTime(2022,02,06 ), Created = new DateTime(2022,01,01), Status = (WorkleaveStatus)20, WorkleaveTypeId= 2 },
                new Workleave {WorkleaveId =9,WorkerId = 9, Days = 4, StartOfWorkleave = new DateTime(2022,01,28 ), Created = new DateTime(2021,12,30), Status = (WorkleaveStatus)30, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =10,WorkerId = 1, Days = 4, StartOfWorkleave = new DateTime(2022,01,24 ), Created = new DateTime(2022,01,01), Status = (WorkleaveStatus)30, WorkleaveTypeId= 3 },
                new Workleave {WorkleaveId =11,WorkerId = 2, Days = 4, StartOfWorkleave = new DateTime(2022,02,07 ), Created = new DateTime(2022,01,04), Status = (WorkleaveStatus)100, WorkleaveTypeId= 3 },
                new Workleave {WorkleaveId =12,WorkerId = 3, Days = 10, StartOfWorkleave = new DateTime(2022,02,03 ), Created = new DateTime(2022,01,04), Status = (WorkleaveStatus)10, WorkleaveTypeId= 4 },
                new Workleave {WorkleaveId =13,WorkerId = 4, Days = 7, StartOfWorkleave = new DateTime(2022,01,24 ), Created = new DateTime(2021,12,31), Status = (WorkleaveStatus)30, WorkleaveTypeId= 2 },
                new Workleave {WorkleaveId =14,WorkerId = 5, Days = 9, StartOfWorkleave = new DateTime(2022,01,25 ), Created = new DateTime(2022,01,26), Status = (WorkleaveStatus)30, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =15,WorkerId = 6, Days = 1, StartOfWorkleave = new DateTime(2022,01,30 ), Created = new DateTime(2022,01,06), Status = (WorkleaveStatus)30, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =16,WorkerId = 7, Days = 1, StartOfWorkleave = new DateTime(2022,01,26 ), Created = new DateTime(2022,01,05), Status = (WorkleaveStatus)20, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =17,WorkerId = 8, Days = 1, StartOfWorkleave = new DateTime(2022,01,20 ), Created = new DateTime(2022,01,03), Status = (WorkleaveStatus)10, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =18,WorkerId = 9, Days = 3, StartOfWorkleave = new DateTime(2022,01,30 ), Created = new DateTime(2021,12,31), Status = (WorkleaveStatus)20, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =19,WorkerId = 1, Days = 2, StartOfWorkleave = new DateTime(2022,01,24 ), Created = new DateTime(2022,01,02), Status = (WorkleaveStatus)30, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =20,WorkerId = 2, Days = 2, StartOfWorkleave = new DateTime(2022,01,25 ), Created = new DateTime(2022,01,07), Status = (WorkleaveStatus)30, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =21,WorkerId = 3, Days = 2, StartOfWorkleave = new DateTime(2022,02,03 ), Created = new DateTime(2022,01,02), Status = (WorkleaveStatus)30, WorkleaveTypeId= 1 },
                new Workleave {WorkleaveId =22,WorkerId = 4, Days = 6, StartOfWorkleave = new DateTime(2022,02,07 ), Created = new DateTime(2022,01,04), Status = (WorkleaveStatus)30, WorkleaveTypeId= 3 },
                new Workleave {WorkleaveId =23,WorkerId = 5, Days = 3, StartOfWorkleave =new DateTime(2022,02,02 ), Created = new DateTime(2022,01,07), Status = (WorkleaveStatus)30, WorkleaveTypeId= 2 },
                new Workleave {WorkleaveId =24,WorkerId = 6, Days = 5, StartOfWorkleave = new DateTime(2022,01,24 ), Created = new DateTime(2022,01,03), Status = (WorkleaveStatus)30, WorkleaveTypeId= 4 }
            };
            workleaves.ForEach(a => context.Workleaves.AddOrUpdate(b => b.WorkleaveId, a));
            context.SaveChanges();
        }
    }
}
