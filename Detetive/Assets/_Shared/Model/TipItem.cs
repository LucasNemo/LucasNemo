
public class TipItem  {
    
    /// <summary>
    /// Tip Path (Sound path to play)
    /// </summary>
    public Enums.TipsSounds TP { get; set; }

    public TipItem(Enums.TipsSounds tipSound)
    {
        this.TP = tipSound;
    }

}
