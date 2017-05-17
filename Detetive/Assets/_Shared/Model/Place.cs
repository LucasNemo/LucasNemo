public class Place {

    /// <summary>
    /// Name
    /// </summary>
    public string N { get; set; }
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

    public Place(string name, Enums.Places myplace, bool isHost)
    {
        this.N = name;
        this.MP = myplace.GetHashCode();
        this.IH =  isHost ? 1 : 0;
    }

}
