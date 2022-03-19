namespace Packt.Shared;
public struct DisplacementVector
{
    public int x { get; set; }
    public int y { get; set; }

    public DisplacementVector( int item1, int item2 )
    {
        x = item1;
        y = item2;
    }

    public static DisplacementVector operator +(
        DisplacementVector vector1,
        DisplacementVector vector2 )
    {
        return new(
            vector1.x + vector2.x,
            vector1.y + vector2.y );
    }
}
