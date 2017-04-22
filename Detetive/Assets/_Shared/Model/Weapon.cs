
public class Weapon {
    /// <summary>
    /// Name
    /// </summary>
    public string N { get; set; }
    /// <summary>
    /// MyWeapon
    /// </summary>
    public Enums.Weapons MW { get; set; }

    public Weapon(string name, Enums.Weapons myweapon)
    {
        this.N = name;
        this.MW = myweapon;
    }

}
