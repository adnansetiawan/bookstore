namespace BookStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryAndUpdateBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Books", "Description", c => c.String());
            AddColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Books", "Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
            CreateIndex("dbo.Books", "Category_Id");
            AddForeignKey("dbo.Books", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Books", new[] { "Category_Id" });
            AlterColumn("dbo.Books", "Title", c => c.String());
            DropColumn("dbo.Books", "Category_Id");
            DropColumn("dbo.Books", "Price");
            DropColumn("dbo.Books", "Description");
            DropTable("dbo.Categories");
        }
    }
}
