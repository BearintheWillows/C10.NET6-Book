using static System.Console;

namespace Packt.Shared;

public class Person : IComparable<Person>
{

    //fields
    public string? Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Person> Children = new();

    //delegate
    public event EventHandler? Shout;

    //data field

    public int AngerLevel;

    //delegate method usage

    public void Poke()
    {
        AngerLevel++;
        if ( AngerLevel >= 3 )
            if ( Shout != null ) //if something is listening
            {
                Shout( this, EventArgs.Empty ); //Call delegate   
            }
    }

    // Methods

    public void WriteToConsole() => WriteLine( $"{Name} was born on a {DateOfBirth:dddd}." );

    //operator to "multiply"

    public static Person operator *( Person p1, Person p2 ) => Person.Procreate( p1, p2 );


    //Static method to "multiply"

    public static Person Procreate( Person p1, Person p2 )
    {
        Person baby = new()
        {
            Name = $"Baby of {p1.Name} and {p2.Name}"
        };
        p1.Children.Add( baby );
        p2.Children.Add( baby );
        return baby;
    }

    //instance method to "multiply" 
    public Person ProcreateWith( Person partner ) => Procreate( this, partner );

    //method with local func

    public static int Factorial( int num )
    {
        if ( num < 0 )
        {
            throw new ArgumentException( $"{nameof( num )} cannot be less than zero" );
        }

        return LocalFactorial( num );

        int LocalFactorial( int localNum ) //local fun
        {
            if ( localNum < 1 ) return 1;

            return localNum * LocalFactorial( localNum - 1 );
        }
    }

    public int CompareTo( Person? other )
    {
        if ( Name is null ) return 0;

        return Name.CompareTo( other.Name );
    }
}
