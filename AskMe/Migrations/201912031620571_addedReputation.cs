namespace AskMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedReputation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Reputation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Reputation");
        }
    }
}
