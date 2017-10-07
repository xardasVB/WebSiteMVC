namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateHotelsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        DateCreate = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "CityId", "dbo.Cities");
            DropIndex("dbo.Hotels", new[] { "CityId" });
            DropTable("dbo.Hotels");
        }
    }
}
