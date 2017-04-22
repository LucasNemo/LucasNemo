public class Place {

    public string Name { get; set; }
    public Enums.Places MyPlace { get; set; }

    public Place(string name, Enums.Places myplace)
    {
        this.Name = name;
        this.MyPlace = myplace;
    }

}
