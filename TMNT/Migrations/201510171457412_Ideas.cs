namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ideas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        IdeaId = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Comment = c.String(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.IdeaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ideas");
        }
    }
}
