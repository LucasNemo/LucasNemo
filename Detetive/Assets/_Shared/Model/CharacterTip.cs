
public class CharacterTip : TipItem {

    /// <summary>
    /// Character Tip (Character that this tips if for)
    /// </summary>
    public Enums.Characters CT { get; set; }

    public CharacterTip(Enums.Characters character, Enums.TipsSounds tipSound) : base(tipSound)
    {
        this.CT = character;
    }

}
