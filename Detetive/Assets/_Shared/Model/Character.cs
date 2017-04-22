

public class Character  {
    
    /// <summary>
    /// Name
    /// </summary>
    public string N { get; set; }
    /// <summary>
    /// MyCharacter Enum
    /// </summary>
    public Enums.Characters MC { get; set; }

    public Character(string name, Enums.Characters mycharacter)
    {
        this.N = name;
        this.MC = mycharacter;
    }

}
