using System.Collections;
using System.Collections.Generic;

public class GameInformation  {
    /// <summary>
    /// Game Rooms created by Sheriff
    /// </summary>
    public List<Room> Rs { get; set; }
    /// <summary>
    /// Player choosed
    /// </summary>
	public Character P { get; set; }
    /// <summary>
    /// Correct Hunch 
    /// </summary>
    public Hunch CH { get; set; }

    /// <summary>
    /// The timer to start the game! (a cooldown to balacen the game)
    /// </summary>
    public float Timer { get; set; }

    public GameInformation(Hunch correctHunch)
    {
        this.CH = correctHunch;
        Rs = new List<Room>();
    }
}
