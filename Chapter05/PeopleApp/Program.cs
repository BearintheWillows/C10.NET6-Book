using Packt.Shared;
using static System.Console;

Person newPerson = new()
{
    Name = "Stuart",
    DateOfBirth = new DateTime(1991,03,19),
    FavouriteWonder = WondersOfTheAncientWorld.ColossusOfRhodes,
    BucketList = WondersOfTheAncientWorld.LighthouseOfAlexandria | WondersOfTheAncientWorld.TempleOfArtemisAtEphesus,
    CurrentAccount = new("FlexPlus",  2.3),
};

newPerson.Children.Add(new() {Name = "Susie"}); //Add Person to list
newPerson.Children.Add(new() {Name = "Daddy Craig"});


WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}. " +
                  "His favourite wonder is {2}",
    arg0: newPerson.Name,
    arg1: newPerson.DateOfBirth,
    arg2: (int)newPerson.FavouriteWonder);

WriteLine($"{newPerson.Name}'s bucket list is {newPerson.BucketList}");
WriteLine($"{newPerson.Name} has {newPerson.Children.Count} children");
WriteLine( $"{newPerson.CurrentAccount} has {newPerson.CurrentAccount.Balance} and interest is {BankAccount.InterestRate}");

foreach (var VARIABLE in newPerson.Children) //Loop through Stuart's children
{
    WriteLine(VARIABLE.Name);
}
WriteLine($"{newPerson.Name} is a {Person.Species}");

Person newPerson2 = new()
{
    Name = "Kate",
    DateOfBirth = new DateTime(1997, 10, 05),
    FavouriteWonder = WondersOfTheAncientWorld.HangingGardensOfBabylon,
    BucketList = WondersOfTheAncientWorld.LighthouseOfAlexandria | WondersOfTheAncientWorld.HangingGardensOfBabylon,
};

WriteLine(format: "{0} was born on {1:dd MMM yy}",
    arg0: newPerson2.Name,
    arg1: newPerson2.DateOfBirth);

Person newPerson3 = new();

WriteLine(format: "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
    arg0: newPerson3.Name,
    arg1: newPerson3.DateOfBirth,
    arg2: newPerson3.Instantiated);

Person newPerson4 = new( "Bob" );    

newPerson.WriteToConsole();
WriteLine(newPerson.GetOrigin());

(string, int) fruit = newPerson.GetFruit();

WriteLine($"{fruit.Item1}, {fruit.Item2}");

var (name1,dob1) = newPerson;
var (name2, dob2, fav2) = newPerson;

WriteLine($"Deconstructed: {name1}, {dob2}, {fav2}");