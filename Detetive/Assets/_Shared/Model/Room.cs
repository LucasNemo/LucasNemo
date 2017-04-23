
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
    public TipItem T { get; set; }

    public Room(Place place, Weapon weapon, TipItem tip)
    {
        this.P = place;
        this.W = weapon;
        this.T = tip;
    }

    public Room(Place place, Weapon weapon)
    {
        this.P = place;
        this.W = weapon;
    }

    public Room(Place place, TipItem tip)
    {
        this.P = place;
        this.T = tip;
    }
}
