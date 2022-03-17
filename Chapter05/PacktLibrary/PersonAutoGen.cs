namespace Packt.Shared;

public partial class Person
{
	//A property using c#1 - 5 Syntax

	public string Origin => $"{Name} loves {FavouriteWonder}";

	//two props defined using c#6 lambda expressions

	public string Greeting => $"{Name} says 'Hello!";

	public int Age => DateTime.Today.Year - DateOfBirth.Year;

	public Person this[ int index ]
	{
		get => Children[ index ]; //Pass on to the List<T> indexer
		set => Children[ index ] = value;
	}
}