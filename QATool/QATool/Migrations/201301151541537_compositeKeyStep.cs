namespace QATool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compositeKeyStep : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Steps", new[] { "StepId" });
            AddPrimaryKey("dbo.Steps", new[] { "StepId", "TestCaseId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Steps", new[] { "StepId", "TestCaseId" });
            AddPrimaryKey("dbo.Steps", "StepId");
        }
    }
}
