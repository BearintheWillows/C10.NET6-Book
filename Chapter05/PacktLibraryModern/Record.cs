namespace Packt.Shared;

public class ImmutablePerson
{
	public string? FirstName { get; init; }
	public string? LastName  { get; init; }
}

public record ImmutableVehicle
{
	public int     Wheels { get; init; }
	public string? Colour { get; init; }
	public string? Make   { get; init; }
}

public record ImmutableAnimal( string Name, string Species );