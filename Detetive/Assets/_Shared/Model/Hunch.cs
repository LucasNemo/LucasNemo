
public class Hunch  {
    /// <summary>
    /// HunchRoom (Room that murder occurred)
    /// </summary>
    public Room HR { get; set; }
    /// <summary>
    /// HunchCharacter (Character that commit the murder)
    /// </summary>
    public int HC { get; set; }

    public Hunch(Room room, int hunchCharacter)
    {
        this.HR = room;
        this.HC = hunchCharacter;
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public static bool operator == (Hunch x, Hunch y)
    {
        //Character/Place/Weapon
        if (x.HC == y.HC && x.HR.P.MP == y.HR.P.MP && x.HR.W.MW == y.HR.W.MW) return true;

        return false;
    }

    public static bool operator !=(Hunch x, Hunch y)
    {
        if (x == y) return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
