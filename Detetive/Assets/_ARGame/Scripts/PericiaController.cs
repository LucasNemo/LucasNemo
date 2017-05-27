using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PericiaController : SingletonBehaviour<PericiaController> {

    public GameObject periciaButton;

    private List<Pericia> m_pericias;

    private void Awake()
    {
        m_pericias = new List<Pericia>();
    }

    /// <summary>
    /// Request perica to place
    /// </summary>
    /// <param name="place">Current place</param>
    public void RequestPericia(Enums.Places place)
    {
        if (!AlreadyHasPericia(place))
        {
            var pericia = new Pericia();
            pericia.Place = place;
            pericia.Status = Enums.PericiaStatus.Requested;
            m_pericias.Add(pericia);
        }
    }

    public void Complete(Pericia pericia)
    {
        foreach (var item in m_pericias)
        {
            if (item.Equals(pericia))
            {
                item.Status = Enums.PericiaStatus.Done;
                break;
            }
        }
    }

    /// <summary>
    /// Got a pericia to place
    /// </summary>
    /// <param name="place"></param>
    /// <returns></returns>
    public Pericia GetPericia(Enums.Places place)
    {
        if (AlreadyHasPericia(place))
        {
            return m_pericias.First(x => x.Place == place);
        }

        return null;
    }

    private bool AlreadyHasPericia(Enums.Places place)
    {
        return m_pericias.Any(x => x.Place == place);
    }
    
    /// <summary>
    /// Run all pericias in progress...
    /// </summary>
    /// <param name="place">Current place</param>
    public Pericia Run(Enums.Places place)
    {
        foreach (var pericia in m_pericias.Where(x=>x.Place != place && x.Status == Enums.PericiaStatus.Requested))
        {
            pericia.Status = Enums.PericiaStatus.Result;

            return pericia;
        }

        return null; 
    }

}

public class Pericia
{
    public Enums.PericiaStatus Status { get; set; }
    public Enums.Places Place { get; set; }
}

