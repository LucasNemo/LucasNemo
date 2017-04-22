
public class Room  {
    /// <summary>
    /// Place 
    /// </summary>
    public Place P { get; set; }
    /// <summary>
    /// Weapon
    /// </summary>
    public Weapon W { get; set; }

    public Room(Place place, Weapon weapon)
    {
        this.P = place;
        this.W = weapon;
    }
}
