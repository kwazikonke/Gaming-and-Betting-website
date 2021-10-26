namespace Green.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Holidays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Holidays", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Holidays", "Name");
        }
    }
}
