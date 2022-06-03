using static System.Console;

// A string array is a squence that implements IEnumerable<string>

string[] names = new[]  { "John", "Paul", "George", "Ringe", "Angela", "Kevine","Creede" };

WriteLine( "Deferred execution" );

//Question: Which names end eith M?
//(Written using LINQ extension method

var query1 = names.Where( n => n.EndsWith( "e" ) );

//Question: Which names end with an M?
//(Written using LINQ query comprehension syntax

var query2 = from name in names where name.EndsWith( "e" ) select name;

//Answer returned as an array of strings containing Pam and Jim

string[] result1 = query1.ToArray();

List<string> result2 = query2.ToList();

foreach ( var item in query1 )
{
    WriteLine( item );
    names[2] = "Georg";
}



WriteLine( "Writing Queries" );
// var query = names.Where(new Func<string, bool>(NameLongerThan4));

IOrderedEnumerable<string> query = names
    .Where( n => n.Length > 4 )
    .OrderBy( n => n.Length )
    .ThenBy( n => n );


static bool NameLongerThan4( string name )
{
    return name.Length > 4;
}

foreach ( var item in query )
{
    WriteLine( item );
}

WriteLine();
WriteLine( "Filtering by type" );
List<Exception> exceptions = new()
{
  new ArgumentException(),
  new SystemException(),
  new IndexOutOfRangeException(),
  new InvalidOperationException(),
  new NullReferenceException(),
  new InvalidCastException(),
  new OverflowException(),
  new DivideByZeroException(),
  new ApplicationException()
};

var arithmeticExceptionQuery = exceptions.OfType<ArithmeticException>();

foreach ( var item in arithmeticExceptionQuery )
{
    WriteLine( item );
}
