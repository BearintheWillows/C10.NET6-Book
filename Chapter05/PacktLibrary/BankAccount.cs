namespace Packt.Shared;

public class BankAccount
{
	public        string? AccountName  { get; set; } //Instance
	public        double  Balance      { get; set; } //Instance
	public static double  InterestRate { get; set; } = 0.3;

	public BankAccount( string accountName, double balance )
	{
		AccountName = accountName;
		Balance     = balance;
	}
}