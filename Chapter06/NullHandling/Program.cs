int thisCannotBeNull = 4;

// thisCannotBeNull = null; //Compile error

int? thisCouldBeNull = null;

Console.WriteLine( thisCouldBeNull );
Console.WriteLine( thisCouldBeNull.GetValueOrDefault() );
thisCouldBeNull = 7;
Console.WriteLine( thisCouldBeNull );
Console.WriteLine( thisCouldBeNull.GetValueOrDefault() );

class Address
{
    public string? Building;
    public string Street = String.Empty;
    public string City = String.Empty;
    public string Region = String.Empty;
}

Address address = new();
address.Building = null;
address.Street = null;
address.City = "London";
address.Region = null;

// Check that the variable is not null 

Arra

