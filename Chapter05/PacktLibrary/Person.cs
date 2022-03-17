namespace Packt.Shared;

public partial class Person
{
    //fields

    public string? Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public WondersOfTheAncientWorld FavouriteWonder { get; set; }
    
    public WondersOfTheAncientWorld BucketList { get; set; }

    public List<Person> Children = new();
    public BankAccount  CurrentAccount { get; set; } = new( "Flex", 2.3 );
    public static string Species { get; }= "Human";

    public readonly DateTime Instantiated;

    public Person()
    {
        Name = "Unknown";
        Instantiated = DateTime.Now;
    }

    public Person( string name )
    {
        Name = name;
    }

    public void WriteToConsole()
    {
        Console.WriteLine($"My name is {Name}");
    }

    public string GetOrigin()
    {
        return $"{Name}";
    }

    public (string, int) GetFruit()
    {
        return ("Apples", 5 );
    }

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