using CourseAndStudents; //Academy
using Microsoft.EntityFrameworkCore; //For GenerateCreateScript()
using static System.Console; //For WriteLine()


Academy a = new();

bool deleted = await a.Database.EnsureDeletedAsync();
WriteLine( $"Deleted: {deleted}" );

bool created = await a.Database.EnsureCreatedAsync();
WriteLine( $"Created: {created}" );
WriteLine( "SQL script used to create database: " );
WriteLine( a.Database.GenerateCreateScript() );

foreach ( var s in a.Students.Include( s => s.Courses ) )
{
    WriteLine( "{0} {1} attends the following {2} courses: ",
        s.FirstName, s.LastName, s.Courses.Count );
    foreach ( var c in s.Courses )
    {
        WriteLine( $"  {c.Title}" );
    }
}