using System.Text.RegularExpressions;
using static System.Console;

Write( "Please enter your age:" );
string? input = ReadLine();

/**
 * ^ - Start of input
 * \d - digit
 * + - More than one
 * $ - End of input
 */

Regex ageChecker = new(@"^\d+$");

if ( ageChecker.IsMatch( input ) )
{
    WriteLine( "Thank you!" );
}
else
{
    WriteLine( $"This is not a valid age: {input}" );
}


String? films = "\"Monsters, Inc.\",\"I, Tonya\",\"Lock, Stock and Two Smoking Barrels\"";

WriteLine( $"Films to split: {films}" );

string[]filmsDumb = films.Split(',');

WriteLine( "Splitting with string.split" );

foreach ( var film in filmsDumb )
{
    WriteLine( film );
}