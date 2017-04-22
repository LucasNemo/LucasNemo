

public class Character  {
    
    public string Name { get; set; }
    public Enums.Characters MyCharacter { get; set; }

    public Character(string name, Enums.Characters mycharacter)
    {
        this.Name = name;
        this.MyCharacter = mycharacter;
    }

}
