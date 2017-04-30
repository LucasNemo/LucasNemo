
public class Weapon {
    
    /// <summary>
    /// MyWeapon <seealso cref="Enums.Weapons"/>
    /// </summary>
    public int MW { get; set; }

    public Weapon(Enums.Weapons myweapon)
    {
        this.MW = myweapon.GetHashCode();
    }

}
