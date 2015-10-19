namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameideastofeedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Comment = c.String(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
            DropTable("dbo.Ideas");
        }
        
        public override void Down()
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
            
            DropTable("dbo.Feedbacks");
        }
    }
}
