﻿public class Place {

    /// <summary>
    /// Name
    /// </summary>
    public string N { get; set; }
    /// <summary>
    /// My Place (Enum)
    /// </summary>
    public Enums.Places MP { get; set; }

    public Place(string name, Enums.Places myplace)
    {
        this.N = name;
        this.MP = myplace;
    }

}
