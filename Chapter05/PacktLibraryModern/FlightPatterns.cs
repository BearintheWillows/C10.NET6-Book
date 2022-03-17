namespace Packt.Shared;

public class BusinessClassPassenger
{
	public override string ToString() => $"Business Class";
}

public class FirstClassPassenger
{
	public int AirMiles { get; set; }
	
	public override string ToString() => $"First Class with {AirMiles:NO} air miles";
}

public class CoachClassPassenger
{
	public double CarryOnKG { get; set; }

	public override string ToString() => $"Coach Class with {CarryOnKG:N2} KG carry on";
}