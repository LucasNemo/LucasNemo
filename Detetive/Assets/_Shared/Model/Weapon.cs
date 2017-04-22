
public class Weapon {
    public string Name { get; set; }
    public Enums.Weapons MyWeapon { get; set; }

    public Weapon(string name, Enums.Weapons myweapon)
    {
        this.Name = name;
        this.MyWeapon = myweapon;
    }

}
