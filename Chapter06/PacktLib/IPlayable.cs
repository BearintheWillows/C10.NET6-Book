namespace Packt.Shared;
public interface IPlayable
{
    void Play();
    void Pause();

    void Stop() //Default
    {
        Console.WriteLine( "Default implementation of Stop" );
    }
}
