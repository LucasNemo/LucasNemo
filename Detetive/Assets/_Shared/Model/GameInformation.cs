﻿using System.Collections;
using System.Collections.Generic;

public class GameInformation  {
    /// <summary>
    /// Game Rooms created by Sheriff
    /// </summary>
    public List<Room> Rs { get; set; }

    /// <summary>
    /// Player choosed
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
	public Character P { get; set; }
    
    /// <summary>
    /// Correct Hunch 
    /// </summary>
    public Hunch CH { get; set; }

    public GameInformation(Hunch correctHunch)
    {
        this.CH = correctHunch;
        Rs = new List<Room>();
    }
}
