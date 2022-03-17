namespace Packt.Shared;

public partial class Person
{
	public readonly DateTime Instantiated;

	public List<Person> Children = new();
	//fields

	public string?  Name        { get; set; }
	public DateTime DateOfBirth { get; set; }

	public WondersOfTheAncientWorld FavouriteWonder { get; set; }

	public        WondersOfTheAncientWorld BucketList     { get; set; }
	public        BankAccount              CurrentAccount { get; set; } = new( "Flex", 2.3 );
	public static string                   Species        { get; }      = "Human";

	public Person()
	{
		Name         = "Unknown";
		Instantiated = DateTime.Now;
	}

	public Person( string name ) { Name = name; }

	public void WriteToConsole() => Console.WriteLine( $"My name is {Name}" );

	public string GetOrigin() => $"{Name}";

	public string sayHello() => $"{Name} says 'Hello!'";

	public string sayHello( string name ) => $"{Name} says 'Hello {name}!'";

	public string OptionalParams( string command = "Run!",
	                              double number  = 0.0,
	                              bool   active  = true ) =>
		$"Command is {command}, Numer is {number}, active is" + $" {active}";

	public void PassingParamters( int x, ref int y, out int z )
	{
		//Out params cannot have a default AND must be initialised
		z = 99;
		x++;
		y++;
		z++;
	}

	public (string, int) GetFruit() => ( "Apples", 5 );

	public void Deconstruct( out string name, out DateTime dob )
	{
		name = Name;
		dob  = DateOfBirth;
	}

	public void Deconstruct( out string name, out DateTime dob, out WondersOfTheAncientWorld fav )
	{
		name = Name;
		dob  = DateOfBirth;
		fav  = WondersOfTheAncientWorld.ColossusOfRhodes;
	}
}