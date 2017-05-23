public class Place {

    /// <summary>
    /// My Place (Enum) <see cref="Enums.Places"/>
    /// </summary>s
    public int MP { get; set; }

    /// <summary>
    /// Is this the game host? 
    /// The place to be the police statation or whatever
    ///  1 - true 0 false
    /// </summary>
    public int IH { get; set; }

    public Place(Enums.Places myplace, bool isHost)
    {
        this.MP = myplace.GetHashCode();
        this.IH =  isHost ? 1 : 0;
    }

}
