
public class PlayerHunch {
    /// <summary>
    /// Player (Player that give this hunch)
    /// </summary>
    public Character P { get; set; }
    /// <summary>
    /// MyHunch (Hunch made by player)
    /// </summary>
    public Hunch MH { get; set; }
    /// <summary>
    /// HunchTime (Time that When hunch was made)
    /// </summary>
    public long HT { get; set; }

    public PlayerHunch(Character player, Hunch hunch, long time)
    {
        this.P = player;
        this.MH = hunch;
        this.HT = time;
    }
}
