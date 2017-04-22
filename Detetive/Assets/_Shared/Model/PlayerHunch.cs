
public class PlayerHunch {
    /// <summary>
    /// Player (Player that give this hunch)
    /// </summary>
    public Character P { get; set; }
    /// <summary>
    /// MyHunch (Hunch made by player)
    /// </summary>
    public Hunch MH { get; set; }

    public PlayerHunch(Character player, Hunch hunch)
    {
        this.P = player;
        this.MH = hunch;
    }
}
