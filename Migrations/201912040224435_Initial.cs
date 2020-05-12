namespace JZappV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "JobType", c => c.String(nullable: false));
            AlterColumn("dbo.Jobs", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Description", c => c.String());
            AlterColumn("dbo.Jobs", "JobType", c => c.String());
        }
    }
}
