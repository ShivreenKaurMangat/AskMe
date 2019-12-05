namespace AskMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnswerProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsAnswerRight", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsAnswerRight");
        }
    }
}
