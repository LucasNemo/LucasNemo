
public class Hunch  {
    /// <summary>
    /// Room that murder occurred
    /// </summary>
    public Room HunchRoom { get; set; }
    /// <summary>
    /// Character that commit the murder
    /// </summary>
    public Character HunchCharacter { get; set; }
    /// <summary>
    /// Time that When hunch was made
    /// </summary>
    public float HunchTime { get; set; }

    public Hunch(Room room, Character hunchCharacter, float time)
    {
        this.HunchRoom = room;
        this.HunchCharacter = hunchCharacter;
        this.HunchTime = time;
    }
}
