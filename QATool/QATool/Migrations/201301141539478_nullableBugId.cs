namespace QATool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableBugId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(maxLength: 120),
                        JiraURL = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.TestCases",
                c => new
                    {
                        TestCaseId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Scenario = c.String(nullable: false, maxLength: 120),
                        Feature = c.String(nullable: false, maxLength: 50),
                        Result = c.String(nullable: false),
                        BugId = c.Int(),
                        Comments = c.String(),
                        Environment = c.String(),
                    })
                .PrimaryKey(t => t.TestCaseId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        StepId = c.Int(nullable: false, identity: true),
                        TestCaseId = c.Int(nullable: false),
                        Action = c.String(nullable: false),
                        Result = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StepId)
                .ForeignKey("dbo.TestCases", t => t.TestCaseId, cascadeDelete: true)
                .Index(t => t.TestCaseId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Steps", new[] { "TestCaseId" });
            DropIndex("dbo.TestCases", new[] { "ProjectId" });
            DropForeignKey("dbo.Steps", "TestCaseId", "dbo.TestCases");
            DropForeignKey("dbo.TestCases", "ProjectId", "dbo.Projects");
            DropTable("dbo.Steps");
            DropTable("dbo.TestCases");
            DropTable("dbo.Projects");
        }
    }
}
