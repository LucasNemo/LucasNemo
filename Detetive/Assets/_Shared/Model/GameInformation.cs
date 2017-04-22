using System.Collections;
using System.Collections.Generic;

public class GameInformation  {
    public List<Room> Rooms { get; set; }
	public Character Player { get; set; }

    public GameInformation()
    {
        Rooms = new List<Room>();
    }
}
