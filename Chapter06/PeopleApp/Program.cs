using Packt.Shared;
using System.Collections;
using static System.Console;

Person harry = new() {Name = "Harry"};
Person mary = new() {Name = "Mary"};
Person jill = new() {Name = "Jill"};

// call instance method

Person baby1 = mary.ProcreateWith(harry);
baby1.Name = "Gary";

// call static method

Person baby2 = Person.Procreate(harry, jill);

//call operator

Person baby3 = harry * mary;

WriteLine( $"{harry.Name} has {harry.Children.Count} children" );
WriteLine( $"{mary.Name} has {mary.Children.Count} children" );
WriteLine( $"{jill.Name} has {jill.Children.Count} children" );
WriteLine( format: "{0}'s first child is named '{1}'.",
    arg0: harry.Name,
    arg1: harry.Children[0].Name );

//call static method

baby2 = Person.Procreate( harry, jill );

//call operator

baby3 = harry * mary;

WriteLine( $"5! is {Person.Factorial( 5 )}" );

static void Harry_Shout( object? sender, EventArgs e )
{
    if ( sender is null ) return;
    Person p = (Person)sender;
    WriteLine( $"{p.Name} is angry: {p.AngerLevel}." );
}

harry.Shout += Harry_Shout;

for ( int i = 0; i < 4; i++ )
{
    harry.Poke();
}

Hashtable lookUpObject = new();
lookUpObject.Add( key: 1, value: "Alpha" );
lookUpObject.Add( key: 2, value: "Beta" );
lookUpObject.Add( key: 3, value: "Gamma" );
lookUpObject.Add( key: harry, value: "Delta" );

int key = 2; //lookup value that has 2 as its key

WriteLine( format: "key {0} has value: {1}",
    arg0: key,
    arg1: lookUpObject[key] );

//generic lookup collection

Dictionary<int, string> lookUpIntString = new();
lookUpIntString.Add( key: 1, value: "Alpha" );
lookUpIntString.Add( key: 2, value: "Beta" );
lookUpIntString.Add( key: 3, value: "Gamma" );
lookUpIntString.Add( key: 4, value: "Delta" );

Person[] people =
{
    new() { Name = "Simon"},
    new() { Name = "Jenny"},
    new() { Name = "Adam"},
    new() { Name = "Richard"},
};

WriteLine( "Initial List:" );

foreach ( var person in people ) WriteLine( $" {person.Name}" );

WriteLine( "Use Persons Icomparable implementation to sort" );

Array.Sort( people );

WriteLine();

foreach ( var person in people ) WriteLine( $" {person.Name}" );

WriteLine();
WriteLine( "Use PersonComparable IComparer implementation to sort:" );
Array.Sort( people, new PersonComparer() );
foreach ( var person in people )
{
    WriteLine( $" {person.Name}" );
}

WriteLine();

DisplacementVector dv1 = new(5, 5);
DisplacementVector dv2 = new(-2, 7);
DisplacementVector dv3 = dv1 + dv2;
WriteLine( $"({dv1.x}, {dv1.y}) + ({dv2.x}, {dv2.y} = ({dv3.x}, {dv3.y}" );

