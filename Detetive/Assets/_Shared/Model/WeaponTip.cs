using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTip : TipItem
{
    /// <summary>
    /// Weapon Tip (Weapon that this tips if for)
    /// </summary>
    public Enums.Weapons W { get; set; }

    public WeaponTip(Enums.Weapons weapon, Enums.TipsSounds tipSound) : base(tipSound)
    {
        this.W = weapon;
    }
}
