namespace glassteeth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTweetModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "glassteeth.MyTweets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        Location = c.String(),
                        Sentiment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("glassteeth.MyTweets");
        }
    }
}
