namespace Green.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientSurvey1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientSurvey1",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        firstname = c.String(),
                        email = c.String(),
                        Ratepricing = c.String(),
                        RatequalityService = c.String(),
                        RateProcedure = c.String(),
                        Ratedentist = c.String(),
                        RateThoroughness = c.String(),
                        RateAnswerfromDent = c.String(),
                        RateDentFriendly = c.String(),
                        RateOverallExperience = c.String(),
                        DrName = c.String(),
                        recommend = c.String(),
                        what_to_change_or_improve = c.String(),
                        date = c.DateTime(nullable: false),
                        can_user_be_contacted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PatientSurvey1");
        }
    }
}
