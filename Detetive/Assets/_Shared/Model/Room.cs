
using Newtonsoft.Json;
using System.Collections.Generic;

public class Room  {
    /// <summary>
    /// Place 
    /// </summary>
    public Place P { get; set; }
    /// <summary>
    /// Weapon
    /// </summary>
    public Weapon W { get; set; }
    /// <summary>
    /// Tip (A tip item that can be in room)
    /// </summary>
    public List<TipItem> T { get; set; }

    [JsonConstructor]
    public Room(Place place)
    {
        this.P = place;
        this.T = new List<TipItem>();
    }

    public Room(Place place, Weapon weapon) : this(place)
    {
        this.W = weapon;
    }

    public Room(Place place, List<TipItem> tip) : this(place)
    {   
        this.T = tip;
    }

    public Room(Place place, Weapon weapon, List<TipItem> tip) : this(place, tip)
    {
        this.W = weapon;        
    }

}
