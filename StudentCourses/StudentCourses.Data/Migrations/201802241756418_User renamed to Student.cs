namespace StudentCourses.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserrenamedtoStudent : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "Students");
            RenameColumn(table: "dbo.Courses", name: "User_Id", newName: "Student_Id");
            RenameColumn(table: "dbo.StudentCourses", name: "UserId", newName: "StudentId");
            RenameIndex(table: "dbo.Courses", name: "IX_User_Id", newName: "IX_Student_Id");
            RenameIndex(table: "dbo.StudentCourses", name: "IX_UserId", newName: "IX_StudentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.StudentCourses", name: "IX_StudentId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Courses", name: "IX_Student_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.StudentCourses", name: "StudentId", newName: "UserId");
            RenameColumn(table: "dbo.Courses", name: "Student_Id", newName: "User_Id");
            RenameTable(name: "dbo.Students", newName: "AspNetUsers");
        }
    }
}
