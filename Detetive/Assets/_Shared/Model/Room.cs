
public class Room  {
    public Place Place { get; set; }
    public Weapon Weapon { get; set; }

    public Room(Place place, Weapon weapon)
    {
        this.Place = place;
        this.Weapon = weapon;
    }
}
