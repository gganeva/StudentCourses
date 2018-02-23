namespace StudentCourses.Data.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class AddedfirstnamelastnameandfacultynumbertoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 50, defaultValue: "First"));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 50, defaultValue: "Last"));
            AddColumn("dbo.AspNetUsers", "FacultyNumber", c => c.String(nullable: false, defaultValue: "FacNumber"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FacultyNumber");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
