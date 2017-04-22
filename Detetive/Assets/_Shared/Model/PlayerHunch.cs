
public class PlayerHunch {
    public Character Player { get; set; }
    public Hunch MyHunch { get; set; }

    public PlayerHunch(Character player, Hunch hunch)
    {
        this.Player = player;
        this.MyHunch = hunch;
    }
}
