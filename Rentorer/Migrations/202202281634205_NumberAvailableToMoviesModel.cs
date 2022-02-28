namespace Rentorer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberAvailableToMoviesModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}
