using Packt.Shared;
using static System.Console;

Person newPerson = new()
{
	Name            = "Stuart",
	DateOfBirth     = new DateTime( 1991, 03, 19 ),
	FavouriteWonder = WondersOfTheAncientWorld.ColossusOfRhodes,
	BucketList      = WondersOfTheAncientWorld.LighthouseOfAlexandria | WondersOfTheAncientWorld.TempleOfArtemisAtEphesus,
	CurrentAccount  = new BankAccount( "FlexPlus", 2.3 ),
};

newPerson.Children.Add( new Person { Name = "Susie" } ); //Add Person to list
newPerson.Children.Add( new Person { Name = "Daddy Craig" } );


WriteLine(
	"{0} was born on {1:dddd, d MMMM yyyy}. " + "His favourite wonder is {2}",
	newPerson.Name,
	newPerson.DateOfBirth,
	( int ) newPerson.FavouriteWonder );

WriteLine( $"{newPerson.Name}'s bucket list is {newPerson.BucketList}" );
WriteLine( $"{newPerson.Name} has {newPerson.Children.Count} children" );
WriteLine( $"{newPerson.CurrentAccount} has {newPerson.CurrentAccount.Balance} and interest is {BankAccount.InterestRate}" );

foreach ( Person VARIABLE in newPerson.Children ) //Loop through Stuart's children
{
	WriteLine( VARIABLE.Name );
}

WriteLine( $"{newPerson.Name} is a {Person.Species}" );

Person newPerson2 = new()
{
	Name            = "Kate",
	DateOfBirth     = new DateTime( 1997, 10, 05 ),
	FavouriteWonder = WondersOfTheAncientWorld.HangingGardensOfBabylon,
	BucketList      = WondersOfTheAncientWorld.LighthouseOfAlexandria | WondersOfTheAncientWorld.HangingGardensOfBabylon,
};

WriteLine(
	"{0} was born on {1:dd MMM yy}",
	newPerson2.Name,
	newPerson2.DateOfBirth );

Person newPerson3 = new();

WriteLine(
	"{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
	newPerson3.Name,
	newPerson3.DateOfBirth,
	newPerson3.Instantiated );

Person newPerson4 = new( "Bob" );

newPerson.WriteToConsole();
WriteLine( newPerson.GetOrigin() );

(string, int) fruit = newPerson.GetFruit();

WriteLine( $"{fruit.Item1}, {fruit.Item2}" );

( string name1, DateTime dob1 )                                = newPerson;
( string name2, DateTime dob2, WondersOfTheAncientWorld fav2 ) = newPerson;

WriteLine( $"Deconstructed: {name1}, {dob2}, {fav2}" );

WriteLine( newPerson.sayHello() );
WriteLine( newPerson.sayHello( "Lenny" ) );
Write( newPerson.OptionalParams() );

WriteLine();

Person Sam = new()
{
	Name        = "Sam",
	DateOfBirth = new DateTime( 1972, 1, 27 ),
};

WriteLine( Sam.Origin );
WriteLine( Sam.Greeting );
WriteLine( Sam.Age );

Sam.Children.Add( new Person { Name = "Ella" } );
Sam.Children.Add( new Person { Name = "Leeroy" } );

WriteLine( $"Sam's children are {Sam[ 0 ].Name} and {Sam[ 1 ].Name}" );

WriteLine();

object[] passengers =
{
	new FirstClassPassenger { AirMiles = 1_419 },
	new FirstClassPassenger { AirMiles = 16_562 },
	new BusinessClassPassenger(),
	new CoachClassPassenger { CarryOnKG = 25.7 },
	new CoachClassPassenger { CarryOnKG = 0 },
};

foreach ( object passenger in passengers )
{
	decimal flightCost = passenger switch
	{
		FirstClassPassenger p when p.AirMiles > 3500  => 1500M,
		FirstClassPassenger p when p.AirMiles > 1500  => 1750M,
		FirstClassPassenger _                         => 2000M,
		BusinessClassPassenger _                      => 1000M,
		CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
		CoachClassPassenger _                         => 650M,
		_                                             => 800M,
	};

	WriteLine( $"Flight costs {flightCost:C} for {passenger}" );
}

//decimal flightCostC9 = passenger switch
//{
/* C# 8 syntax
FirstClassPassenger p when p.AirMiles > 35000 => 1500M,
FirstClassPassenger p when p.AirMiles > 15000 => 1750M,
FirstClassPassenger                           => 2000M, */
// C# 9 or later syntax
//FirstClassPassenger p => p.AirMiles switch
//{
//	> 35000 => 1500M,
//	> 15000 => 1750M,
//	_       => 2000M,
//},
//BusinessClassPassenger                        => 1000M,
//CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
//CoachClassPassenger                           => 650M,
//_                                             => 800M,
//};


ImmutablePerson jeff = new()
{
	FirstName = "Jeff",
	LastName  = "Brown",
};
//jeff.FirstName = "Geoff";

ImmutableVehicle car1 = new()
{
	Make   = "Mazda MX-5 RF",
	Colour = "Soul Red Crystal",
	Wheels = 4,
};

WriteLine( car1.Colour + car1.Make + car1.Wheels );

ImmutableVehicle repaintedCar = car1 with
{
	Colour = "PolyMetal Grey",
};

WriteLine( $"Original car colour was {car1.Colour}" );
WriteLine( $"New car colour is {repaintedCar.Colour}" );

ImmutableAnimal dog = new( "Oscar", "Dog" );

( string who, string what ) = dog;

WriteLine( $"{who} is a {what}" );