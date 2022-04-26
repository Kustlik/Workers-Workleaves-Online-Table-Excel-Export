namespace AstraRekrutacja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ManagerId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        WorkerId = c.Int(nullable: false, identity: true),
                        ManagerId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.WorkerId)
                .ForeignKey("dbo.Managers", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Workleaves",
                c => new
                    {
                        WorkleaveId = c.Int(nullable: false, identity: true),
                        WorkerId = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        StartOfWorkleave = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        WorkleaveTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkleaveId)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .ForeignKey("dbo.WorkleaveTypes", t => t.WorkleaveTypeId, cascadeDelete: true)
                .Index(t => t.WorkerId)
                .Index(t => t.WorkleaveTypeId);
            
            CreateTable(
                "dbo.WorkleaveTypes",
                c => new
                    {
                        WorkleaveTypeId = c.Int(nullable: false, identity: true),
                        WorkleaveName = c.String(),
                        WorkleaveTemplate = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WorkleaveTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workleaves", "WorkleaveTypeId", "dbo.WorkleaveTypes");
            DropForeignKey("dbo.Workers", "ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Workleaves", "WorkerId", "dbo.Workers");
            DropIndex("dbo.Workleaves", new[] { "WorkleaveTypeId" });
            DropIndex("dbo.Workleaves", new[] { "WorkerId" });
            DropIndex("dbo.Workers", new[] { "ManagerId" });
            DropTable("dbo.WorkleaveTypes");
            DropTable("dbo.Workleaves");
            DropTable("dbo.Workers");
            DropTable("dbo.Managers");
        }
    }
}
