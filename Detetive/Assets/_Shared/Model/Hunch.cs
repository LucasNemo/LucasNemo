
public class Hunch  {
    /// <summary>
    /// HunchRoom (Room that murder occurred)
    /// </summary>
    public Room HR { get; set; }
    /// <summary>
    /// HunchCharacter (Character that commit the murder)
    /// </summary>
    public Character HC { get; set; }
    /// <summary>
    /// HunchTime (Time that When hunch was made)
    /// </summary>
    public long HT { get; set; }

    public Hunch(Room room, Character hunchCharacter, long time)
    {
        this.HR = room;
        this.HC = hunchCharacter;
        this.HT = time;
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public static bool operator == (Hunch x, Hunch y)
    {
        if (x.HC == y.HC && x.HR == y.HR) return true;

        return false;
    }

    public static bool operator !=(Hunch x, Hunch y)
    {
        if (x == y) return false;

        return true;
    }

}
